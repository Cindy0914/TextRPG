namespace TextRPG;

public class Warrior : Character
{
    public int Gold { get; private set; }
    public CharacterStats EnhancedStats { get; }

    public Warrior(string name, CharacterStats stats) : base(name, stats)
    {
        Level = 1;
        Gold = 100;
        Stats = stats;
        EnhancedStats = new CharacterStats(0, 0, 0);
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}