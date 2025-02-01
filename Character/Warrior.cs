namespace TextRPG;

public class Warrior : Character
{
    public int Gold { get; private set; }
    
    public Warrior(string name) : base(name)
    {
        Level = 1;
        Health = 100;
        Attack = 10;
        Defense = 5;
        Gold = 100;
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
        
    }
}