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

    // 9000 번대 아이템은 특수 아이템으로 판단
    public static List<Equipment> GetBuyEquipments()
    {
        List<Equipment> buyEquipments = new();
        foreach (var pair in EquipmentDatas.Dict)
        {
            if (pair.Key > 9000) continue;
            buyEquipments.Add(pair.Value);
        }
        
        return buyEquipments;
    }
}