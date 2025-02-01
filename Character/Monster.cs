namespace TextRPG;

public class Monster : Character
{
    public Monster(string name) : base(name)
    {
        // TODO: Set monster stats
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}