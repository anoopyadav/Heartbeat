namespace Heartbeat.Models
{
    public class Summary
    {
        public int ActiveSeconds { get; set; }
        public int TotalSeconds { get; set; }
        public int TotalDistance { get; set; }
        public double AverageSpeed { get; set; }
        public int Calories { get; set; }
        public int StepCount { get; set; }
        public int ElevationGain { get; set; }
    }
}