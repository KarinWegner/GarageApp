using Microsoft.VisualBasic.FileIO;
using System.Drawing;
using System.IO.Pipes;
using System.Security.Cryptography;

namespace GarageApp
{
    internal class GarageHandler
    {
        // List<Garage> garageList;
        Garage<Vehicle> activeGarage;
        Garage<Vehicle> sampleGarage;
        private string filterString = "";

        public string FilterString { get; private set; }


        public GarageHandler()
        {
            //garageList = new List<Garage>();

            sampleGarage = new("sampleGarage", 4);
            sampleGarage.AddVehicle(new Vehicle("abc123", "white", 4), 0);
            sampleGarage.AddVehicle(new Motorcycle("abc123", "white", 2, 4), 1);
            sampleGarage.AddVehicle(new Car("abc123", "white", 4, "diesel"), 2);
            sampleGarage.AddVehicle(new Bus("abc123", "white", 4, 34), 3);

        }
        public void AddGarage(string name, int capacity)
        {
            Garage<Vehicle> garage = new(name, capacity);
            // garageList.Add(garage);
            Console.WriteLine("Created Garage:"
                             + $"\nName:\t{garage.Name}"
                             + $"\nCapacity:\t{garage.Capacity}");

            activeGarage = garage;                                  //Currently chosen garage
        }

        public void Seeder()
        {

            activeGarage.AddVehicle(new Motorcycle("jts647", "blue", 2, 10), 0);
            activeGarage.AddVehicle(new Bus("xug175", "red", 6, 30), 1);
            activeGarage.AddVehicle(new Motorcycle("mqp032", "purple", 2, 5), 2);
            activeGarage.AddVehicle(new Car("hlv759", "green", 4, "diesel"), 3);
            activeGarage.AddVehicle(new Car("ptf070", "blue", 4, "petrol"), 4);
            activeGarage.AddVehicle(new Bus("rmv104", "blue", 4, 18), 5);
            activeGarage.AddVehicle(new Bus("msb786", "black", 6, 28), 6);
            activeGarage.AddVehicle(new Motorcycle("xdb238", "red", 2, 1), 7);
            activeGarage.AddVehicle(new Motorcycle("tqy456", "red", 3, 5), 8);
            activeGarage.AddVehicle(new Car("jfn060", "blue", 4, "petrol"), 9);
            activeGarage.AddVehicle(new Motorcycle("yij473", "purple", 2, 5), 10);
            activeGarage.AddVehicle(new Bus("lft899", "black", 8, 42), 11);
            activeGarage.AddVehicle(new Motorcycle("zpf596", "gray", 2, 2), 12);
            return;

        }

        public void AddVehicle()
        {
            bool isActive = true;
            while (isActive)
            {
                if (activeGarage.IsFull())
                {
                    Console.WriteLine($"{activeGarage.Name} is full!");
                    Console.ReadLine();
                    return;
                }
                int parkingSpot = activeGarage.FindParkingSpot();

                char vehicleType = UI.EnterVehicle();       //gets the type of vehicle wanted
                if (vehicleType == '0')                     //ID 0 is for returning
                {
                    return;
                }
                //string vehicleTypeString = vehicleType.ToString();
                //Console.WriteLine(vehicleTypeString);
                //Console.ReadLine();
                int sampleVehicle = (int)vehicleType - 1;
                //Console.WriteLine(sampleVehicle);
                Console.WriteLine("Enter vehicle information."
                                    + "\nSeparate fields using a comma"
                                    + "\n"
                                    + "\nRequired information:");

                //ToDo: make generic so any base class can be used. 

                string[] requiredInformation = sampleGarage.GetVehicle(sampleVehicle).Parametres.Split(",");
                foreach (string item in requiredInformation)
                {
                    Console.WriteLine($"- {item}");
                }
                string dirtyInput = Console.ReadLine();
                string input = UI.CleanInput(dirtyInput);

                Console.WriteLine($"Input: {input}");

                string[] recievedInformation = input.Split(",");

                if (requiredInformation.Length != recievedInformation.Length)
                {
                    Console.WriteLine("Incorrect number of entries. Please only enter the required information and only put commas between entries.");
                    Console.ReadLine();
                    continue;
                }
                string regNumber = "empty"; string color = "empty"; int wheelCount = 0;
                try
                {
                    regNumber = recievedInformation[0];
                    color = recievedInformation[1];
                    wheelCount = int.Parse(recievedInformation[2]);

                    bool repeatedRegNumber = activeGarage.FindVehicle(regNumber);
                    if (repeatedRegNumber)
                    {
                        Console.WriteLine("There is already a vehicle with the entered registration number! Vehicle cannot be entered.");
                    }
                    else
                    {
                        //ToDo: Add parkingSpots
                        switch ((int)vehicleType)
                        {
                            case 1:
                                Vehicle vehicle = new(regNumber, color, wheelCount);
                                activeGarage.AddVehicle(vehicle, parkingSpot);
                                Console.WriteLine("ADDED VEHICLE");

                                Console.ReadLine();
                                break;
                            case 2:
                                int cylinderCount = int.Parse(recievedInformation[3]);
                                activeGarage.AddVehicle(new Motorcycle(regNumber, color, wheelCount, cylinderCount), parkingSpot);
                                Console.WriteLine("ADDED MC");
                                Console.ReadLine();
                                break;
                            case 3:
                                string fuelType = recievedInformation[3];
                                activeGarage.AddVehicle(new Car(regNumber, color, wheelCount, fuelType), parkingSpot);
                                Console.WriteLine(" ADDED CAR");
                                Console.ReadLine();
                                break;
                            case 4:
                                int numberOfSeats = int.Parse(recievedInformation[3]);
                                activeGarage.AddVehicle(new Bus(regNumber, color, wheelCount, numberOfSeats), parkingSpot);
                                Console.WriteLine("ADDED BUS");
                                Console.ReadLine();
                                break;
                            default:
                                Console.WriteLine("Error.");
                                Console.ReadLine();
                                break;
                        }

                        Console.WriteLine($"Vehicle was entered into the garage at parking spot {parkingSpot}");
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Some of the information entered was in the wrong format. Please try again.");
                    Console.ReadLine();
                    continue;
                }

                Console.ReadLine();
                isActive = false;


            }
        }

        internal bool ListVehicles(string? filterList)
        {
            Console.Clear();


            if (activeGarage.IsEmpty())
            {
                Console.WriteLine("Garage is empty!");
                Console.ReadLine();
                return false;
            }
            Console.WriteLine("Vehicles currently in garage: ");
            activeGarage.GenerateVehicleList(filterList);
            return true;
        }

        internal void RemoveVehicle()
        {
            Console.WriteLine("Please enter the registration number of the vehicle you'd like to remove:");
            Console.WriteLine("Leave input empty to return to previous menu.");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return;

            bool vehicleInGarage = activeGarage.FindVehicle(input);

            if (vehicleInGarage)
            {
                Console.WriteLine("Vehicle found! Removing from garage");
                activeGarage.RemoveVehicle(input);
            }
            else
                Console.WriteLine("Vehicle with matching registration number coild not be found. ");
        }
    }
}