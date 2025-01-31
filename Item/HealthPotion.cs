namespace TextRPG;

public class HealthPotion : IItem
{
    public string Name { get; set; }
    
    public void Use(Warrior warrior)
    {
        throw new NotImplementedException();
    }
}