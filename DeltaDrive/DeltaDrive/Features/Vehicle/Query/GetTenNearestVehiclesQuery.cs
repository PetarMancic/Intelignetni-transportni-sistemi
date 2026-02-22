using DeltaDrive.Dto;
using MediatR;

public record GetTenNearestVehiclesQuery(double latitude, double longitude) : IRequest<TenNearestVehicles>;