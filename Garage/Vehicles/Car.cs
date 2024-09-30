using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Vehicles
{
    internal class Car : Vehicle
    {
        private string fuelType;
        public string FuelType { get; protected set; }
        public Car(string regNumber, string color, int wheelCount, string fuelType) : base(regNumber, color, wheelCount)
        {
           FuelType = fuelType;
        }

        public override string ReturnParametres()
        {
            string parametreString = base.ReturnParametres();
            parametreString += ",Fuel Type";
            return parametreString;
        }
    }
}
