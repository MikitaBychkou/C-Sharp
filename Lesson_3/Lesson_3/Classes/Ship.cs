namespace CW2;

public class Ship(double maxTotalWeightOfCargoInContainers, int maxSpeed, int maxContainerCount)
{
    public List<AbstrСontainer> Containers { get; set; } = new List<AbstrСontainer>();
    public double MaxSpeed { get; set; } = maxSpeed;
    public int MaxContainerCount { get; set; } = maxContainerCount;
    public double MaxTotalWeightOfCargoInContainers { get; set; } = maxTotalWeightOfCargoInContainers;
    
    public void AddContainer(AbstrСontainer container)
    {
        if (Containers.Count >= MaxContainerCount ||
            GetTotalWeightOfCargoWithContainers() > MaxTotalWeightOfCargoInContainers)
        {
            throw new OverfillException("Cannot add container");
        }
        Containers.Add(container);
    }
    
    public void LoadContainers(List<AbstrСontainer> containersToLoad)
    {
        foreach (var container in containersToLoad)
        {
            try
            {
                AddContainer(container);
            }
            catch (OverfillException e)
            {
                Console.WriteLine("Failed loading container");
            }
        }
    }
    
    public void TransferContainerToAnOtherShip(Ship anotherShip, string serialNumber)
    {
        var containerToTransfer = Containers.FirstOrDefault(container => container.SerialNumber == serialNumber);
        Console.WriteLine(containerToTransfer);
        if (containerToTransfer != null)
        {
            DeleteContainer(serialNumber);
            anotherShip.AddContainer(containerToTransfer);
        }
        else
        {
            throw new ArgumentException("Failed transfering container");
        }
    }

    public void DeleteContainer(string serialNumber)
    {
        var containerToDelete = Containers.FirstOrDefault(container => container.SerialNumber == serialNumber);
        if (containerToDelete != null)
        {
            Containers.Remove(containerToDelete);
        }
        else
        {
            throw new ArgumentException("Container not found");
        }
    }

    public double GetTotalWeightOfCargoWithContainers()
    {
        return Containers.Sum(contain => contain.ContainerWeight + contain.CargoWeight);
    }
    
    public override string ToString()
    {
        return $"[{GetType().Name}] -- MaxSpeed:{MaxSpeed}, MaxContainerCount: {MaxContainerCount}, MaxTotalWeightOfCargoInContainers: {MaxTotalWeightOfCargoInContainers},  Number of containers in the ship: {Containers.Count}";
    }
}