using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace TotallyAccurateRacingSim
{
    public class SpeedyCamel : GroundVehicle 
    {
        public SpeedyCamel()
        {
            Name = "Speedy camel";
            Speed = 40;
            RestInterval = 10;
        }

        public override double RestDuration(int restCycle)
        {
            switch (restCycle)
            {
                case 0:
                    return 5;
                case 1:
                    return 6.5;
                default:
                    return 8;
            }
        }
    }

    public class TwoHumpedCamel : GroundVehicle 
    {
        public TwoHumpedCamel()
        {
            Name = "Two-humped camel";
            Speed = 10;
            RestInterval = 30;
        }

        public override double RestDuration(int restCycle)
        {
            switch (restCycle)
            {
                case 0:
                    return 5;
                default:
                    return 8;
            }
        }
    }

    public class Centaur : GroundVehicle
    {
        public Centaur()
        {
            Name = "Centaur";
            Speed = 15;
            RestInterval = 8;
        }

        public override double RestDuration(int restCycle)
        {
            return 2;
        }
    }

    public class AllTerrainBoots : GroundVehicle 
    {
        public AllTerrainBoots()
        {
            Name = "All-terrain boots";
            Speed = 6;
            RestInterval = 60;
        }

        public override double RestDuration(int restCycle)
        {
            switch (restCycle)
            {
                case 0:
                    return 10;
                default:
                    return 5;
            }
        }
    }

    public class MagicCarpet : AirVehicle 
    {
        public MagicCarpet()
        {
            Name = "Magic carpet";
            Speed = 10;
        }

        public override double DistanceReducer(double distance)
        {
            if (distance < 1000)
            {
                return distance;
            }

            if (distance < 5000)
            {
                return distance * 0.97;
            }

            if (distance < 10000)
            {
                return distance * 0.9;
            }

            return distance * 0.95;
        }
    }

    public class Mortar : AirVehicle 
    {
        public Mortar()
        {
            Name = "Mortar";
            Speed = 8;
        }

        public override double DistanceReducer(double distance)
        {
            return distance * 0.94;
        }
    }

    public class Broom : AirVehicle
    {
        public Broom()
        {
            Name = "Broom"; 
            Speed = 20;
        }

        public override double DistanceReducer(double distance)
        {
            if (distance >= 100000)
            {
                return 0;
            }
            
            double multiplier = 1;
            for (double i = distance; i >= 1000; i-=1000)
            {
                multiplier -= 0.01;
            }

            return distance * multiplier;
        }
    }
}