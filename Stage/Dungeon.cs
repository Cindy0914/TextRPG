using System.Text;

namespace TextRPG.Stage;

public class Dungeon
{
    private static StringBuilder dungeonSb = new();
    
    public static void Init()
    {}
}

public enum DungeonLevel
{
    Easy,
    Normal,
    Hard
}