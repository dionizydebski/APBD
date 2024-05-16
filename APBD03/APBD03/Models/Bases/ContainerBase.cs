using APBD03.Models.Exceptions;

namespace APBD03.Models.Bases;

public abstract class ContainerBase
{
    public double Weight { get; set; }
    private double Height { get; }
    private double BaseWeight { get; }
    private double Depth { get; }
    public string SerialNumber { get; }
    protected double MaxWeight { get; }
    private static int _id;

    protected ContainerBase(double height, double baseWeight, double depth, char type, double maxWeight)
    {
        _id++;
        Weight = baseWeight;
        Height = height;
        BaseWeight = baseWeight;
        Depth = depth;
        SerialNumber = "KON-"+type+"-"+_id;
        MaxWeight = maxWeight;
    }

    public virtual void Unload()
    {
        Weight = BaseWeight;
        Console.WriteLine("Unloaded: "+ SerialNumber);
    }

    public virtual void Load(double load)
    {
        if (Weight + load > MaxWeight)
        {
            try
            {
                throw new OverfillException("Too much weight!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            Weight =+ load;
            Console.WriteLine("Loaded: " + load + "kg on: " + SerialNumber);
        }
    }

    public override string ToString()
    {
        return SerialNumber + ": weight -> " + Weight + ", height -> " + Height + ", base weight -> " + BaseWeight +
               ", depth -> " + Depth + ", max weight -> " + MaxWeight;
    }
}