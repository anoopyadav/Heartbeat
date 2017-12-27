using System;
using Heartbeat.Parser;

namespace Heartbeat
{
    static class Program
    {
        public static void Main(string[] args)
        {
            WorkoutParser parser = new WorkoutParser();
            parser.ParseXml();

            foreach (var result in parser.GetWorkoutSummary())
            {
                Console.WriteLine($"{result.Key}{result.Value}");
            }
        }
    }
}