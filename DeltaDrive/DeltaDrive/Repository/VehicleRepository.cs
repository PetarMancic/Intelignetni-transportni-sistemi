using DeltaDrive.Domain;
using DeltaDrive.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly DeltaDriveDbContext _context;

        public VehicleRepository(DeltaDriveDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Vehicle>> GetAvailableVehicles(double lat, double lon)
        {
            return _context.Vehicles
                .FromSqlInterpolated($@"
                    SELECT *,
                        ST_Distance(
                            geography(ST_SetSRID(ST_MakePoint(""Location_Longitude"", ""Location_Latitude""), 4326)),
                            geography(ST_SetSRID(ST_MakePoint({lon}, {lat}), 4326))
                        ) AS distance_meters
                    FROM ""Vehicles""
                    WHERE ""VehicleStatus"" = 0
                    ORDER BY distance_meters
                    LIMIT 10
                ")
                .ToListAsync();
        }

       
    }
}

