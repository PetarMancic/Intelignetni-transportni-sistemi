using DeltaDrive.Models.Geoapify;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class GeoapifyService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeoapifyService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiKey = configuration["Geoapify:ApiKey"]
               ?? throw new Exception("Geoapify API key nije konfigurisan.");
            _httpClient = httpClient;
        }

        public async Task<List<Location>> GetRouteCoordinates(Location start, Location destination)
        {

            string url = $"https://api.geoapify.com/v1/routing?" +
                         $"waypoints={start.Latitude}%2C{start.Longitude}%7C{destination.Latitude}%2C{destination.Longitude}" +
                         $"&mode=drive&apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();


            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Geoapify response: {json}");

            var root = JsonSerializer.Deserialize<GeoapifyResponse>(json);

            var coordinates = new List<Location>();

            foreach (var feature in root.Features)
            {
                foreach (var line in feature.Geometry.Coordinates)
                {
                    foreach (var point in line)
                    {
                        coordinates.Add(new Location
                        {
                            Longitude = point[0],
                            Latitude = point[1]
                        });
                    }
                }
            }

            return coordinates;
        }
    }
}
