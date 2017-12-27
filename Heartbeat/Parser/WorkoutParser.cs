using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Heartbeat.Models;

namespace Heartbeat.Parser
{
    public class WorkoutParser
    {
        private readonly string _filePath;
        private readonly IList<Trackpoint> _trackpoints;
        
        public WorkoutParser()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData", "20171017.tcx");
            _trackpoints = new List<Trackpoint>();
        }

        public Dictionary<string, string> GetWorkoutSummary()
        {
            var summary = new Dictionary<string, string>();
            summary.Add("Distance Covered: ", DistanceCovered().ToString("F2"));
            summary.Add("Average Speed: ", AverageSpeed().ToString("F2"));
            summary.Add("Average Heartrate: ", AverageHeartRate().ToString());
            return summary;
        }

        public void ParseXml()
        {
            var xml = XDocument.Load(_filePath);
            XNamespace ns = XNamespace.Get("http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");
            foreach (var node in xml.Descendants(ns + "Trackpoint"))
            {
                _trackpoints.Add(new Trackpoint()
                {
                    Timetamp = Convert.ToDateTime(node.Element(ns + "Time")?.Value),
                    Position = new Position(Convert.ToDouble(node.Element(ns + "Position")?.Element(ns + "LongitudeDegrees")?.Value),
                        Convert.ToDouble(node.Element(ns + "Position")?.Element(ns + "LatitudeDegrees")?.Value)),
                    Altitude = Convert.ToDouble(node.Element(ns + "AltitudeMeters")?.Value),
                    Distance = Convert.ToDouble(node.Element(ns + "DistanceMeters")?.Value),
                    Heartrate = Convert.ToDouble(node.Element(ns + "HeartRateBpm")?.Value),
                    Speed = Convert.ToDouble(node.Element(ns + "Extensions")?.Value)
                });
            }
        }

        private double DistanceCovered()
        {
            return _trackpoints.Last().Distance * 0.000621371;
        }

        private double AverageHeartRate()
        {
            return Math.Floor(_trackpoints.Select(x => x.Heartrate).ToArray().Average());
        }

        private double AverageSpeed()
        {
            //return 26.8224 / _trackpoints.Where(x => x.Speed > 0).Select(x => x.Speed).ToArray().Average();
            return _trackpoints.Select(x => x.Speed).ToArray().Average();
        }
        
        
    }
}