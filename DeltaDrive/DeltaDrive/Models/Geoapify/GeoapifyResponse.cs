using System.Text.Json.Serialization;

namespace DeltaDrive.Models.Geoapify
{
    public class GeoapifyResponse
    {
        [JsonPropertyName("features")]
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<List<double[]>> Coordinates { get; set; }
    }
}
