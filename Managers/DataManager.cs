using Newtonsoft.Json;
using TextRPG.Data;
using TextRPG.Stage;

namespace TextRPG.Manager;

public class DataManager : Singleton<DataManager>
{
    public MonsterDatas MonsterDatas { get; } = new();
    public EquipmentDatas EquipmentDatas { get; } = new();
    public ConsumeItemDatas ConsumeItemDatas { get; } = new();
    private const string FolderPath = "Data/JSON";
    private const string SavePath = "Data/SAVE";
    
    public override void Init()
    {
        MonsterDatas.LoadData(FolderPath);
        EquipmentDatas.LoadData(FolderPath);
        ConsumeItemDatas.LoadData(FolderPath);
    }

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

    public void SaveData(int slot)
    {
        var player = GameManager.Instance.Player;
        SaveData save = new(player.Name, player.Level, player.Exp, player.Gold, player.CurrentHp);
        save.Save();
        
        string json = JsonConvert.SerializeObject(save);
        string filePath = Path.Combine(SavePath, $"SaveData_0{slot}.json");
        File.WriteAllText(filePath, json);
    }
    
    public SaveData? LoadData(int slot)
    {
        string filePath = Path.Combine(SavePath, $"SaveData_0{slot}.json");
        if (!File.Exists(filePath))
        {
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }
    
    public List<string> GetSaveFiles()
    {
        string[] files = Directory.GetFiles(SavePath);
        List<string> saveFiles = new();
        foreach (var file in files)
        {
            if (file.Contains("SaveData_0"))
            {
                saveFiles.Add(file);
            }
        }

        return saveFiles;
    }
}