
using System.ComponentModel.Design;
using System.IO.Pipes;
using System.Security.Cryptography;
using System.Threading.Channels;

namespace GarageApp.UIFunctions
{
    public class UI
    {
        private GarageHandler garageHandler = null!;

        public UI()
        {

        }

        public void Run()
        {
            Instantiate();
            StartMenu();
        }

        public void StartMenu()
        {
            bool isActive = true;
            while (isActive)
            {


                Console.Clear();
                Console.WriteLine("Welcome to the GarageApp!"
                                    + "\n"
                                    + "\nNavigate the menu by entering the number in front of action you want to take:"
                                    + "\n1.\tAdd Vehicle to garage"
                                    + "\n2.\tList Vehicles currently in the garage"
                                    + "\n0.\tExit application");
                char input = RecieveInput(2);
                switch (input)
                {
                    case '1':
                        garageHandler.AddVehicle();
                        break;
                    case '2':
                        ViewVehicles();
                        break;
                    case '0':
                        isActive = false;
                        break;
                    default:
                        Console.WriteLine("An error seems to have occurred during the selection input. Please try again.");
                        break;
                }
            }

        }

        private void ViewVehicles()
        {
            string filterString = "";
            bool isActive = true;
            while (isActive)
            {
                isActive = garageHandler.ListVehicles(SearchFilter.ActiveFilters);

                if (!isActive)      //Avslutar metoden om ListVehicles är tom.
                {
                    return;
                }

                Console.WriteLine("What would you like to do:"
                                    + "\n 1. Filter Vehicles"
                                    
                                    + "\n 2. Remove Vehicle"
                                    + "\n 0. Return to Main Menu");
                char input = UI.RecieveInput(2);
                switch (input)
                {

                    case '1':
                        filterString = SearchFilter.CheckFilters();

                        break;
                    case '2':
                        garageHandler.RemoveVehicle();
                        break;
                    case '3':

                        break;
                    case '0':
                        isActive = false;
                        break;
                    default:
                        break;
                }
            }

            Console.ReadLine();
        }

