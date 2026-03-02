using MediatR;

namespace DeltaDrive.Features.Passengers.Commands
{
    public sealed record BookVehicleCommand(
    int VehicleId,
    int PassengerId,
    double StartLat,
    double StartLon,
    double DestLat,
    double DestLon
) : IRequest<bool>; // vraca RideId
}
