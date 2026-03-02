
using Core.Dto;
using DeltaDrive.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeltaDrive.Repository
{
    public class VehicleRepository : BaseRepository<VehicleItem>, IVehicleRepository
    {
        private readonly DeltaDriveDbContext _context;

        public VehicleRepository(DeltaDriveDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TenNearestVehiclesResponseDto> GetAvailableVehicles(TenNearestVehiclesRequestDto request)
        {
            var nearestVehicles = await _context.Vehicles
                .FromSqlInterpolated($@"
                    SELECT
                            ""Id"",
                    ""Brand"",
                    ""Location_Longitude"",
                    ""Location_Latitude"",
                    ""DriverName"",
                    ""DriverSurname"",
                    ""StartPrice"",
                    ""VehicleStatus"",
                    ""pricePerKM"",
                           ST_Distance(
                               geography(ST_SetSRID(ST_MakePoint(""Location_Longitude"", ""Location_Latitude""), 4326)),
                               geography(ST_SetSRID(ST_MakePoint({request.PickUpLocation.Longitude}, {request.PickUpLocation.Latitude}), 4326))
                           ) AS ""distanceToPassenger""
                    FROM ""Vehicles""
                    WHERE ""VehicleStatus"" = 0
                    ORDER BY ""distanceToPassenger""
                    LIMIT 10
                ")
                .ToListAsync();

                //            var rideDistanceMeters = await _context.Database
                //.SqlQuery<double>($@"
                //        SELECT ST_Distance(
                //            geography(ST_SetSRID(ST_MakePoint({request.PickUpLocation.Longitude}, {request.PickUpLocation.Latitude}), 4326)),
                //            geography(ST_SetSRID(ST_MakePoint({request.DestinationLocation.Longitude}, {request.DestinationLocation.Latitude}), 4326))
                //        ) AS ""Value""
                //    ")
                //.SingleAsync();

            var rideDistanceKm = CalculateDistanceFromPickUpToDestionationLocation(request.PickUpLocation, request.DestinationLocation);

            //var rideDistanceKm = rideDistanceMeters / 1000.0;

            var vehiclesDto = nearestVehicles.Select(v =>
            {
                var totalPrice = v.StartPrice + (rideDistanceKm * v.pricePerKM);

                return new VehicleResponseDto(
                    vehicleId: v.Id,
                    model: v.Brand,
                    driverName: v.DriverName!,
                    driverSurname: v.DriverSurname!,
                    distanceToPassenger: Math.Round(v.distanceToPassenger,2),
                    startPrice: Math.Round(v.StartPrice,2),
                    totalPrice: Math.Round(totalPrice, 2)
                );
            }).ToList();

            return new TenNearestVehiclesResponseDto(vehiclesDto);

        }
        /// <summary>
        /// This method calculates the distance in kilometers between the pick-up location and the destination location 
        /// using the Haversine formula, which is a common method for calculating distances between two points on the Earth's surface 
        /// given their latitude and longitude.
        /// </summary>
        /// <param name="pickUpLocation"></param>
        /// <param name="destinationLocaiton"></param>
        /// <returns>Returns value calcualted in kilometers</returns>
        private double CalculateDistanceFromPickUpToDestionationLocation(Location pickUpLocation , Location destinationLocaiton)
        {
            double R = 6371;
          

            double dLat = this.ToRadian(destinationLocaiton.Latitude - pickUpLocation.Latitude);
            double dLon = this.ToRadian(destinationLocaiton.Longitude - pickUpLocation.Longitude);
           

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.ToRadian(pickUpLocation.Latitude)) * Math.Cos(this.ToRadian(destinationLocaiton.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

        /// <summary>
        /// Convert to Radians.
        /// </summary>
        /// <param name=”val”></param>
        /// <returns></returns>
        private double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

    }
}

