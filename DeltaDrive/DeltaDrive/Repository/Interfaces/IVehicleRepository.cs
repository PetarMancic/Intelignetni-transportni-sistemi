using DeltaDrive.Domain;
using DeltaDrive.Dto;
using MediatR;

namespace DeltaDrive.Repository.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<TenNearestVehiclesResponseDto> GetAvailableVehicles(TenNearestVehiclesRequestDto request);
    }
}
