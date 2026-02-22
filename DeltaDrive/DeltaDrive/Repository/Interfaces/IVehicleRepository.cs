using DeltaDrive.Domain;

namespace DeltaDrive.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAvailableVehicles(double lat, double lon);
    }
}
