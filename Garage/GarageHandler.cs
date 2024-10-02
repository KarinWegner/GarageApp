
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


        public GarageHandler()
        {
            //garageList = new List<Garage>();

            sampleGarage = new("sampleGarage", 4);
            sampleGarage.AddVehicle(new Vehicle("abc123", "white", 4), 0);
            sampleGarage.AddVehicle(new Motorcycle("abc123", "white", 2, 4.5), 1);
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
                            double cylinderCount = float.Parse(recievedInformation[3]);
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
                    
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Some of the information entered was in the wrong format. Please try again.");
                    Console.ReadLine();
                    continue;
                }
            Console.WriteLine($"Vehicle was entered into the garage at parking spot {parkingSpot}");

                Console.ReadLine();
                isActive = false;


            }
        }


    }
}