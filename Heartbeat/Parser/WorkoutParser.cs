using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Heartbeat.Models;

namespace Heartbeat.Parser
{
    public class WorkoutParser
    {
        private readonly string _filePath;
        public IList<Trackpoint> Trackpoints { get; }
        public Summary Summary { get; }
        
        public WorkoutParser()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleData", "20171017.tcx");
            Trackpoints = new List<Trackpoint>();
            Summary = new Summary();
            ParseTrackpoints();
            ParseSummary();
        }

        private void ParseTrackpoints()
        {
            var xml = XDocument.Load(_filePath);
            XNamespace ns = XNamespace.Get("http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");
            
            // Gather all the trackpoints
            foreach (var node in xml.Descendants(ns + "Trackpoint"))
            {
                Trackpoints.Add(new Trackpoint()
                {
                    Timetamp = Convert.ToDateTime(node.Element(ns + "Time")?.Value),
                    Position = new Position(
                        Convert.ToDouble(node.Element(ns + "Position")?.Element(ns + "LongitudeDegrees")?.Value),
                        Convert.ToDouble(node.Element(ns + "Position")?.Element(ns + "LatitudeDegrees")?.Value)),
                    Altitude = Convert.ToDouble(node.Element(ns + "AltitudeMeters")?.Value),
                    Distance = Convert.ToDouble(node.Element(ns + "DistanceMeters")?.Value),
                    Heartrate = Convert.ToDouble(node.Element(ns + "HeartRateBpm")?.Value),
                    Speed = Convert.ToDouble(node.Element(ns + "Extensions")?.Value)
                });
            }
        }

        private void ParseSummary()
        {
            var xml = XDocument.Load(_filePath);
            XNamespace ns = XNamespace.Get("http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");
            
            // Parse the Workout Summary
            var summaryNode = xml.Descendants(ns + "Extensions")
                .FirstOrDefault(x => x.HasAttributes && x.FirstAttribute.Value == "Extensions_t");//?.Descendants(ns +"x" + "LX").FirstOrDefault();

            Summary.ActiveSeconds = Convert.ToInt32(summaryNode?.Element(ns + "ActiveSeconds")?.Value);
            Summary.TotalSeconds = Convert.ToInt32(summaryNode?.Element(ns + "ElapsedSeconds")?.Value);
            Summary.TotalDistance = Convert.ToInt32(summaryNode?.Element(ns + "DistanceMeters")?.Value);
            Summary.AverageSpeed = Convert.ToDouble(summaryNode?.Element(ns + "AvgSpeed")?.Value);
            Summary.Calories = Convert.ToInt32(summaryNode?.Element(ns + "KiloCalories")?.Value);
            Summary.StepCount = Convert.ToInt32(summaryNode?.Element(ns + "StepCount")?.Value);
            Summary.ElevationGain = Convert.ToInt32(summaryNode?.Element(ns + "ClimbMeters")?.Value);
        }
    }
}