
using Core.Dto;
namespace Application.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<TenNearestVehiclesResponseDto> GetTenNearestVehicles(TenNearestVehiclesRequestDto request, CancellationToken cancellationToken);

    }
}
