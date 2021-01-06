using System.Linq.Expressions;

namespace TotallyAccurateRacingSim
{
    public class AirVehicle : IVehicle
    {
        public string Name { get; set; }
        public RaceType Type { get; } = RaceType.Air;
        public double TotalTime { get; set; }
        public double Speed { get; set; }
        public virtual double DistanceReducer(double distance) => default;

        public double GetRaceTime(double distance)
        {
            return TotalTime = DistanceReducer(distance) / Speed;
        }
        
        public override string ToString()
        {
            return Name + " | " + TotalTime;
        }
    }
}