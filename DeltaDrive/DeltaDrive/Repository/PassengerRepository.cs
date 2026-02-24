using DeltaDrive.Repository.Interfaces;

namespace DeltaDrive.Repository
{
    public class PassengerRepository : BaseRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(DeltaDriveDbContext _context) :base(_context)
        {
            
        }

    }
}
