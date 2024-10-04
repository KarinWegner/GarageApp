using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GarageApp
{
    internal class Garage<T>
                   where T : Vehicle
    {
        string name;
        int capacity;
        T[] vehicleArray;
        //ToDo: add list of holdable vehicle types to call when adding new vehicle



        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public Garage(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            vehicleArray = new T[capacity];

        }

        public void AddVehicle(T vehicle, int parkingSpot)
        {
            vehicleArray[parkingSpot] = vehicle;
        }

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

        internal int FindParkingSpot()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (vehicleArray[i] == null)
                {
                    return i;
                }

            }
            return Capacity;
        }

        internal bool IsEmpty()
        {
            foreach (var item in vehicleArray)
            {
                if (item != null)
                {
                    return false;
                }
            }
            return true;
        }


        internal void GenerateVehicleList(string? filters)
        {

            if (!string.IsNullOrEmpty(filters))
            {


                List<IEnumerable> filteredList = new List<IEnumerable>();
                string[] filterArray = filters.Split(',');
                string[] filterCategories = new string[filterArray.Length];
                string[] categoryOptions = new string[filterArray.Length];

                for (int i = 0; i < filterArray.Length; i++)
                {


                    filterCategories[i] = filterArray[i].Substring(0, filterArray[i].IndexOf(':'));

                    categoryOptions[i] = filterArray[i].Substring(filterArray[i].IndexOf(':') + 1);
                }
                for (int i = 0; i < filterArray.Length; i++)
                {

                    var filter = vehicleArray.Where(T => T != null)
                                             .Where(T => T.Color == categoryOptions[i])
                                             .Select(T => T.Color);
                    filteredList.Add(filter);
                
                foreach (var item in filteredList[i])
                {
                    Console.WriteLine(item);
                }}
                
            }

            Console.ReadLine();



            var c = vehicleArray.Where(T => T != null);
            c = c.Where(T => T.GetType() == typeof(Car));
            Console.WriteLine($"Cars: {c.Count()}");

            var mc = vehicleArray.Where(T => T != null);
            mc = mc.Where(T => T.GetType() == typeof(Motorcycle));
            Console.WriteLine($"Motorcycles: {mc.Count()}");

            var b = vehicleArray.Where(T => T != null);
            b = b.Where(T => T.GetType() == typeof(Bus));
            Console.WriteLine($"Buses: {b.Count()}");



            var reg = vehicleArray.Where(T => T != null)
                .OrderBy(T => T.RegNumber)
                .Select(T => T.RegNumber);
            var col = vehicleArray.Where(T => T != null)
                .OrderBy(T => T.RegNumber)
                .Select(T => T.Color);
            var veh = vehicleArray.Where(T => T != null)
                .OrderBy(T => T.RegNumber)
                .Select(T => T.GetType().Name);
            //var c = from T in vehicleArray
            //        where T is not null
            //        select T.RegNumber;
            for (int j = 0; j < reg.Count(); j++)

            {
                Console.WriteLine($"{j}:\tRegistration: {reg.ElementAt(j)}\t Type: {veh.ElementAt(j)}");
            }
            return;
        }
    }
}