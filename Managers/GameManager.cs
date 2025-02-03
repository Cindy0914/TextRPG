using System.Text;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG.Manager;

public static class GameManager
{
    public static Warrior? Player { get; private set; }

    public static void Init(Warrior player)
    {
        CsvToJsonConverter.ConvertAllCsvInFolder();
        DataManager.Init();
        Player = player;
    }
}

