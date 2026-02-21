using DeltaDrive.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Features.Vehicle.Query
{
    public class GetClosestVehiclesQueryHandler : IRequestHandler<GetClosestVehiclesQuery, FindTenNearestVehicles>
    {
        private readonly DeltaDriveDbContext _context;

        public GetClosestVehiclesQueryHandler(DeltaDriveDbContext context)
        {
            _context = context;
        }

        public async Task<FindTenNearestVehicles> Handle(GetClosestVehiclesQuery request, CancellationToken cancellationToken)
        {

            double passengerLat = request.latitude;
            double passengerLon = request.longitude;

            var allVehicles = await _context.Vehicles.ToListAsync(cancellationToken);

            var closestVehicles = allVehicles
                .OrderBy(v => CalculateDistance(passengerLat, passengerLon, v.Location.Latitude, v.Location.Longitude))
                .Take(10)
                .ToList();

            return new FindTenNearestVehicles(closestVehicles);
        }


        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Zemljin radijus u km
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle) => angle * Math.PI / 180.0;
    }
}
