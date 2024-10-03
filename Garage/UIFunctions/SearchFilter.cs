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
        private string[][] categoryOptions;
        private string? activatedFilters;

        public static string[] FilterCategories { get; private set; }
        public static string[][] CategoryOptions { get; private set; }

        public static string? ActivatedFilters { get; set; }

        //ToDo: Add solving for no filters instantiated
        public SearchFilter(List<string> filterList)
        {
            activatedFilters = "";
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
                        FindFilter();
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
        public static void RemoveFilter(string filter)
        {
            if (!ActivatedFilters.Contains(filter))
            {
                Console.WriteLine("Filter is not selected");
                Console.ReadLine();
                return;
            }
            else
            {

            }
        }

        public static void AddFilter(string filter)
        {
            if (!ActivatedFilters.Contains(filter))
            {
                Console.WriteLine("Added filter to list!");


                if(!string.IsNullOrEmpty(ActivatedFilters))
                    ActivatedFilters += ",";     //First string in ActivatedFilters doesn't need the comma
                
                ActivatedFilters += filter;

                return;
            }
           else
            {
                Console.WriteLine($"Filter {filter} has already been selected. Do you want to deselect it?"
                                    + "\n 1. Yes"
                                    + "\n 0. No");
                char input = UI.RecieveInput(1);
                switch (input)
                {
                    case '1':
                        RemoveFilter(filter);
                        return;
                    case '0':Console.WriteLine("Returning to previous menu.");
                        Console.ReadLine();
                        return;
                    default :
                        Console.WriteLine("An error has occured. Returning to previous menu.");
                        Console.ReadLine();
                        return; ;
                }
               
            }


        }
        public static void FindFilter()
        {
            bool isActive = true;
            while (isActive)
            {
                Console.WriteLine("What category would you like to filter for?");


                for (int i = 0; i < FilterCategories.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {FilterCategories[i]}");
                }
                Console.WriteLine("0. return to previous menu");
                char input = UI.RecieveInput(FilterCategories.Length);

                if (input == '0')
                {
                    return;
                }

                int selectedCategory = input - 1;
                Console.WriteLine($"Which filter in{FilterCategories[selectedCategory]} would you like to add?"
                                    + "Hint: Select an already added filter to remove it");

                for (int i = 0; i < CategoryOptions[selectedCategory].Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {CategoryOptions[selectedCategory][i]}");
                }
                Console.WriteLine("0. Cancel and return to previous menu.");
                input = UI.RecieveInput(FilterCategories[selectedCategory].Length);

                if (input == '0')
                {
                    continue;                                                           //starts sequence over
                }
                int selectedOption = input - 1;
                string completeFilterString = FilterCategories[selectedCategory] + ":" + CategoryOptions[selectedCategory][selectedOption];

                Console.WriteLine($"Adding filter {completeFilterString}");
                AddFilter(completeFilterString);
            }
        }
    }
}

