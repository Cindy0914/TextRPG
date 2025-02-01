using System.Text;
using TextRPG.Manager;

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
        Console.Write(titleSb.ToString());
        int input = Utils.GetUserInput(menuCount);
    }
}

public enum TitleAction
{
    Status,
    Inventory,
    Shop,
    Dungeon,
    Rest
}
