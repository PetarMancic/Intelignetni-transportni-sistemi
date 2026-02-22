using DeltaDrive.Domain;
using DeltaDrive.Repository.Interfaces;
using DeltaDrive.Services.Interfaces;

namespace DeltaDrive.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        /// <summary>
        /// Method used to retrieve the 10 nearest and available  vehicles to the given location (latitude and longitude)
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public async Task<List<Vehicle>> GetTenNearestVehicles(double lat, double lon)
        {
          return await _vehicleRepository.GetAvailableVehicles(lat, lon);
        }


     

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371;
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle) => angle * Math.PI / 180.0;

        private static bool IsVehicleAvailable(Vehicle vehicle)
        {
            if (vehicle == null)
                return false;
            if (vehicle.VehicleStatus != VehicleStatus.Available)
            {
                return false;
            }
            return true;
        }
    }
}
