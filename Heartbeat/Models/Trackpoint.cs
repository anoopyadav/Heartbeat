using System;

namespace Heartbeat.Models
{
    public class Trackpoint
    {
        public DateTime Timetamp;
        public Position Position { get; set; }
        public double Altitude { get; set; }
        public double Distance { get; set; }
        public double Heartrate { get; set; }
        public double Speed { get; set; }
    }
}