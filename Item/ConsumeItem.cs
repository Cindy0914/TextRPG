using Newtonsoft.Json;
using TextRPG.Data;

namespace TextRPG;

public class ConsumeItemDatas : IData<ConsumeItem>
{
    public Dictionary<int, ConsumeItem>? Dict { get; } = new();
    
    public void LoadData(string folderPath)
    {
        string className = GetType().Name;
        string jsonFilePath = Path.Combine(folderPath, $"{className}.json");
        List<ConsumeItem>? consumeItems = null;

        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine($"JSON 파일을 찾을 수 없습니다. : {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        consumeItems = JsonConvert.DeserializeObject<List<ConsumeItem>>(json);

        if (consumeItems == null)
        {
            Console.WriteLine("데이터 로드 실패");
            return;
        }

        foreach (var consumeItem in consumeItems)
        {
            Dict.Add(consumeItem.Id, consumeItem);
        }
        Console.WriteLine($"{className} 데이터 로드 완료! 데이터 개수 : {Dict.Count}");
    }
}

public class ConsumeItem
{
    public StatType Stat;
    public string Name;
    public string Desc;
    public int Value;
    public int Price;
    public int Id;
    public int SellPrice => Price / 2;

    public string GetDescription()
    {
        return Desc.Replace("Value", Value.ToString());
    }

    public void Use(CharacterStats stats)
    {
        switch (Stat)
        {
            case StatType.MaxHp:
                stats.CurrentHp += Value;
                break;
            case StatType.Attack:
                stats.Attack += Value;
                break;
            case StatType.Defense:
                stats.Defense += Value;
                break;
        }
    }
}

