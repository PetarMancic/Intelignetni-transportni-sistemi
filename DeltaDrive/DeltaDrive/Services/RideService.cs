using DeltaDrive.Domain;
using DeltaDrive.Helpers;
using DeltaDrive.Repository;
using DeltaDrive.Repository.Interfaces;
using DeltaDrive.Services.Interfaces;

namespace DeltaDrive.Services
{
    public class RideService : IRideService
    {

        private readonly DeltaDriveDbContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IRideRepository _rideRepo;
        private readonly IHelperMethods _helperMethods;
        private readonly IPassengerRepository _passengerRepo;

        public RideService(IVehicleRepository vehicleRepo,        
            IHelperMethods helperMethods, 
            IRideRepository rideRepository )
        {
            _vehicleRepo = vehicleRepo;
            _rideRepo = rideRepository;
            _helperMethods = helperMethods;
        }


        public async  Task<Ride> BookRide(int vehicleId, int passengerId, Location start, Location destination)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId);
            if (vehicle.VehicleStatus != VehicleStatus.Available)
                 return null;

            // Business logic: distance, price
            double distanceKm = _helperMethods.CalculateDistanceFromPickUpToDestionationLocation(start, destination);
            double totalPrice = vehicle.StartPrice + vehicle.pricePerKM * distanceKm;

            Passenger passenger = await _passengerRepo.GetByIdAsync(passengerId);
            if (passenger != null)
            {
                throw new Exception($"Passenger with ID {passengerId} not found.");
            }

            var ride = new Ride
            {
                Vehicle = vehicle,
                Passenger = passenger,
                StartLocation = start,
                DestinationLocation = destination,
                DistanceKM = distanceKm,
                TotalPrice = totalPrice,
                Status = RideStatus.Accepted,
                RequestedAt = DateTime.UtcNow
            };

            vehicle.VehicleStatus = VehicleStatus.OnRide;
            await _rideRepo.AddAsync(ride);

            await _context.SaveChangesAsync();

            // Start background task for simulating ride
            // _ = Task.Run(() => SimulateRide(ride));

            return ride;
        }
    }
}
