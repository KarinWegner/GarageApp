using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace GarageApp
{
    internal class Garage<T> : IEnumerable
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



                string[] filterArray = filters.Split(',');
                string[] filterCategories = new string[filterArray.Length];
                string[] categoryOptions = new string[filterArray.Length];
                string[] subCategoryOptions = new string[filterArray.Length];

                for (int i = 0; i < filterArray.Length; i++)
                {


                    filterCategories[i] = filterArray[i].Substring(0, filterArray[i].IndexOf(':'));

                    categoryOptions[i] = filterArray[i].Substring(filterArray[i].IndexOf(':') + 1); //adds subcategories to relevant array spots
                    if (categoryOptions[i].Contains('_'))
                    {
                        subCategoryOptions[i] = categoryOptions[i].Substring(categoryOptions[i].IndexOf("_") + 1);
                        categoryOptions[i] = categoryOptions[i].Substring(0, categoryOptions[i].IndexOf("_"));          //removes subcategory from category
                    }
                }
                IEnumerable<T>? filteredVehicleColleciton = Enumerable.Empty<T>();
                IEnumerable<T>? combinedCategoryCollection = GetFilterList(filterCategories[0], categoryOptions[0], subCategoryOptions[0]) ?? Enumerable.Empty<T>(); //Lägger in första filtret i concatsamlingen så koden under inte kraschar
                //Creating lists of sorted vehicles for each filter

                bool concatenatingCategories = false;
                for (int i = 1; i < filterArray.Length; i++)
                {

                    if (filterCategories[i] != filterCategories[i - 1])     //Om nuvarande kategorin inte är samma som den innan
                    {
                        //       if (concatenatingCategories)                   //   men concatenatingCategories == true
                        //       {

                        if (combinedCategoryCollection.Count() > 0)        //Lägger bara till intersecten om den faktiskt har något att filtrera.
                        {
                            if (filteredVehicleColleciton.Count() != 0)
                            {
                                filteredVehicleColleciton = (filteredVehicleColleciton ?? Enumerable.Empty<T>()).Intersect((combinedCategoryCollection ?? Enumerable.Empty<T>())); //intersecta in den sparade combinedCategoryCollection i filtreradesamlingen
                            }

                            else
                                filteredVehicleColleciton = combinedCategoryCollection;     //Påbörjar filteredvehiclecollection om den inte redan är igång

                        }
                        //töm concatsamlingen, spara om som nuvarande listan
                        combinedCategoryCollection = GetFilterList(filterCategories[i], categoryOptions[i], subCategoryOptions[i]) ?? Enumerable.Empty<T>();
                      //  concatenatingCategories = false;

                        //     }
                        //Samlar alla fordon som passar in i den kategorins filtrering

                    }
                    else                                                         // Om nuvarande kategorin är samma som den innan concatas nya samlingen till concatsamlingen
                    {
                        combinedCategoryCollection = (combinedCategoryCollection ?? Enumerable.Empty<T>()).Concat(GetFilterList(filterCategories[i], categoryOptions[i], subCategoryOptions[i]) ?? Enumerable.Empty<T>());

                    }
                  

                    //filteredVehicleColleciton = (filteredVehicleColleciton ?? Enumerable.Empty<T>()).Intersect(GetFilterList(filterCategories[i], categoryOptions[i], subCategoryOptions[i]) ?? Enumerable.Empty<T>());

                }          //Lägger på den sista samlingen
                if (filterArray.Length == 1)
                {
                    filteredVehicleColleciton = combinedCategoryCollection;
                }
                else
                        filteredVehicleColleciton = (filteredVehicleColleciton ?? Enumerable.Empty<T>()).Intersect((combinedCategoryCollection ?? Enumerable.Empty<T>()));
                    
                var regFiltered = filteredVehicleColleciton.Select(T => T.RegNumber);
                var colFiltered = filteredVehicleColleciton.Select(T => T.Color);
                var typeFiltered = filteredVehicleColleciton.Select(T => T.GetType().Name);

                for (int j = 0; j < filteredVehicleColleciton.Count(); j++)

                {

                    Console.WriteLine($"{j}:\tRegistration: {regFiltered.ElementAt(j)}\t Color: {colFiltered.ElementAt(j)}\t Type: {typeFiltered.ElementAt(j)}");
                }

            }
            else
            {








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
                    Console.WriteLine($"{j}:\tRegistration: {reg.ElementAt(j)}\t Color: {col.ElementAt(j)}\t Type: {veh.ElementAt(j)}");
                }
            }
            return;
        }

        private List<T> GetFilterList(string v, string c, string? s)
        {
            if (v == "wheel count")
            {

                var filter = vehicleArray.Where(T => T != null);
                if (c.Contains("less"))
                    filter = filter.Where(T => T.WheelCount < int.Parse(s));
                else if (c.Contains("more"))
                    filter = filter.Where(T => T.WheelCount > int.Parse(s));
                return filter.ToList();
            }
            else if (v == "color")
            {
                var filter = vehicleArray.Where(T => T != null)
                                        .Where(T => T.Color == c);
                return filter.ToList();
            }
            else if (v == "registration number")
            {

                var filter = vehicleArray.Where(T => T != null)
                                        .Where(T => T.RegNumber.Contains(s));
                return filter.ToList();


            }
            else if (v == "vehicle type")
            {
                var filter = vehicleArray.Where(T => T != null)
                                        .Where(T => T.GetType().Name == c);
                return filter.ToList();

            }
            return null;
        }

        public IEnumerator GetEnumerator(List<T> list)
        {
            return list.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal bool FindVehicle(string regNumber)
        {
            bool found = vehicleArray.Where(T => T != null)
                .Where(T => T.RegNumber == regNumber).Count() > 0;

            //bool found = false; 
            //foreach (var item in  vehicleArray)
            //{
            //    if (item.RegNumber== regNumber)
            //    {
            //        found = true; 
            //        return found;
            //    }
            //}
            return found;
        }
    }
}