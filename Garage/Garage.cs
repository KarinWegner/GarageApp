using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

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


                List<IEnumerable> wheelCountFilters = new List<IEnumerable>();
                List<IEnumerable> colorFilterList = new List<IEnumerable>();
                List<IEnumerable> regFilterList = new List<IEnumerable>();
                List<IEnumerable> vehicleFilterList = new List<IEnumerable>();
                List<IEnumerable> compiledFilterList = new List<IEnumerable>();
                List<IEnumerable> filterList = new List<IEnumerable>();
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

                //Creating lists of sorted vehicles for each filter

                for (int i = 0; i < filterArray.Length; i++)
                {
                    if (filterCategories[i] == "wheel count")
                    {

                        var filter = vehicleArray.Where(T => T != null);
                        if (categoryOptions[i].Contains("less"))
                            filter = filter.Where(T => T.WheelCount < int.Parse(subCategoryOptions[i]));
                        else if (categoryOptions[i].Contains("more"))
                            filter = filter.Where(T => T.WheelCount > int.Parse(subCategoryOptions[i]));
                        wheelCountFilters.Add(filter);
                    }
                    else if (filterCategories[i] == "color")
                    {
                        var filter = vehicleArray.Where(T => T != null)
                                                .Where(T => T.Color == categoryOptions[i])
                                                .Select(T => T.Color);
                        colorFilterList.Add(filter);
                    }
                    else if (filterCategories[i] == "registration number")
                    {

                        var filter = vehicleArray.Where(T => T != null)
                                                .Where(T => T.RegNumber.Contains(subCategoryOptions[i]))
                                                .Select(T => T.RegNumber);
                        regFilterList.Add(filter);


                    }
                    else if (filterCategories[i] == "vehicle type")
                    {
                        var filter = vehicleArray.Where(T => T != null)
                                                .Where(T => T.GetType().Name == categoryOptions[i])
                                                .Select(T => T.GetType().Name);
                        vehicleFilterList.Add(filter);

                    }

                }


                //Combining lists together to make filter
                if (wheelCountFilters.Count > 0)
                {


                    if (wheelCountFilters.Count == 2)       //Finishes the wheelcount intersect before intersecting with other categories.
                    {
                        var first = wheelCountFilters[0];
                        var second = wheelCountFilters[1];
                        var third = first.Cast<T>().Intersect(second.Cast<T>());
                        compiledFilterList.Add(third);
                    }
                    else
                    {
                        compiledFilterList.Add(wheelCountFilters[0]);
                    }
                }

                if (colorFilterList.Count > 0)      //Bestämmer om listan ska läggas till i comletefilterlist
                {
                    var gatheredColorFiltration = colorFilterList[0].Cast<T>();
                    if (colorFilterList.Count > 1)      //Bestämmer om listan ska concatas först
                    {
                        for (int i = 1; i < colorFilterList.Count; i++)
                        {
                            gatheredColorFiltration = (gatheredColorFiltration ?? Enumerable.Empty<T>()).Concat(colorFilterList[i].Cast<T>() ?? Enumerable.Empty<T>());
                        }
                    }
                    compiledFilterList.Add(gatheredColorFiltration);
                }
                if (regFilterList.Count > 0)      //Bestämmer om listan ska läggas till i comletefilterlist
                {
                    var gatheredRegFiltration = regFilterList[0].Cast<T>();
                    if (regFilterList.Count > 1)      //Bestämmer om listan ska concatas först
                    {
                        for (int i = 1; i < regFilterList.Count; i++)
                        {
                            gatheredRegFiltration = (gatheredRegFiltration ?? Enumerable.Empty<T>()).Concat(regFilterList[i].Cast<T>() ?? Enumerable.Empty<T>());
                        }
                    }
                    compiledFilterList.Add(gatheredRegFiltration);
                }
                if (vehicleFilterList.Count > 0)      //Bestämmer om listan ska läggas till i comletefilterlist
                {
                    var gatheredVehicleFiltration = vehicleFilterList[0].Cast<T>();
                    if (vehicleFilterList.Count > 1)      //Bestämmer om listan ska concatas först
                    {
                        for (int i = 1; i < vehicleFilterList.Count; i++)
                        {
                            gatheredVehicleFiltration = (gatheredVehicleFiltration ?? Enumerable.Empty<T>()).Concat(vehicleFilterList[i].Cast<T>() ?? Enumerable.Empty<T>());
                        }
                    }
                    compiledFilterList.Add(gatheredVehicleFiltration);
                }


                //  if (compiledFilterList.Count > 0) är alltid minst ett om denna kod aktiveras
                //  {
                var filteredVehicles = compiledFilterList[0];
                if (compiledFilterList.Count > 1)
                {
                    for (int i = 0; i < compiledFilterList.Count; i++)
                    {

                        filteredVehicles = (filteredVehicles.Cast<T>() ?? Enumerable.Empty<T>()).Intersect((compiledFilterList[i].Cast<T>() ?? Enumerable.Empty<T>()));
                    }

                }

                var filteredReg = filteredVehicles.Cast<T>()
                     .Select(T => T.RegNumber);
                var filteredColor = filteredVehicles.Cast<T>()
                     .Select(T => T.Color);
                var filteredWheelCount = filteredVehicles.Cast<T>()
                    .Select(T => T.WheelCount);
                var filteredVehicleType = filteredVehicles.Cast<T>()
                    .Select(T => T.GetType().Name);

                for (int j = 0; j < filteredReg.Count(); j++)

                {
                    Console.WriteLine($"{j}:\tRegistration: {filteredReg.ElementAt(j)}\t Color: {filteredColor.ElementAt(j)}\t Wheel count: {filteredWheelCount.ElementAt(j)}\t Type: {filteredVehicleType.ElementAt(j)}");
                }
                Console.ReadLine();



                //{
                //    if (i == 0)
                //    {
                //        compiledFilterList.Add(filterList[i]);
                //        continue;
                //    }

                //    if (filterCategories[i] == filterCategories[i - 1])
                //    {
                //        int firstAppearance = compiledFilterList.Where(T => T.index)
                //                             .First(z => z.Item == categoryOptions[i]).Index;                                                           //Finds the first Item in compiledFilterList that has the same value as categoryOptions[i]
                //        compiledFilterList[firstAppearance] = compiledFilterList[firstAppearance].Cast<IENume>().Concat(filterList[i].Cast<T>());       //Joins two Ienumerabes

                //    }
                //    else
                //    {
                //        compiledFilterList.Add(filterList[i]);
                //    }
                //for (int l = 0; l < compiledFilterList.Count; l++)
                //{


                //    Console.WriteLine(i);
                //    foreach (var item in compiledFilterList[i])
                //    {
                //        Console.WriteLine(item);
                //    }

                //}
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
                Console.WriteLine($"{j}:\tRegistration: {reg.ElementAt(j)}\t Type: {veh.ElementAt(j)}");
            }
            }
            return;
        }
    }
}