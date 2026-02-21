

using DeltaDrive.Domain;
using System.Drawing;

public class Ride
{
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public double DistanceKM { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public RideStatus Status { get; set; }
        public Passenger Passenger { get; set; }
        public Location StartLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public Vehicle Vehicle { get; set; } 
        public Rating Rating { get; set; }
}

public enum RideStatus
{
    Requested = 0,
    Accepted = 1,
    Rejected = 2,
    DriverOnTheWay = 3,
    InProgress = 4,
    Completed = 5,
    Cancelled = 6
}