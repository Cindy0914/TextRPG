using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class Title : Singleton<Title>
{
    private StringBuilder titleSb = new();
    
    public override void Init()
    {
        titleSb.AppendLine("Text RPG");
        titleSb.AppendLine();
        titleSb.AppendLine("1. 새 게임");
        titleSb.AppendLine("2. 이어하기");
        titleSb.AppendLine("0. 게임 종료");
        titleSb.AppendLine();
        titleSb.AppendLine("어떤 행동을 하시겠습니까?");
    }
    
    public void Run()
    {
        Console.Clear();
        Console.Write(titleSb.ToString());
        int input = Util.GetUserInput(2);
        TitleAction(input);
    }
    
    private void TitleAction(int input)
    {
        switch (input)
        {
            case 0: Exit();
                break;
            case 1: NewGame();
                break;
            case 2: Continue();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }
    
    private void NewGame()
    {
        Warrior player = SetCharacter();
        GameManager.Instance.SetPlayer(player);
        GameManager.Instance.Init();
        
        Thread.Sleep(200);
        Town.Instance.Run();
    }
    
    private void Continue()
    {
        bool successLoad = SaveStage.Instance.TryLoad();

        if (successLoad) return;
        
        string message = "[System] 타이틀 화면으로 돌아갑니다.";
        Util.PrintColorMessage(Util.system, message);
        Thread.Sleep(1000);
        Run();
    }
    
    private void Exit()
    {
        Environment.Exit(0);
    }
    
    private Warrior SetCharacter()
    {
        Console.Clear();
        
        StringBuilder sb = new();
        sb.AppendLine("TextRPG 게임에 어서오세요!");
        sb.AppendLine("용사님, 당신의 이름을 입력해주세요.");
        sb.Append(">> ");
        Console.Write(sb.ToString());

        string? playerName = Console.ReadLine();
        while (string.IsNullOrEmpty(playerName))
        {
            string wrongInputMessage = "[ERROR] 잘못된 입력입니다. 다시 입력해주세요.";
            Util.PrintColorMessage(Util.error, wrongInputMessage, true);
            Thread.Sleep(1000);
            
            Console.Clear();
            Console.Write(sb.ToString());
            playerName = Console.ReadLine();
        }

        CharacterStats stats = new(100, 10, 5);
        Warrior warrior = new(playerName, stats);
        
        Console.WriteLine($"\n용사님의 이름은 {playerName}이시군요!");
        Console.WriteLine("마을로 이동할게요.");
        Thread.Sleep(1000);
        Console.Clear();

        return warrior;
    }
}