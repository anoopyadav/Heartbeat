using System;
using System.Collections.Generic;
using System.Linq;
using Heartbeat.Models;
using Heartbeat.Parser;

namespace Heartbeat.Statistics
{
    public class DailyWorkout
    {
        private readonly WorkoutParser _parser;
        private readonly IList<Trackpoint> _trackpoints;
        

        public DailyWorkout()
        {
            _parser = new WorkoutParser();
            _trackpoints = _parser.Trackpoints;
            AnalyseWorkout();
        }

        private void AnalyseWorkout()
        {
            CalculateTotalDistanceCovered();
        }

        private double CalculateTotalDistanceCovered()
        {
            return _trackpoints.Last().Distance * 0.000621371;
        }

        private double CalculateAverageHeartRate()
        {
            return Math.Floor(_trackpoints.Select(x => x.Heartrate).ToArray().Average());
        }
        
        private double CalculateAverageSpeed()
        {
            //return 26.8224 / _trackpoints.Where(x => x.Speed > 0).Select(x => x.Speed).ToArray().Average();
            return _trackpoints.Select(x => x.Speed).ToArray().Average();
        }

        public double TotalDistanceCovered => CalculateTotalDistanceCovered();

        public double AverageHeartRate => CalculateAverageHeartRate();

        public double AverageSpeed => CalculateAverageSpeed();
    }
}