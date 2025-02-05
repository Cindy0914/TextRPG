using Newtonsoft.Json;

namespace TextRPG.Data;

public class EquipmentDatas : IData<Equipment>
{
    public Dictionary<int, Equipment>? Dict { get; } = new();
    
    public void LoadData(string folderPath)
    {
        string className = GetType().Name;
        string jsonFilePath = Path.Combine(folderPath, $"{className}.json");
        List<Equipment>? equipments = null;

        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine($"JSON 파일을 찾을 수 없습니다. : {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        equipments = JsonConvert.DeserializeObject<List<Equipment>>(json);

        if (equipments == null)
        {
            Console.WriteLine("데이터 로드 실패");
            return;
        }

        foreach (var equipment in equipments)
        {
            Dict.Add(equipment.Id, equipment);
        }
        Console.WriteLine($"{className} 데이터 로드 완료! 데이터 개수 : {Dict.Count}");
    }
}

public class Equipment
{
    public EquipmentSlot Slot;
    public StatType Stat;
    public string Name;
    public string Desc;
    public int Id;
    public int Value;
    public int Price;
    public bool IsEquipped;

    public int SellPrice => Price / 2;
    
    public void Equip(CharacterStats enhancedStats)
    {
        IsEquipped = true;

        switch (Stat)
        {
            case StatType.MaxHp:
                enhancedStats.MaxHp += Value;
                break;
            case StatType.Attack:
                enhancedStats.Attack += Value;
                break;
            case StatType.Defense:
                enhancedStats.Defense += Value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void UnEquip(CharacterStats enhancedStats)
    {
        IsEquipped = false;

        switch (Stat)
        {
            case StatType.MaxHp:
                enhancedStats.MaxHp -= Value;
                break;
            case StatType.Attack:
                enhancedStats.Attack -= Value;
                break;
            case StatType.Defense:
                enhancedStats.Defense -= Value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum EquipmentSlot
{
    Weapon,
    Head,
    Armor,
    Acc,
}