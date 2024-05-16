using APBD03.Models.Containers;

GasContainer c1 = new GasContainer(100,150.0,20,200.0,150);
LiquidContainer c2 = new LiquidContainer(40, 50, 60, 250, true);
ColdContainer c3 = new ColdContainer(40, 50, 60, 250, "Bananas",13.2);

ContainerShip containerShip = new ContainerShip(150,4,500);

Console.WriteLine(c1);

c1.Load(151);

Console.WriteLine(c1);

c1.Unload();

Console.WriteLine(c1);
Console.WriteLine(c2);
Console.WriteLine(c3);