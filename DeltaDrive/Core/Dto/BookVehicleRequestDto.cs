namespace Core.Dto
{
    public sealed record  BookVehicleRequestDto(int VehicleId,
    int PassengerId,
    double StartLat,
    double StartLon,
    double DestLat,
    double DestLon);
}
