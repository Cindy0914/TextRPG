using TextRPG.Data;

namespace TextRPG.Manager;

public class DataManager
{
    public static MonsterDatas MonsterDatas { get; } = new();

    private const string folerPath = "Data/JSON";
    
    public static void Init()
    {
        MonsterDatas.LoadData(folerPath);
    }
}