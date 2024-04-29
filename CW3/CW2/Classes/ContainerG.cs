using CW2.Interfaces;

namespace CW2;

public class ContainerG(int height, int containerWeight, int depth, 
    int maxLoadWeight, int pressure ) : AbstrСontainer(height, containerWeight, depth, maxLoadWeight), IHazardNotifier
{
    static private char sortOfContainer='G';
    
    public override string SerialNumber { get; set; }="KON"+$"-{nextId++}-{sortOfContainer}";
    public int Pressure { get; set; } = pressure;

    public override void CargoUnloading()
    {
        CargoWeight*= 0.05;
    }

    public override void CargoLoading(double cargoWeight)
    {
        if (cargoWeight > MaxLaodWeight)
        {
            sentDangerousNotification("too high loading weight");
            throw new OverfillException("The weight of the load is greater than the maximum loading weight");
        }
        
        CargoWeight = cargoWeight;
    }
    
    public override string ToString()
    {
        return $"SerialNumber: {SerialNumber}, {base.ToString()}, Pressure: {Pressure}";;
    }

    public void sentDangerousNotification(string msg)
    {
        Console.WriteLine($"Dangerous situation with container({SerialNumber}) : {msg}" );
    }
}