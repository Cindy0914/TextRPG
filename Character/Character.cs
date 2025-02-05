namespace TextRPG;

// Player와 Monster의 부모 클래스로 사용하려 했으나 던전 구현 중 몬스터에게 필요없는 기능이 많아짐
// 추후 Player 직업 종류가 많아지면 사용할 수 있을 것 같아서 남겨둠
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