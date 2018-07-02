
namespace Open.GooglePhotos
{
    public class GeoPosition
    {
        public GeoPosition()
        {
        }

        public GeoPosition(double latitude, double longitude)
        {
            Altitude = 0;
            Latitude = latitude;
            Longitude = longitude;
        }

        public GeoPosition(double latitude, double longitude, double altitude)
        {
            Altitude = altitude;
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Altitude { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
