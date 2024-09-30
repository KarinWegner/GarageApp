namespace GarageApp.Vehicles
{
    internal interface IVehicle
    {
        string Color { get; set; }
        string RegNumber { get; set; }
        int WheelCount { get; set; }
    }
}