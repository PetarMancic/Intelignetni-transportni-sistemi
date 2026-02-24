namespace DeltaDrive.Services.Interfaces
{
    public interface  IRideService
    {

        Task<Ride> BookRide(int vehicleId, int passengerId, Location start, Location destination);
    }
}
