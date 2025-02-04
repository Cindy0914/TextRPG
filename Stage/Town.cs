using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class Town : Singleton<Town>
{
    private readonly StringBuilder townSb = new();

    public override void Init()
    {
        townSb.AppendLine("스파르타 마을에 오신 여러분 환영합니다!");
        townSb.AppendLine("던전에 들어가기 전 준비를 하고, 던전에서 몬스터를 사냥하며 경험치를 얻어 레벨업을 할 수 있습니다.");

        townSb.AppendLine();
        townSb.AppendLine("1. 상태 보기");
        townSb.AppendLine("2. 인벤토리");
        townSb.AppendLine("3. 상점");
        townSb.AppendLine("4. 던전 입장");
        townSb.AppendLine("5. 휴식하기");
        townSb.AppendLine("6. 저장하기");
        townSb.AppendLine("0. 게임 종료");

        townSb.AppendLine();
        townSb.AppendLine("어떤 행동을 하시겠습니까?");
    }

    public void Run()
    {
        Console.Clear();
        Console.Write(townSb.ToString());
        
        
        int input = Util.GetUserInput(6);
        TitleAction((TitleActionEnum)input);
    }

    private void TitleAction(TitleActionEnum action)
    {
        switch (action)
        {
            case TitleActionEnum.Status: Status();
                break;
            case TitleActionEnum.Inventory: Inventory();
                break;
            case TitleActionEnum.Shop: EnterShop();
                break;
            case TitleActionEnum.Dungeon: EnterDungeon();
                break;
            case TitleActionEnum.Rest: Rest();
                break;
            case TitleActionEnum.Save: Save();
                break;
            case TitleActionEnum.Exit: Exit();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
    
    private void Status()
    {
        Console.Clear();
        
        Warrior Player = GameManager.Instance.Player!;
        const string message = ("[System] 현재 상태를 확인합니다.");
        Util.PrintColorMessage(Util.system, message);
        
        StringBuilder statusSb = new();
        statusSb.AppendLine();
        statusSb.AppendLine("  == 상태 ==");
        statusSb.AppendLine();
        statusSb.Append(Player.ShowStats());
        statusSb.AppendLine();
        statusSb.AppendLine("0. 나가기");
        statusSb.AppendLine();
        statusSb.AppendLine("어떤 행동을 하시겠습니까?");
        
        Console.Write(statusSb.ToString());
        
        Util.GetUserInput(0);
        Run();
    }
    
    private void Inventory()
    {
        GameManager.Instance.Inventory.ShowInventory();
    }
    
    private void EnterShop()
    {
        Shop.Instance.Run();
    }
    
    private static void EnterDungeon()
    {
        Dungeon.Instance.Run();
    }
    
    private static void Rest()
    {
        HealingHouse.Instance.Run();
    }
    
    private void Save()
    {
        DataManager.Instance.SaveData();
        string message = "[System] 게임을 저장했습니다!";
        Util.PrintColorMessage(Util.success, message, false, true);
        Thread.Sleep(1000);
        Run();
    }
    
    private void Exit()
    {
        Environment.Exit(0);
    }
}

public enum TitleActionEnum
{
    Exit,
    Status,
    Inventory,
    Shop,
    Dungeon,
    Rest,
    Save,
    Event
}
