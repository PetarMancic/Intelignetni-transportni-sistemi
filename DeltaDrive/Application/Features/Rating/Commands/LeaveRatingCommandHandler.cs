

using Application.Exceptions;
using Core.Repository_Interfaces;
using DeltaDrive.Features.Passengers.Queries;
using DeltaDrive.Repository.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace Application.Features.Rating.Commands
{
    public sealed class LeaveRatingCommandHandler : IRequestHandler<LeaveRatingCommand, bool>
    {
        private readonly IRideRepository rideRepository;
        private readonly IPassengerRepository passengerRepository;
        private readonly IRatingRepository ratingRepository;

        public LeaveRatingCommandHandler(IRideRepository _rideRepository,
            IPassengerRepository _passengerRepository,
            IRatingRepository _ratingRepository)

        {
            rideRepository = _rideRepository;
            passengerRepository = _passengerRepository;
            ratingRepository = _ratingRepository;

        }

        public async Task<bool> Handle(LeaveRatingCommand request, CancellationToken cancellationToken)
        {
            var ride = await rideRepository.GetByIdAsync(request.RideId);
            if(ride is null)
                throw new NotFoundException("ride", request.RideId);

            var passenger = await passengerRepository.GetByIdAsync(request.PassengerId);

            if( passenger is null)
                throw new NotFoundException("passenger", request.PassengerId);

            // Proveri da li je vožnja završena
            if (ride.Status != RideStatus.Completed)
                throw new InvalidOperationException("Ocenjivanje je moguće samo za završene vožnje.");

            // Proveri da li je putnik već ostavio ocenu
            var existingRating = await ratingRepository
                .GetByRideAndPassengerAsync(request.RideId, request.PassengerId, cancellationToken);

            if (existingRating is not null)
                throw new InvalidOperationException("Passenger has already rated this ride.");

            var rating = new RideRating
            {
                RideId = request.RideId,
                PassengerId = request.PassengerId,
                Value = request.Value,
                Comment = request.Comment
            };

            await ratingRepository.AddAsync(rating);
            return true;
        }
    }
}
