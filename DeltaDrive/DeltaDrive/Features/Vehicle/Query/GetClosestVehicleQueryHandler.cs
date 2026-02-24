using DeltaDrive.Dto;
using DeltaDrive.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Features.Vehicle.Query
{
    public class GetClosestVehiclesQueryHandler : IRequestHandler<GetTenNearestVehiclesQuery, TenNearestVehiclesResponseDto>
    {
        private readonly IVehicleService _vehicleService;

        public GetClosestVehiclesQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<TenNearestVehiclesResponseDto> Handle(GetTenNearestVehiclesQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleService.GetTenNearestVehicles(request.Request, cancellationToken);
        }

        //public async Task<TenNearestVehiclesResponseDto> Handle(GetTenNearestVehiclesQuery request, CancellationToken cancellationToken)
        //{
        //    return await _vehicleService.GetTenNearestVehicles(request.Request, cancellationToken);
        //}
    }
}
