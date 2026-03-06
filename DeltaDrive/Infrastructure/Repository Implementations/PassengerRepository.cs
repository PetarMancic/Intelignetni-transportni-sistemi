using DeltaDrive.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Repository
{
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {
        private readonly DeltaDriveDbContext _context;
        public PassengerRepository(DeltaDriveDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<Passenger> GetByEmailAsync(string email)
        {
            return await _context.Passengers.FirstOrDefaultAsync(p => p.Email == email);

        }

        public async Task<Passenger> GetByVerificationTokenAsync(string token)
        {
            return await _context.Passengers.FirstOrDefaultAsync(p => p.EmailVerificationToken == token);
        }
    }
}
