namespace TextRPG;

public abstract class Character
{
    public CharacterStats Stats { get; protected set; }
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public bool IsDead { get; protected set; }

    protected Character(string name, CharacterStats stats)
    {
        Name = name;
        Stats = stats;
    }
}

public class CharacterStats
{
    public int MaxHp { get; set; } = 0;
    public int Attack { get; set; } = 0;
    public int Defense { get; set; } = 0;

    public CharacterStats(int maxHp, int attack, int defense)
    {
        MaxHp = maxHp;
        Attack = attack;
        Defense = defense;
    }
}

public enum StatType
{
    MaxHp,
    Attack,
    Defense
}