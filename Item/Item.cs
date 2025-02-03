namespace TextRPG;

public abstract class ConsumeItem
{
    public string Name { get; set; }

    public abstract void Use(Warrior warrior);
}