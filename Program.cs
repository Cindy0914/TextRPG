using TextRPG.Manager;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG;

public class Program
{
    private static void Main(string[] args)
    {
        // 필요한 Manager들과 Stage들을 초기화
        GameManager.Instance.OnGameInit += CsvToJsonConverter.ConvertAllCsvInFolder;
        GameManager.Instance.OnGameInit += SaveStage.Instance.Init;
        GameManager.Instance.OnGameInit += Town.Instance.Init;
        GameManager.Instance.OnGameInit += Shop.Instance.Init;
        GameManager.Instance.OnGameInit += Dungeon.Instance.Init;
        GameManager.Instance.OnGameInit += HealingHouse.Instance.Init;
        
        // Title 화면에서 시작
        DataManager.Instance.Init();
        Title.Instance.Init();
        Title.Instance.Run();
    }

    // 게임 종료시 호출
    public static void Exit()
    {
        GameManager.Instance.OnGameInit -= CsvToJsonConverter.ConvertAllCsvInFolder;
        GameManager.Instance.OnGameInit -= SaveStage.Instance.Init;
        GameManager.Instance.OnGameInit -= Town.Instance.Init;
        GameManager.Instance.OnGameInit -= Shop.Instance.Init;
        GameManager.Instance.OnGameInit -= Dungeon.Instance.Init;
        GameManager.Instance.OnGameInit -= HealingHouse.Instance.Init;
        
        Environment.Exit(0);
    }
}