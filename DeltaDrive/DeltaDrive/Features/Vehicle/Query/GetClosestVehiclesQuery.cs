using DeltaDrive.Dto;
using MediatR;

public record GetClosestVehiclesQuery(double latitude, double longitude) : IRequest<FindTenNearestVehicles>;