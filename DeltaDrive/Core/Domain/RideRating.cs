public class RideRating
{
	public int Id { get; set; }
	public int RideId { get; set; }
    public double Value { get; set; }
	public int PassengerId { get; set; }
    public string? Comment { get; set; }
	public Ride Ride { get; set; }
	public Passenger Passenger { get; set; }
}
