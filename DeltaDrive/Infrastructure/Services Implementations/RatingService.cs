
using Application.Service_Interfaces;
using Core.Repository_Interfaces;
using DeltaDrive.Repository.Interfaces;

namespace Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRideRepository _rideRepository;

        public RatingService(IRatingRepository ratingRepository, IRideRepository rideRepository)
        {
            _ratingRepository = ratingRepository;
            _rideRepository = rideRepository;
        }

        public async Task<bool> LeaveRating(int rideId, int passengerId, double value, string comment)
        {
            // 1. Proveri da li voznja postoji i da li je završena
            var ride = await _rideRepository.GetByIdAsync(rideId);
            if (ride == null || ride.Status != RideStatus.Completed)
                return false;

            // 2. Proveri da li je passenger bio u toj voznji
            if (ride.PassengerId != passengerId)
                return false;

            // 3. Proveri da li je vec ocenio
            var alreadyRated = await _ratingRepository.ExistsForRide(rideId, passengerId);
            if (alreadyRated)
                return false;

            // 4. Sacuvaj ocenu
            var rating = new RideRating
            {
                RideId = rideId,
                PassengerId = passengerId,
                Value = value,
                Comment = comment
            };

            await _ratingRepository.AddAsync(rating);
            return true;
        }
    }
}
