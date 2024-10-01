
namespace GarageApp {
    internal class GarageHandler
    {
        List<Garage> garageList;
        Garage activeGarage;
        Garage sampleGarage;


        public GarageHandler()
        {
            garageList = new List<Garage>();

            sampleGarage = new Garage("sampleGarage", 4);
            sampleGarage.CreateVehicle(0,"abc123", "white", 4);
            sampleGarage.CreateMC(1, "abc123", "white", 2, 4.5);
            sampleGarage.CreateCar(2, "abc123", "white", 4, "diesel");
            sampleGarage.CreateBus(3, "abc123", "white", 4, 34);

        }
        public void AddGarage(string name, int capacity)
        {
            Garage garage = new Garage(name, capacity);
            garageList.Add(garage);
            Console.WriteLine("Created Garage:"
                             +$"\nName:\t{garage.Name}"
                             +$"\nCapacity:\t{garage.Capacity}");

            activeGarage = garage;
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
                Console.ReadLine();

                string input = UI.CleanInput(Console.ReadLine());
                Console.WriteLine($"Input: {input}");
                Console.ReadLine();

            }
        }


    }
}