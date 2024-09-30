using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Vehicles
{
    internal class Bus : Vehicle
    {
        private int numberOfSeats;
        public int NumberOfSeats { get; set; }
        public Bus(string regNumber, string color, int wheelCount, int numberOfSeats) : base(regNumber, color, wheelCount)
        {
            NumberOfSeats = numberOfSeats;
        }

        public override string ReturnParametres()
        {
            string parametreString = base.ReturnParametres();
            parametreString += ",Number of seats";
            return parametreString;
        }
    }
}
