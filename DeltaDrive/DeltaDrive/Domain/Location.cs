
public class Location
{
    private double startLat;
    private double startLon;

    public Location(double startLat, double startLon)
    {
        this.startLat = startLat;
        this.startLon = startLon;
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

