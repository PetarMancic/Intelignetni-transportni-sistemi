using DeltaDrive.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Repository
{
    public class RideRepository : BaseRepository<Ride>, IRideRepository
    {
        private readonly DeltaDriveDbContext _context;
        public RideRepository(DeltaDriveDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DriverApproachingToPassenger(int rideId)
        {
            var ride = await _context.Rides
            .Include(r => r.Vehicle)  
            .FirstOrDefaultAsync(r => r.Id == rideId);

            ride.Status = RideStatus.Accepted;
            ride.Vehicle.VehicleStatus = VehicleStatus.Unavailable;
            await _context.SaveChangesAsync();
        }
     
        public async Task StartRide(int rideId)
        {
            var ride = await _context.Rides
            .Include(r => r.Vehicle)  
            .FirstOrDefaultAsync(r => r.Id == rideId);

            ride.Status = RideStatus.Completed;
            ride.StartedAt = DateTime.UtcNow;
            ride.Vehicle.VehicleStatus = VehicleStatus.OnRide;
            await _context.SaveChangesAsync();
        }

        public async Task FinishRideAsync(int rideId)
        {
            var ride = await _context.Rides
            .Include(r => r.Vehicle)  
            .FirstOrDefaultAsync(r => r.Id == rideId);

            ride.Status = RideStatus.Completed;
            ride.FinishedAt = DateTime.UtcNow;
            ride.Vehicle.VehicleStatus = VehicleStatus.Available;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ride>> GetRidesByPassengerId(int passengerId)
        {
            List<Ride> rides =  await _context.Rides.Where(r => r.PassengerId == passengerId && r.Status == RideStatus.Completed).ToListAsync();
            return rides;
        }
    }
}
