namespace CW2;

public abstract class AbstrСontainer(int height, int containerWeight, int depth, int maxLoadWeight)
{
    
    public abstract string SerialNumber { get; set; }
    public static int nextId = 0;
    public double CargoWeight { get; set; } 
    public int Height { get; set; } = height;
    public int ContainerWeight { get; set; } = containerWeight;
    public int Depth { get; set; } = depth;
    
    public int MaxLaodWeight { get; set; } = maxLoadWeight;

    public abstract void CargoUnloading();

    public abstract void CargoLoading(double cargoWeight);

    public override string ToString()
    {
        return $"[{GetType().Name}] -- CargoWeight:{CargoWeight}, MaxLoadWeight: {MaxLaodWeight}, ContainerWeight: {ContainerWeight}, Depth: {Depth}, Height: {Height}";
    }
}