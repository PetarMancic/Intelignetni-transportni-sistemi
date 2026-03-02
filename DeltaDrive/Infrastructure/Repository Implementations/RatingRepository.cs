using Core.Repository_Interfaces;
using DeltaDrive.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository_Implementations
{
    public class RatingRepository : BaseRepository<RideRating>, IRatingRepository
    {
        private readonly DeltaDriveDbContext _context;
        public RatingRepository(DeltaDriveDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsForRide(int rideId, int passengerId)
        {
           var ratingExists = await _context.Ratings.FindAsync(rideId, passengerId);

            if (ratingExists != null)
                return true;
            return false;
        }

        public async Task<RideRating> GetByRideAndPassengerAsync(int rideId, int passengerId, CancellationToken cancellationToken)
        {
            var rating=  await _context.Ratings.Where(r=> r.RideId== rideId && r.PassengerId == passengerId).FirstOrDefaultAsync(cancellationToken);
            return rating;
        }
    }
}
