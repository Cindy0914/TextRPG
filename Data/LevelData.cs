namespace TextRPG.Data;

public class LevelData
{
    private int[] expTable = new int[]
    {
        0, 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600
    };

    public bool IsMaxExp(int level, int exp)
    {
        return exp >= expTable[level];
    }
    
    public void LevelUp(CharacterStats baseStats)
    {
        baseStats.MaxHp += 10;
        baseStats.Attack += 1;
        baseStats.Defense += 1;
    }
}