
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
            if (activeGarage.IsFull())
            {
                Console.WriteLine($"{activeGarage.Name} is full!");
            }
            else
            {
                //ToDo: Call to UI to ask what kind of vehicle you want to add. Call ReturnParameters, have user enter required parametres
            }
        }


    }
}