using APBD03.Models.Bases;

namespace APBD03.Models.Containers;

public class ContainerShip
{
    public List<ContainerBase> ContainersList { get; set; }
    public double Speed { get; set; }
    public int MaxNumberOfConatiners { get; set; }
    public double MaxLoadOfConatiners { get; set; }
    private double Weight { get; set; }

    public ContainerShip(List<ContainerBase> containersList, double speed, int maxNumberOfConatiners, double maxLoadOfConatiners)
    {
        ContainersList = containersList;
        Speed = speed;
        MaxNumberOfConatiners = maxNumberOfConatiners;
        MaxLoadOfConatiners = maxLoadOfConatiners;
        Weight = 0;
    }
    
    public ContainerShip(double speed, int maxNumberOfConatiners, double maxLoadOfConatiners) : this(new List<ContainerBase>(), speed, maxNumberOfConatiners, maxLoadOfConatiners)
    {
    }

    public void Add(ContainerBase cb)
    {
        if (Weight + cb.Weight <= MaxLoadOfConatiners && ContainersList.Count + 1 <= MaxNumberOfConatiners)
        {
            Weight = +cb.Weight;
            ContainersList.Add(cb);
        }
    }
    
    public void AddList(List<ContainerBase> cbList)
    {
        foreach (var cb in cbList)
        {
            Add(cb);
        }
    }
    
    public void Remove(ContainerBase cb)
    {
        if (ContainersList.Contains(cb))
        {
            Weight = -cb.Weight;
            ContainersList.Remove(cb);
        }
    }
    
    public void Replace(string sn, ContainerShip cs)
    {
        foreach (var cb in ContainersList)
        {
            if (cb.SerialNumber.Equals(sn))
            {
                ContainersList.Remove(cb);
                cs.Add(cb);
            }
        }
    }

    public override string ToString()
    {
        return "Container ship: speed -> " + Speed + " max number of containers -> " + MaxNumberOfConatiners +
               " max load -> " + MaxLoadOfConatiners;
    }

    public void Info()
    {
        Console.WriteLine(this);
        foreach (var cb in ContainersList)
        {
            Console.WriteLine(cb);
        }
    }
}