
namespace Application.Service_Interfaces
{
    public interface IRatingService
    {
        Task<bool> LeaveRating(int rideId, int passengerId, double value, string comment);
    }
}
