using DeltaDrive.Domain;
using DeltaDrive.Dto;

namespace DeltaDrive.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<TenNearestVehiclesResponseDto> GetTenNearestVehicles(TenNearestVehiclesRequestDto request, CancellationToken cancellationToken);

    }
}
