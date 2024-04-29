using CW2.Interfaces;

namespace CW2;

public class ContainerL(int height, int containerWeight, int depth, 
    int maxLoadWeight, bool isDangerousCargo ) : AbstrСontainer(height, containerWeight, depth, maxLoadWeight), IHazardNotifier
{
    
    static private char sortOfContainer='L';
    public override string SerialNumber { get; set; }="KON"+$"-{nextId++}-{sortOfContainer}";
    public bool IsDangerousCargo { get; set; } = isDangerousCargo;

    public override void CargoUnloading()
    {
        CargoWeight = 0;
    }

    public override void CargoLoading(double cargoWeight)
    {
        if (IsDangerousCargo)
        {
            if (cargoWeight > MaxLaodWeight * 0.5)
            {
                sentDangerousNotification("Dangerous situation: too high loading weight of container number "+$"{SerialNumber}");
            }
        }
        else
        {
            if (cargoWeight > MaxLaodWeight * 0.9)
            {
                sentDangerousNotification("too high loading weight");
            } 
        }
        
        if (cargoWeight > MaxLaodWeight)
        {
            throw new OverfillException("too high loading weight");
        }
        
        CargoWeight = cargoWeight;
    }

    public void sentDangerousNotification(string msg)
    {
        Console.WriteLine($"Dangerous situation with container({SerialNumber}) : {msg}" );
    }

    public override string ToString()
    {
        return $"SerialNumber: {SerialNumber}, {base.ToString()}, IsDangerousCargo: {IsDangerousCargo}";;
    }
}