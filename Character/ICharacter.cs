namespace TextRPG;

public interface ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }

    public void TakeDamage(int damage);
}