namespace TextRPG;

public class Monster : Character
{
    public Monster(string name, CharacterStats stats) : base(name, stats)
    {
        // TODO: Set monster stats
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}