using DeltaDrive.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Repository
{
    public class RideRepository : BaseRepository<Ride>, IRideRepository
    {
        public RideRepository(DeltaDriveDbContext context) : base(context)
        {
        }
    }
}
