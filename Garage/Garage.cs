using System.Collections;
namespace GarageApp { 
internal class Garage
{ 
    string name;
    int capacity;
    Vehicle[] vehicleArray;

    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public Garage(string name, int capacity)
    {
        this.Name = name;
        this.Capacity = capacity;
        vehicleArray = new Vehicle[capacity];

    }

    public void AddVehicle(string regNumber, string color, int wheelCount)
    {
        Vehicle vehicle = new Vehicle(regNumber, color, wheelCount);
    }
    


}}