
namespace GarageApp {
    internal class GarageHandler
    {
        List<Garage> garageList;
        Garage activeGarage;


        public GarageHandler()
        {
            garageList = new List<Garage>();
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

                Console.WriteLine("Enter vehicle information."
                                    + "\nSeparate fields using a comma"
                                    + "\n"
                                    + "\nRequired information:");

                //ToDo: make generic so any base class can be used. 

               // string[] requiredInformation = Vehicle.Parametres.Split(",");
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