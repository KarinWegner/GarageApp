using System.Runtime.CompilerServices;

namespace GarageApp.Vehicles
{
    internal class Vehicle : IVehicle
    {

        private string regNumber;
        private string color;
        private int wheelCount;
        private string parametres;

        public string Parametres { get; private set; }

        public int WheelCount { get; set; }
        public string Color { get; set; }
        public string RegNumber { get; set; }

        public Vehicle(string regNumber, string color, int wheelCount)
        {
            WheelCount = wheelCount;
            RegNumber = regNumber;
            Color = color;
            Parametres = SetParametres();
        }

        internal virtual string SetParametres()
        {
            string parameterString = String.Concat("RegistrationNumber,", "Color,", "Number of Wheels,");

            Console.WriteLine($"SetParametres: Parametres set to {parameterString}");

            return parameterString;
        }
        
    }
}