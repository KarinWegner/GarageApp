
using System.Drawing;
using System.IO.Pipes;
using System.Security.Cryptography;

namespace GarageApp {
    internal class GarageHandler
    {
       // List<Garage> garageList;
        Garage<Vehicle> activeGarage;
        Garage<Vehicle> sampleGarage;


        public GarageHandler()
        {
            //garageList = new List<Garage>();

            sampleGarage = new("sampleGarage", 4);
            sampleGarage.CreateVehicle(0,"abc123", "white", 4);
            sampleGarage.CreateMC(1, "abc123", "white", 2, 4.5);
            sampleGarage.CreateCar(2, "abc123", "white", 4, "diesel");
            sampleGarage.CreateBus(3, "abc123", "white", 4, 34);

        }
        public void AddGarage(string name, int capacity)
        {
            Garage<Vehicle> garage = new(name, capacity);
           // garageList.Add(garage);
            Console.WriteLine("Created Garage:"
                             +$"\nName:\t{garage.Name}"
                             +$"\nCapacity:\t{garage.Capacity}");

            activeGarage = garage;                                  //Currently chosen garage
        }
        public void CreateVehicle(int parkingsSpot, string regNumber, string color, int wheelCount)
        {
            // if x
            // Vehicle vehicle = new Vehicle(regNumber, color, wheelCount);
            //  if y
            Car vehicle = new(regNumber, color, wheelCount, "adsad");
            Motorcycle vehicle2 = new(regNumber, color, wheelCount, 1.2);

            //  vehicleArray[parkingsSpot] = vehicle;
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
                    continue;
                }
                string regNumber = "empty"; string color = "empty"; int wheelCount = 0;
                try
                {
                    regNumber = recievedInformation[0];
                    color = recievedInformation[1];
                    wheelCount = int.Parse(recievedInformation[2]);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("The information entered was in the wrong format. Please try again.");
                    continue;
                }

                switch ((int)vehicleType)
                {
                    case 1:

                        break;
                    case 2:
                        break;
                    case 3:
                        Car vehicle = new(regNumber, color, wheelCount, "adsad");
                        break;
                    case 4:
                        break;
                    default: Console.WriteLine("Error.");
                        Console.ReadLine();
                        break;
                }


                Console.ReadLine();
                

            }
        }


    }
}