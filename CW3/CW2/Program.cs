namespace CW2;

public class Test
{
    public static void Main(string[] args)
    {
        AbstrСontainer container = new ContainerL( 50, 399, 34, 7900, true);
        // container.CargoLoading(2000);
        // Console.WriteLine(container.CargoWeight);
        // container.CargoUnloading();
        // Console.WriteLine(container.CargoWeight);

        ContainerG containerG1 = new ContainerG(56, 56, 56, 100, 45);
        ContainerG containerG = new ContainerG(56, 56, 56, 100, 45);
        Console.WriteLine(containerG.CargoWeight);
        containerG.CargoLoading(100);
        Console.WriteLine(containerG.CargoWeight);
        containerG.CargoUnloading();
        Console.WriteLine(containerG.CargoWeight);
        Console.WriteLine(containerG);

        Ship ship = new Ship(12222222, 70, 5);
        ship.AddContainer(containerG);
        Console.WriteLine(ship);
        // ship.DeleteContainer("KON-2-G");
        Console.WriteLine(ship);

        Ship ship1 = new Ship(12222, 70, 34);
        ship.TransferContainerToAnOtherShip(ship1,"KON-2-G");
        Console.WriteLine(ship1);

    }
}

