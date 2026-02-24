using DeltaDrive.Dto;
using MediatR;

public sealed record GetTenNearestVehiclesQuery(
    TenNearestVehiclesRequestDto Request) 
    : IRequest<TenNearestVehiclesResponseDto>;