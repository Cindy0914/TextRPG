namespace TextRPG;

public class Warrior : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }
    
    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}