
namespace GarageApp {
    internal class GarageHandler
    {
        List<Garage> garageList;


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
        }

        public void AddVehicle()
        {

        }


    }
}