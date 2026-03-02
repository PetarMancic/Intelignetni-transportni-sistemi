using Application.Services.Interfaces;
using Core.Dto;
using DeltaDrive.Repository.Interfaces;

namespace Infrastructure.Services
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
        public async Task<TenNearestVehiclesResponseDto> GetTenNearestVehicles(TenNearestVehiclesRequestDto request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetAvailableVehicles(request);
        }
    }
}
