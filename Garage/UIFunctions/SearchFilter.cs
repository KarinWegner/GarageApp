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
        private string? activeFilters;

        public static string[] FilterCategories { get; private set; }
        public static string[][] CategoryOptions { get; private set; }

        public static string? ActiveFilters { get; set; }

        //ToDo: Add solving for no filters instantiated
        public SearchFilter(List<string> filterList)
        {
            ActiveFilters = "";
            FilterCategories = new string[filterList.Count];
            CategoryOptions = new string[filterList.Count][];
            for (int i = 0; i < filterList.Count; i++)
            {
                FilterCategories[i] = filterList[i].Substring(0, filterList[i].IndexOf(':'));

                CategoryOptions[i] = filterList[i].Substring(filterList[i].IndexOf(':') + 1).Split(','); //Adds the categories to an array of options separated by category
            }
        }

        internal static string CheckFilters()
        {
            bool isActive = true;
            while (isActive)
            {
                Console.WriteLine("Currently active filters: ");
                if (string.IsNullOrEmpty(ActiveFilters))
                {
                    Console.Write("none");
                }
                else
                {
                    string[] currentFilters = ActiveFilters.Split(',');
                    foreach (string filter in currentFilters)
                    {
                        Console.WriteLine(filter);
                    }
                }
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
                        AddFilter(FindFilter());
                        break;
                    case '2':
                        RemoveFilter();
                        break;
                    case '3':
                        ClearFilters();
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
            return ActiveFilters;
        }

        private static void ClearFilters()
        {
            throw new NotImplementedException();
        }

        public static void RemoveFilter()
        {
            if (string.IsNullOrEmpty(ActiveFilters))
            {
                Console.WriteLine("No filters have been selected. Returning to previous menu");
                return;
            }
            Console.WriteLine("Which selected filter would you like to remove?");
            string[] currentFilters = ActiveFilters.Split(',');
            for (int i = 0; i < currentFilters.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {currentFilters[i]}");
            }
            Console.WriteLine("0. Return to previous menu");
            int selectedOption = 0;
            if (currentFilters.Length > 9)
            {
                int bigInput = UI.RecieveIntInput(currentFilters.Length);

                selectedOption = bigInput - 1;
            }
            else
            {
                char input = UI.RecieveInput(currentFilters.Length);

                selectedOption = (int)char.GetNumericValue(input) - 1;
            }
            if (selectedOption < 0)
            {
                return;                                                           //starts sequence over
            }
            RemoveFilter(currentFilters[selectedOption]);

        }
        public static void RemoveFilter(string filter)
        {
            if (filter == "")
            {
                return;
            }
            if (!ActiveFilters.Contains(filter))
            {
                Console.WriteLine("Filter is not selected");
                Console.ReadLine();
                return;
            }
            else
            {
                string newFilters = "";
                string[] filters = ActiveFilters.Split(',');
                for (int i = 0; i < filters.Length; i++)
                {
                    if (filters[i].Contains(filter))
                    {

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(newFilters))
                        {
                            newFilters += "," + filters[i];
                        }
                        else
                        {
                            newFilters += filters[i];
                        }
                    }

                }
                ActiveFilters = newFilters;
                return;
            }
        }

        public static void AddFilter(string filter)
        {
            if (filter == "")
            {
                return;
            }
            if (string.IsNullOrEmpty(ActiveFilters) || !ActiveFilters.Contains(filter))
            {
                Console.WriteLine("Added filter to list!");


                if (!string.IsNullOrEmpty(ActiveFilters))
                    ActiveFilters += ",";     //First string in ActivatedFilters doesn't need the comma

                ActiveFilters += filter;

                string[] ActiveFilterSorter = ActiveFilters.Split(',');
                Array.Sort(ActiveFilterSorter);
                ActiveFilters = string.Join(",",ActiveFilterSorter);

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
                    case '0':
                        Console.WriteLine("Returning to previous menu.");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("An error has occured. Returning to previous menu.");
                        Console.ReadLine();
                        return; ;
                }

            }


        }
        public static string? FindFilter()
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
                    return "";
                }

                int selectedCategory = (int)Char.GetNumericValue(input);
                selectedCategory -= 1;
                Console.WriteLine($"Which filter in{FilterCategories[selectedCategory]} would you like to add?");
                //                       + "\nHint: Select an already added filter to remove it");

                for (int i = 0; i < CategoryOptions[selectedCategory].Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {CategoryOptions[selectedCategory][i]}");
                }
                Console.WriteLine("0. Cancel and return to previous menu.");

                int bigInput = 0;
                int selectedOption = 0;
                if (CategoryOptions[selectedCategory].Count() > 9)
                {
                    bigInput = UI.RecieveIntInput(FilterCategories[selectedCategory].Length);

                    selectedOption = bigInput - 1;
                }
                else
                {
                    input = UI.RecieveInput(FilterCategories[selectedCategory].Length);

                    selectedOption = (int)char.GetNumericValue(input) - 1;
                }
                if (selectedOption < 0)
                {
                    continue;                                                           //starts sequence over
                }
                string? enteredData = "";

                string completeFilterString = FilterCategories[selectedCategory] + ":" + CategoryOptions[selectedCategory][selectedOption];

                if (CategoryOptions[selectedCategory][selectedOption].Contains('_'))
                {
                    //ToDo: find better way to find if input requires only int or char input
                    
                    enteredData = UI.RecieveCustomInput(completeFilterString);
                    completeFilterString += enteredData;
                }

                Console.WriteLine($"Adding filter {completeFilterString}");
                return completeFilterString;
            }
            return "";
        }
    }
}

