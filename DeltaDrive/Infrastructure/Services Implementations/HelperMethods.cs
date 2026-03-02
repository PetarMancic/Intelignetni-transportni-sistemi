namespace DeltaDrive.Helpers
{
    public class HelperMethods : IHelperMethods
    {

        /// <summary>
        /// This methos is used to calculate distance between pick-up location and destination location using Haversine formula.
        /// </summary>
        /// <param name="pickUpLocation"></param>
        /// <param name="destinationLocaiton"></param>
        /// <returns>Returns value calcualted in kilometers</returns>
        public double CalculateDistanceFromPickUpToDestionationLocation(Location pickUpLocation, Location destinationLocaiton)
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
