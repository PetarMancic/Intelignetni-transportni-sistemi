using DeltaDrive.Repository.Interfaces;
using MediatR;

namespace Application.Features.Rides.Queries
{
    public sealed class GetHistoryOfPassengerRidesQueryHandler : IRequestHandler<GetHistoryOfPassengerRidesQuery, List<Ride>>
    {
        private readonly IRideRepository _rideRepository;
        public GetHistoryOfPassengerRidesQueryHandler( IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        public  async Task<List<Ride>> Handle(GetHistoryOfPassengerRidesQuery request, CancellationToken cancellationToken)
        {
            List<Ride> rides =  await _rideRepository.GetRidesByPassengerId(request.passengerId);

            return rides;
        }
    }
}
