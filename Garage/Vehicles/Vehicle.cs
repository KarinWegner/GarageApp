namespace GarageApp.Vehicles
{
    internal class Vehicle : IVehicle
    {

        private string regNumber;
        private string color;
        private int wheelCount;

        public int WheelCount { get; set; }
        public string Color { get; set; }
        public string RegNumber { get; set; }

        public Vehicle(string regNumber, string color, int wheelCount)
        {
            WheelCount = wheelCount;
            RegNumber = regNumber;
            Color = color;
        }

        
    }
}