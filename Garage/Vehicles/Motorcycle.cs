using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.Vehicles
{
    internal class Motorcycle : Vehicle
    {
        private int cylinderVolume;
        public int CylinderVolume { get; set; }  
        public Motorcycle(string regNumber, string color, int wheelCount, int cylinderVolume) : base(regNumber, color, wheelCount)
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
