using DeltaDrive.Domain;

namespace DeltaDrive.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetTenNearestVehicles(double latitude, double longitude);

    }
}
