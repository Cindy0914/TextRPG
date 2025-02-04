using Newtonsoft.Json;
using TextRPG.Data;
using TextRPG.Stage;

namespace TextRPG.Manager;

public class DataManager : Singleton<DataManager>
{
    public MonsterDatas MonsterDatas { get; } = new();
    public EquipmentDatas EquipmentDatas { get; } = new();
    public ConsumeItemDatas ConsumeItemDatas { get; } = new();
    private const string folerPath = "Data/JSON";
    
    public override void Init()
    {
        MonsterDatas.LoadData(folerPath);
        EquipmentDatas.LoadData(folerPath);
        ConsumeItemDatas.LoadData(folerPath);
    }

    // 9000 번대 아이템은 특수 아이템으로 판단
    public List<Equipment> GetBuyEquipments()
    {
        List<Equipment> buyEquipments = new();
        foreach (var pair in EquipmentDatas.Dict)
        {
            buyEquipments.Add(pair.Value);
        }
        
        return buyEquipments;
    }

    public MonsterData GetMonsterData(DungeonLevel level)
    {
        Random random = new();
        var monsters = Monsters((int)level);
        int rand = random.Next(0, monsters.Count);
        return monsters[rand];
        
        List<MonsterData> Monsters(int index)
        {
            List<MonsterData> datas = new();
            foreach (var pair in MonsterDatas.Dict!)
            {
                if (pair.Key / 1000 == index)
                {
                    datas.Add(pair.Value);
                }
            }

            return datas;
        }
    }
    
    public ConsumeItem GetConsumeItem(DungeonLevel level, out int count)
    {
        ConsumeItem consumeItem;
        count = 0;
        
        switch (level)
        {
            case DungeonLevel.Easy:
                count = 2;
                return consumeItem = ConsumeItemDatas.Dict![3000];
            case DungeonLevel.Normal:
                count = 1;
                return consumeItem = ConsumeItemDatas.Dict![3001];
            case DungeonLevel.Hard:
                count = 1;
                return consumeItem = ConsumeItemDatas.Dict![3002];
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
    }

    public void SaveData()
    {
        var player = GameManager.Instance.Player;
        SaveData save = new(player.Name, player.Level, player.Exp, player.Gold, player.CurrentHp);
        save.Save();
        
        string json = JsonConvert.SerializeObject(save);
        string filePath = Path.Combine(folerPath, "SaveData.json");
        File.WriteAllText(filePath, json);
    }
    
    public SaveData? LoadData()
    {
        string filePath = Path.Combine(folerPath, "SaveData.json");
        if (!File.Exists(filePath))
        {
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }
}