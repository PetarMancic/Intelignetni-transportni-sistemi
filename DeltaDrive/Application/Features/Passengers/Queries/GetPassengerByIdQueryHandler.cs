using DeltaDrive.Repository.Interfaces;
using MediatR;

namespace DeltaDrive.Features.Passengers.Queries
{
    public sealed class GetPassengerByIdQueryHandler
       : IRequestHandler<GetPassengedByIdQuery, Passenger?>
    {
        private readonly IPassengerRepository _passengerRepository;

        public GetPassengerByIdQueryHandler(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<Passenger?> Handle(
            GetPassengedByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _passengerRepository.GetByIdAsync(request.Id);
        }
    }
}
