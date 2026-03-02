namespace DeltaDrive.Repository.Interfaces
{
    public interface IRideRepository : IBaseRepository<Ride>
    {
        Task FinishRideAsync(int rideId);
        Task StartRide(int rideId);
        Task DriverApproachingToPassenger(int ride);
        Task <List<Ride>> GetRidesByPassengerId(int passengerId);
    }
}
