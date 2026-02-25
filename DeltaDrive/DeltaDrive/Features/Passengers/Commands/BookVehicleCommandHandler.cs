using DeltaDrive.Services.Interfaces;
using MediatR;

namespace DeltaDrive.Features.Passengers.Commands
{
    public class BookVehicleCommandHandler : IRequestHandler<BookVehicleCommand, bool>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IRideService _rideService;

        public BookVehicleCommandHandler(IVehicleService vehicleService, IRideService rideService)
        {
            _vehicleService = vehicleService;
            _rideService = rideService;
        }
        public async Task<bool> Handle(BookVehicleCommand request, CancellationToken cancellationToken)
        {
            Location startLocation = new Location (request.StartLat, request.StartLon);
            Location destinationLocation = new Location(request.DestLat, request.DestLon);

            var ride= await _rideService.BookRide(request.VehicleId, request.PassengerId, startLocation, destinationLocation);
            if (ride != null)
                return true;
            return false;
            // var number= Math.Round(1 + new Random().NextDouble()*4);
        }
    }
}
