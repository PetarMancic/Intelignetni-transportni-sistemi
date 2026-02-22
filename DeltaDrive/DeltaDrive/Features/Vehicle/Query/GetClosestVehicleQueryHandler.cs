using DeltaDrive.Dto;
using DeltaDrive.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Features.Vehicle.Query
{
    public class GetClosestVehiclesQueryHandler : IRequestHandler<GetTenNearestVehiclesQuery, TenNearestVehicles>
    {
        private readonly IVehicleService _vehicleService;

        public GetClosestVehiclesQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<TenNearestVehicles> Handle(GetTenNearestVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetTenNearestVehicles(request.latitude, request.longitude);

            return new TenNearestVehicles(vehicles);
        }
    }
}
