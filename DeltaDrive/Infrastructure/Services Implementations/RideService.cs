

using Application.Services.Interfaces;
using DeltaDrive.Helpers;
using DeltaDrive.HubSimulation;
using DeltaDrive.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Services
{
    public class RideService : IRideService
    {

        private readonly DeltaDriveDbContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IRideRepository _rideRepo;
        private readonly IHelperMethods _helperMethods;
        private readonly IPassengerRepository _passengerRepo;
        private readonly IHubContext<RideHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly GeoapifyService _geoapifyService;

        public RideService(
            DeltaDriveDbContext context,
            IVehicleRepository vehicleRepo,
            IHelperMethods helperMethods,
            IRideRepository rideRepository,
            IPassengerRepository passengerRepository,
            GeoapifyService geoapifyService,
            IHubContext<RideHub> hubContext,
            IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _vehicleRepo = vehicleRepo;
            _rideRepo = rideRepository;
            _helperMethods = helperMethods;
            _passengerRepo = passengerRepository;
            _geoapifyService = geoapifyService;
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }


        public async Task<Ride> BookRide(int vehicleId, int passengerId, Location start, Location destination)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId);
            if (vehicle.VehicleStatus != VehicleStatus.Available)
                return null;

            // Business logic: distance, price
            double distanceKm = _helperMethods.CalculateDistanceFromPickUpToDestionationLocation(start, destination);
            double totalPrice = vehicle.StartPrice + vehicle.pricePerKM * distanceKm;

            Passenger passenger = await _passengerRepo.GetByIdAsync(passengerId);
            if (passenger == null)
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

            // Start background task for simulating ride
            _ = Task.Run(() => SimulateRide(ride));

            return ride;
        }



        public async Task SimulateRide(Ride ride)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DeltaDriveDbContext>();
            var rideRepo = scope.ServiceProvider.GetRequiredService<IRideRepository>();

            var vehicle = ride.Vehicle;
            string group = $"ride-{ride.Id}";

            var vehicleLocation = new Location { Latitude = vehicle.Location.Latitude, Longitude = vehicle.Location.Longitude };

            // FAZA 1: Vozač ide do putnika
            await rideRepo.DriverApproachingToPassenger(ride.Id);

            var routeToPassenger = await _geoapifyService.GetRouteCoordinates(vehicleLocation, ride.StartLocation);
            await SimulateMovement(group, vehicle, routeToPassenger, "DRIVER_APPROACHING", context);

            //zapocni voznju 
            await rideRepo.StartRide(ride.Id);

            // FAZA 2: Vozač vozi putnika do destinacije
            var routeToDestination = await _geoapifyService.GetRouteCoordinates(ride.StartLocation, ride.DestinationLocation);
            await SimulateMovement(group, vehicle, routeToDestination, "ON_RIDE", context);

            // Završi vožnju
            await rideRepo.FinishRideAsync(ride.Id);

            await _hubContext.Clients.Group(group).SendAsync("RideCompleted", new
            {
                rideId = ride.Id,
                message = "Vožnja završena!"
            });
        }

        private async Task SimulateMovement(string group, VehicleItem vehicle, List<Location> route, string phase, DeltaDriveDbContext context)
        {
            foreach (var point in route)
            {
                // fetch iz novog contexta, ne koristi stari vehicle objekat
                var vehicleInDb = await context.Vehicles
                    .Include(v => v.Location)
                    .FirstOrDefaultAsync(v => v.Id == vehicle.Id);

                vehicleInDb.Location.Latitude = point.Latitude;
                vehicleInDb.Location.Longitude = point.Longitude;
                await context.SaveChangesAsync();

                await _hubContext.Clients.Group(group).SendAsync("LocationUpdate", new
                {
                    phase,
                    latitude = point.Latitude,
                    longitude = point.Longitude
                });

                Console.WriteLine($"Latitude: {point.Latitude} Longitude: {point.Longitude}");
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }
    }
}
