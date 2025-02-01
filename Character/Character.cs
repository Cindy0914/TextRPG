namespace TextRPG;

public abstract class Character
{
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public int Health { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public bool IsDead { get; protected set; }

    public Character(string name)
    {
        Name = name;
    }
    
    public abstract void TakeDamage(int damage);
}