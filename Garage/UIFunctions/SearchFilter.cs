using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageApp.UIFunctions
{
    internal class SearchFilter
    {
        private string[] filterCategories;
        public string[][] categoryOptions;

        public static string[] FilterCategories { get; private set; }
        public static string[][] CategoryOptions { get; private set; }

        //ToDo: Add solving for no filters instantiated
        public SearchFilter(List<string> filterList)
        {
            filterCategories = new string[filterList.Count];
            categoryOptions = new string[filterList.Count][];
            for (int i = 0; i < filterList.Count; i++)
            {
                FilterCategories[i] = (filterList[i].Reverse().ToString().Substring(filterList[i].IndexOf(':')).Reverse().ToString());//Reverses filterList string, gets substring from : and reverses string back
                Console.WriteLine(filterCategories);

                CategoryOptions[i] = filterList[i].Substring(filterList[i].IndexOf(':')).Split(','); //Adds the categories to an array of options separated by category
            }
        }

        internal static string CheckFilters(string? currentFilterString)
        {
            string? newFilterString = currentFilterString;
            bool isActive = true;
            while (isActive)
            {
                Console.WriteLine("Currently active filters: ");
                //ToDo: Make function that separates string into rows and "category: filter", ex "color: red"
                Console.WriteLine();
                Console.WriteLine("What do you want to do?"
                                    + "\n1. Add filter"
                                    + "\n2. Remove filter"
                                    + "\n3. Clear filters"
                                    + "\n4. Apply and view filtered search "
                                    + "\n0. Return without saving.");
                char input = UI.RecieveInput(4);
                switch (input)
                {
                    case '1':
                        AddFilter();
                        break;
                    case '2':
                        RemoveFilter();
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '0':
                        isActive = false;
                        break;
                    default:
                        break;
                }


            }
            return newFilterString;
        }
        public static void RemoveFilter()
        {
            throw new NotImplementedException();
        }

        public static void AddFilter()
        {
            Console.WriteLine("What category would you like to filter for?");
            for (int i = 0; i < FilterCategories.Length; i++)
            {
                Console.WriteLine($"{i+1}: {FilterCategories[i]}");
            }
            char input = UI.RecieveInput(FilterCategories.Length);
        }
        public static void FindFilter()
        {
            /*
                Available things to filter:
                Registration numbers?       Color                           Number of Wheels
                *Contains Letter/Digit     Select one or many colors        Specific amount
                *                          Pick from list?                  More than
                *                                                           Less than
             
             Maybe later: Does NOT contain X
           */


        }
    }
}

