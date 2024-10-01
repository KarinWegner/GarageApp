using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Vehicles
{
    internal class Motorcycle : Vehicle
    {
        private double cylinderVolume;
        public double CylinderVolume { get; set; }  
        public Motorcycle(string regNumber, string color, int wheelCount, double cylinderVolume) : base(regNumber, color, wheelCount)
        {
            CylinderVolume = cylinderVolume;
        }
        internal override string SetParametres()
        {
            string parametreString = base.SetParametres();
            parametreString += ",Cylinder Volume";
            return parametreString;
        }
    }
}
