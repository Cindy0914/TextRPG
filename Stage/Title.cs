using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class Title
{
    private static readonly StringBuilder titleSb = new();
    private const int menuCount = 5;

    public Title()
    {
        titleSb.AppendLine("스파르타 마을에 오신 여러분 환영합니다!");
        titleSb.AppendLine("던전에 들어가기 전 준비를 하고, 던전에서 몬스터를 사냥하며 경험치를 얻어 레벨업을 할 수 있습니다.");

        titleSb.AppendLine();
        titleSb.AppendLine("1. 상태 보기");
        titleSb.AppendLine("2. 인벤토리");
        titleSb.AppendLine("3. 상점");
        titleSb.AppendLine("4. 던전 입장");
        titleSb.AppendLine("5. 휴식하기");

        titleSb.AppendLine();
        titleSb.AppendLine("어떤 행동을 하시겠습니까?");
    }

    public void Run()
    {
        Console.Clear();
        Console.Write(titleSb.ToString());
        int input = Util.GetUserInput(menuCount);
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
            case TitleActionEnum.Shop: Shop();
                break;
            case TitleActionEnum.Dungeon: Dungeon();
                break;
            case TitleActionEnum.Rest: Rest();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
    
    private void Status()
    {
        Warrior Player = GameManager.Player!;
        const string message = ("[System] 현재 상태를 확인합니다.");
        Util.PrintColorMessage(Util.system, message);
        
        StringBuilder statusSb = new();
        statusSb.AppendLine();
        statusSb.AppendLine($" {Player.Name} (전사)");
        statusSb.AppendLine($" Lv.{Player.Level}");
        
        statusSb.Append($" HP : {Player.Stats.CurrentHp} / {Player.Stats.MaxHp}");
        if (Player.EnhancedStats.MaxHp > 0) statusSb.Append($" +({Player.EnhancedStats.MaxHp})");
        statusSb.AppendLine();
        
        statusSb.Append($" Atk : {Player.Stats.Attack}");
        if (Player.EnhancedStats.Attack > 0) statusSb.Append($" +({Player.EnhancedStats.Attack})");
        statusSb.AppendLine();
        
        statusSb.Append($" Dfs : {Player.Stats.Defense}");
        if (Player.EnhancedStats.Defense > 0) statusSb.Append($" +({Player.EnhancedStats.Defense})");
        statusSb.AppendLine();
        
        statusSb.AppendLine($" Gold : {Player.Gold} G");
        statusSb.AppendLine();
        statusSb.AppendLine("1. 뒤로가기");
        
        Console.Clear();
        Console.Write(statusSb.ToString());
        
        Util.GetUserInput(1);
        Run();
    }
    
    private void Inventory()
    {
        
    }
    
    private void Shop()
    {
        
    }
    
    private void Dungeon()
    {
        
    }
    
    private void Rest()
    {
        
    }
}

public enum TitleActionEnum
{
    Status = 1,
    Inventory,
    Shop,
    Dungeon,
    Rest
}
