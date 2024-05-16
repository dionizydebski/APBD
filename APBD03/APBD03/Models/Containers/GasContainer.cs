using APBD03.Models.Bases;
using APBD03.Models.Exceptions;
using APBD03.Models.Interfaces;

namespace APBD03.Models.Containers;

public class GasContainer : ContainerBase, IHazardNotifier
{
    private double Pressure { get; set; }
    private double MaxPressure { get; }
    public GasContainer(double height, double baseWeight, double depth, double maxWeight, int maxPressure) : base(height, baseWeight, depth, 'G', maxWeight)
    {
        Pressure = 0;
        MaxPressure = maxPressure;
    }

    public void DangerousSituation()
    {
        Console.WriteLine("Dangerous Situation: " + SerialNumber);
    }

    public override void Load(double load)
    {
        if (Pressure + load > MaxPressure)
        {
            DangerousSituation();
            try
            {
                throw new OverfillException("Too much atm!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            Pressure =+ load;
            Console.WriteLine("Loaded: " + load + "atm on: " + SerialNumber);
        }
    }
    
    public override void Unload()
    {
        Pressure *= 0.05;
        Console.WriteLine("Unloaded: "+ SerialNumber);
    }
    public override string ToString()
    {
        return base.ToString() + ", pressure -> " + Pressure + ", max pressure -> " + MaxPressure;
    }
}