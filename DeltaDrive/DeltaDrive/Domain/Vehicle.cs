namespace DeltaDrive.Domain
{
    public enum VehicleStatus
    {
        Available,
        OnRide,
        Unavailable
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public Location? Location { get; set; }
        public string? DriverName { get; set; }
        public string? DriverSurname { get; set; }
        public double distanceToPassenger { get; set; }
        public double pricePerKM { get; set; }
        public double StartPrice { get; set; }

        public VehicleStatus VehicleStatus { get; set; } = VehicleStatus.Available;
    }
}
