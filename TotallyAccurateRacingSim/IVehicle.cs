namespace TotallyAccurateRacingSim
{
    // public enum VehicleType
    // {
    //     Air,
    //     Ground
    // }

    public interface IVehicle
    {
        double Speed { get; set; }
        string Name { get; set; }
        RaceType Type { get; }

        double GetRaceTime(double distance) => default;
        double TotalTime { get; set; }
        
    }
}