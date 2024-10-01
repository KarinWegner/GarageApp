using System.Collections;
namespace GarageApp
{
    internal class Garage
    {
        string name;
        int capacity;
        Vehicle[] vehicleArray;
        //ToDo: add list of holdable vehicle types to call when adding new vehicle



        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public Garage(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            vehicleArray = new Vehicle[capacity];

        }

        #region CreateVehicle            
        public void CreateVehicle(int parkingSpot, string regNumber, string color, int wheelCount)//ToDo: make generic
        {
            Vehicle vehicle = new Vehicle(regNumber, color, wheelCount);
            vehicleArray[parkingSpot] = vehicle;                                //ToDo, automate finding a free spot
        }
        public void CreateCar(int parkingsSpot, string regNumber, string color, int wheelCount, string fuelType)
        {
            Car car = new Car(regNumber, color, wheelCount, fuelType);
            vehicleArray[parkingsSpot] = car;
        }
        public void CreateMC(int parkingsSpot, string regNumber, string color, int wheelCount, double cylinderVolume)
        {
            Motorcycle mc = new Motorcycle(regNumber, color, wheelCount, cylinderVolume);
            vehicleArray[parkingsSpot] = mc;
        }
        public void CreateBus(int parkingsSpot, string regNumber, string color, int wheelCount, int numberOfSeats)
        {
            Bus bus = new Bus(regNumber, color, wheelCount, numberOfSeats);
            vehicleArray[parkingsSpot] = bus;
        }
        #endregion CreateVehicle

        public Vehicle GetVehicle(int parkingSpot)
        {
            Vehicle vehicle = vehicleArray[parkingSpot];
            
            return vehicle;
        }
        public bool IsFull()
        {
            bool isFull;
            foreach (var item in vehicleArray)
            {
                if (item == null)
                {
                    return false;
                }
            }

            //ToDo: Add check to find how many slots in vehicleArray are free to list number of available spots
            //Console.WriteLine($"Garage capacity: {Capacity}"
            //                    +$"\nSpots filled: {spotsFilled}"
            //                    +$"\nSpots available: {Capacity - spotsFilled}");


            isFull = true;

            return isFull;
        }



    }
}