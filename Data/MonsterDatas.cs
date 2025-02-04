using Newtonsoft.Json;

namespace TextRPG.Data;

public class MonsterDatas : IData<MonsterData>
{
    public Dictionary<int, MonsterData>? Dict { get; } = new();

    public void LoadData(string folderPath)
    {
        string className = GetType().Name;
        string jsonFilePath = Path.Combine(folderPath, $"{className}.json");
        List<MonsterData>? monsters = null;

        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine($"JSON 파일을 찾을 수 없습니다. : {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        monsters = JsonConvert.DeserializeObject<List<MonsterData>>(json);

        if (monsters == null)
        {
            Console.WriteLine("데이터 로드 실패");
            return;
        }

        foreach (var monster in monsters)
        {
            Dict.Add(monster.Id, monster);
        }
        Console.WriteLine($"{className} 데이터 로드 완료! 데이터 개수 : {Dict.Count}");
    }
}

public class MonsterData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
}