using System.Text;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG.Manager;

public class GameManager(Warrior warrior)
{
    public Warrior? Player { get; private set; } = warrior;

    public void Init()
    {
        CsvToJsonConverter.ConvertAllCsvInFolder();
        DataManager.Init();

        if (DataManager.MonsterDatas.Monsters != null)
        {
            string test = DataManager.MonsterDatas.Monsters[0].Name;
            Console.Write(test);
        }
    }
}

