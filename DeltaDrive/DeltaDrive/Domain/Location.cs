
public class Location
{

    public double Latitude { get; set; }
    public double Longitude { get; set; }


    public Location() { }
    public Location(double startLat, double startLon)
    {
        this.Latitude = startLat;
        this.Longitude = startLon;
    }
}

