using TextRPG.Data;

namespace TextRPG.Manager;

public static class DataManager
{
    public static MonsterDatas MonsterDatas { get; } = new();
    public static EquipmentDatas EquipmentDatas { get; } = new();
    public static ConsumeItemDatas ConsumeItemDatas { get; } = new();
    private const string folerPath = "Data/JSON";
    
    public static void Init()
    {
        MonsterDatas.LoadData(folerPath);
        EquipmentDatas.LoadData(folerPath);
        ConsumeItemDatas.LoadData(folerPath);
    }
}