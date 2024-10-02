
using System.ComponentModel.Design;
using System.IO.Pipes;
using System.Security.Cryptography;

namespace GarageApp
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
                                    +"\n2.\tList Vehicles currently in the garage"
                                    + "\n0.\tExit application");
                char input = RecieveInput(2);
                switch (input)
                {
                    case '1':
                        garageHandler.AddVehicle();
                        break;
                    case '2':
                        garageHandler.ListVehicles();
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
                    return (char)(VehicleID.Vehicle);
                case '0':
                    return (char)VehicleID.Unknown;
                default:
                    Console.WriteLine("An error occured while recieving input. Returning to previous menu.");
                    return (char)VehicleID.Vehicle;

            }
        }
        void RecieveInput(string input) { }
        static char RecieveInput(int maxOptionNumber)
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
        public static string CleanInput(string input)
        {

            input.ToLower();
            string cleanedInput = "";

            string[] inputCleanup = input.Split(',');
            Console.WriteLine($"InputCleanup: {input}");

            for (int i = 0; i < inputCleanup.Length; i++)
            {

                Console.WriteLine($"inputCleanup before trim: '{inputCleanup[i]}'");
                inputCleanup[i] =inputCleanup[i].Trim();
                Console.WriteLine($"inputCleanup after trim: '{inputCleanup[i]}'");

                if (!inputCleanup[i].All(Char.IsLetterOrDigit))
                {

                    string newString = inputCleanup[i];
                    inputCleanup[i] = "";
                    foreach (char c in newString)       //Rewrites string without invalid characters
                    {
                        if (char.IsLetterOrDigit(c) || c == '.')
                        {
                            
                            inputCleanup[i] += c;
                        }
                        else
                        {
                            Console.WriteLine("An input you entered contained invalid characters. Input was cleaned. To ensure proper naming, please only use Letters, Periods and Digits.");
                        }
                    }

                }

                if (String.IsNullOrEmpty(inputCleanup[i]))             //separate if in case previous cleanup left string empty
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


        }
    }
}