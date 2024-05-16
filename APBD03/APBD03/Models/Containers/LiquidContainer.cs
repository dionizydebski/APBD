using APBD03.Models.Bases;
using APBD03.Models.Exceptions;
using APBD03.Models.Interfaces;

namespace APBD03.Models.Containers;

public class LiquidContainer : ContainerBase, IHazardNotifier
{
    private Boolean IsItDangerous { get; }
    
    public LiquidContainer(double height, double baseWeight, double depth, double maxWeight, Boolean isItDangerous) : base(height, baseWeight, depth, 'L', maxWeight)
    {
        IsItDangerous = isItDangerous;
    }

    public void DangerousSituation()
    {
        Console.WriteLine("Dangerous Situation: " + SerialNumber);
    }

    public override void Load(double load)
    {
        if ((IsItDangerous && (Weight + load) / MaxWeight > 0.5) || (!IsItDangerous && (Weight + load) / MaxWeight > 0.9))
        {
            DangerousSituation();
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
            Console.WriteLine("Loaded: " + load + "liters on: " + SerialNumber);
        }
    }
}