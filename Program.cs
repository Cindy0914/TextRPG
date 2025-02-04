using System.Security.Cryptography;
using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG;

public class Program
{
    private static void Main(string[] args)
    {
        GameManager.Instance.OnGameInit += CsvToJsonConverter.ConvertAllCsvInFolder;
        GameManager.Instance.OnGameInit += DataManager.Instance.Init;
        GameManager.Instance.OnGameInit += Town.Instance.Init;
        GameManager.Instance.OnGameInit += Shop.Instance.Init;
        GameManager.Instance.OnGameInit += Dungeon.Instance.Init;
        GameManager.Instance.OnGameInit += HealingHouse.Instance.Init;
        
        Title.Instance.Init();
        Title.Instance.Run();
    }
}