        public static char EnterVehicle()
        {
            //Console.Clear();
            Console.WriteLine("What kind of vehicle is entering the garage?"
                               + "\n1. Motorcycle"
                               + "\n2. Car"
                               + "\n3. Bus"
                               + "\n4. Unspecified Vehicle"
                               + "\n0. Return to menu");

            char input = RecieveInput(4);
            switch (input)
            {
                case '1':
                    return (char)VehicleID.Motorcycle;
                case '2':
                    return (char)VehicleID.Car;
                case '3':
                    return (char)VehicleID.Bus;
                case '4':
                    return (char)VehicleID.Vehicle;
                case '0':
                    return (char)VehicleID.Unknown;
                default:
                    Console.WriteLine("An error occured while recieving input. Returning to previous menu.");
                    return (char)VehicleID.Vehicle;

            }
        }
        public static int RecieveIntInput(int maxOptionNumber)
        {
            int returnInt = 0;
            bool correctInput = false;
            do
            {
                int numberInput; 

                bool isNumber = false;
                string input = Console.ReadLine();
                try
                {



                    isNumber = int.TryParse(input, out _);
                    if (isNumber)
                    {
                        numberInput = int.Parse(input);
                        if (maxOptionNumber < numberInput)
                        {
                            Console.WriteLine("Option not available. Please enter the number of the menu choice you want to choose:"); Console.WriteLine();
                        }
                        else
                        {
                            returnInt = numberInput;
                            correctInput = true;

                        }
                    }

                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }


            } while (!correctInput);
            return returnInt;
        }
        public static char RecieveInput(int maxOptionNumber)
        {
            char returnChar = 'x';
            bool correctInput = false;
            do
            {
                int numberInput = 10; //onåbart av char

                bool isNumber = false;
                string input = Console.ReadLine();
                try
                {



                    isNumber = int.TryParse(input, out numberInput);


                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                if (isNumber)
                {
                    if (maxOptionNumber < numberInput)
                    {
                        Console.WriteLine("Option not available. Please enter the number of the menu choice you want to choose:"); Console.WriteLine();
                    }
                    else
                    {
                        returnChar = input[0];
                        correctInput = true;

                    }
                }

            } while (!correctInput);
            return returnChar;
        }

        public static string RecieveCustomInput(string category)
        {
            string cleanedCategory;
            cleanedCategory = category.Replace(':', ' ').Replace('_', ':');
            string customInput = "";
            bool correctInput = false;

            do
            {
                Console.WriteLine("Enter filter specification:");
                Console.WriteLine(cleanedCategory);
                Console.WriteLine();

                if (category.Contains("wheel count"))
                {
                    customInput = UI.RecieveIntInput(10).ToString();
                    correctInput = true;
                }
                else if (category.Contains("registration number"))
                {
                    customInput = UI.CleanInput(Console.ReadLine());          //Hämtar ut en char som är nummer eller siffra, alt ger den e från empty
                    if (customInput == "empty")
                    {
                        Console.WriteLine("No valid characters were entered. Please try again.");
                    }
                    else
                    {
                        customInput = customInput[0].ToString().ToLower();

                        correctInput = true;

                    }
                }
                else
                {
                    Console.WriteLine("Error in detecting filter category. Please enter your input");
                    string input = Console.ReadLine();


                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Error: Input is empty");
                    }
                    else
                    {
                        customInput = input;
                        correctInput = true;
                    }
                }


            } while (!correctInput);
            return customInput;
        }
        public static string CleanInput(string input)
        {

            input.ToLower();
            string cleanedInput = "";

            string[] inputCleanup = input.Split(',');
            Console.WriteLine($"InputCleanup: {input}");

            for (int i = 0; i < inputCleanup.Length; i++)
            {

                Console.WriteLine($"inputCleanup before trim: '{inputCleanup[i]}'");
                inputCleanup[i] = inputCleanup[i].Trim();
                Console.WriteLine($"inputCleanup after trim: '{inputCleanup[i]}'");

                if (!inputCleanup[i].All(char.IsLetterOrDigit))
                {

                    string newString = inputCleanup[i];
                    inputCleanup[i] = "";
                    foreach (char c in newString)       //Rewrites string without invalid characters
                    {
                        if (char.IsLetterOrDigit(c) /*|| c == '.'*/)  //Not using floats anymore, don't need '.'
                        {

                            inputCleanup[i] += c;
                        }
                        else
                        {
                            Console.WriteLine("An input you entered contained invalid characters. Input was cleaned. To ensure proper naming, please only use Letters, Periods and Digits.");
                        }
                    }

                }

                if (string.IsNullOrEmpty(inputCleanup[i]))             //separate if in case previous cleanup left string empty
                {
                    Console.WriteLine("An input you entered was empty. If you wish to change this, please ensure all inputs contain only Letters and Digits");
                    inputCleanup[i] = "empty";
                }

            }
            cleanedInput = string.Join(',', inputCleanup);
            Console.WriteLine($"Cleaned input: {cleanedInput}");
            return cleanedInput;
        }
        private void Instantiate()
        {
            garageHandler = new GarageHandler();
            garageHandler.AddGarage("Main Garage", 50);
            garageHandler.Seeder();


            string searchFilter1 = "registration number:contains character_";
            string searchFilter2 = "color:red,blue,yellow,purple,black,white,orange,green,brown,pink,gray";
            string searchFilter3 = "wheel count:more than_,less than_";
            string searchFilter4 = "vehicle type:Car,Motorcycle,Bus,Vehicle";
            List<string> searchFilterList = new List<string> { searchFilter1, searchFilter2, searchFilter3, searchFilter4};
            SearchFilter searchFilter = new SearchFilter(searchFilterList);
            SearchFilter.ActiveFilters = "color:blue,registration number:contains character_J,vehicle type:Car";


        }




    }
}