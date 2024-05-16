using APBD03.Models.Bases;

namespace APBD03.Models.Containers;

public class ColdContainer : ContainerBase
{
    private string _loadName;
    
    private double _temperature;
    
    private static Dictionary<string, double> _productsList = new Dictionary<string, double>()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 },
    };
    public ColdContainer(double height, double baseWeight, double depth, double maxWeight, string loadName, double temperature) : base(height, baseWeight, depth, 'C', maxWeight)
    {
        this._loadName = loadName;
        this._temperature = temperature;
    }

    public void Load(string loadName,double load)
    {
        double temp = 0;
        foreach (var pair in _productsList)
        {
            if (pair.Key.Equals(_loadName))
            {
                temp = pair.Value;
            }
        }
        if (_loadName == loadName && _temperature >= temp)
        {
            base.Load(load);
        }
        else
        {
            Console.WriteLine("There is problem with product name or temperature!!!");
        }
    }
}