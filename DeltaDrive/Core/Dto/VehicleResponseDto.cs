namespace Core.Dto
{
    public sealed record VehicleResponseDto(
        int vehicleId,
        string model,
        string driverName,
        string driverSurname, 
        double distanceToPassenger, 
        double startPrice,
        double totalPrice,
        double latitude,
        double longitude
        );    
}
