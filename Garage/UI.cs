
using System.ComponentModel.Design;
using System.IO.Pipes;

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
                                    + "\n0.\tExit application");
                char input = RecieveInput(1);
                switch (input)
                {
                    case '1':

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
                    if (maxOptionNumber > numberInput)
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
        private void Instantiate()
        {
            garageHandler = new GarageHandler();
            garageHandler.AddGarage("Main Garage", 50);
        }
    }
}