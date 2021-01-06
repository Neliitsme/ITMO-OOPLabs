using System.Linq.Expressions;

namespace TotallyAccurateRacingSim
{
    public class GroundVehicle : IVehicle
    {
        public string Name { get; set; }
        public RaceType Type { get; } = RaceType.Ground;
        public double Speed { get; set; }
        public double RestInterval { get; set; }
        public virtual double RestDuration(int restCycle) => default;
        public double TotalTime { get; set; }
        public double GetRaceTime(double distance)
        {

            double travelTime = 0;
            int stopsAmount = 0;
            double stopsTime = 0;

            while (distance > 0)
            {
                if (distance < Speed*RestInterval)
                {
                    double multi = distance / (Speed * RestInterval);
                    travelTime = travelTime + RestInterval * multi;
                    distance = distance - Speed * RestInterval;
                }
                else
                {
                    distance = distance - Speed * RestInterval;
                    travelTime += RestInterval;
                    stopsAmount++;
                }
            }
            for (int i = 0; i < stopsAmount; i++)
            {
                stopsTime += RestDuration(i);
            }
            return TotalTime = travelTime + stopsTime;
        }

        public override string ToString()
        {
            return Name + " | " + TotalTime;
        }
    }
}