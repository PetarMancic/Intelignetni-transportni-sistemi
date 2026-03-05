
using DeltaDrive.Repository.Interfaces;

namespace Core.Repository_Interfaces
{
    public interface IRatingRepository : IBaseRepository<RideRating>
    {
        Task<bool> ExistsForRide(int rideId, int passengerId);
        Task<RideRating> GetByRideAndPassengerAsync(int rideId, int passengerId, CancellationToken cancellationToken);
        Task<List<RideRating>> GetRatingsByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken);
    }
}
