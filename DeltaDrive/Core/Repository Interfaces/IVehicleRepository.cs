using Core.Dto;

namespace DeltaDrive.Repository.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<VehicleItem>
    {
        Task<TenNearestVehiclesResponseDto> GetAvailableVehicles(TenNearestVehiclesRequestDto request);
    }
}
