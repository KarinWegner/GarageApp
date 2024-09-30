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

        public void CreateVehicle(string regNumber, string color, int wheelCount)
        {
            Vehicle vehicle = new Vehicle(regNumber, color, wheelCount);                //ToDo: Update to create subtypes
            vehicleArray[vehicleArray.Count()] = vehicle;
        }

        public bool IsFull()
        {
            bool isFull;
            int spotsFilled = vehicleArray.Count();

            Console.WriteLine($"Garage capacity: {Capacity}"
                                +$"\nSpots filled: {spotsFilled}"
                                +$"\nSpots available: {Capacity - spotsFilled}");

            Console.WriteLine();
            if (spotsFilled == Capacity) 
                isFull = true;
            else
                isFull = false;

            return isFull;
        }



    }
}