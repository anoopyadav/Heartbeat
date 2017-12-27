namespace Heartbeat.Models
{
    public class Position
    {
        private double Longitude { get; set; }
        private double Latitude { get; set; }

        public Position(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}