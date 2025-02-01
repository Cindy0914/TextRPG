using System.Text;
using TextRPG.Manager;
using TextRPG.Stage;

namespace TextRPG;

public class Program
{
    private static GameManager? gameManager;
    private static Title title = new();
    
    private static void Main(string[] args)
    {
        Warrior warrior = SetCharacter();
        gameManager = new GameManager(warrior);
        title.Run();
    }

    private static Warrior SetCharacter()
    {
        StringBuilder sb = new();
        sb.AppendLine("TextRPG 게임에 어서오세요!");
        sb.AppendLine("용사님, 당신의 이름을 입력해주세요.");
        sb.Append(">> : ");
        Console.Write(sb.ToString());

        string? playerName = Console.ReadLine();
        while (string.IsNullOrEmpty(playerName))
        {
            Utils.PrintColorMessage(ConsoleColor.Red, "[ERROR] 입력된 이름이 없어요! 다시 입력해주세요.");
            Thread.Sleep(1000);
            
            Console.Clear();
            Console.Write(sb.ToString());
            playerName = Console.ReadLine();
        }

        Warrior warrior = new(playerName);
        
        Console.WriteLine($"\n용사님의 이름은 {playerName}이시군요!");
        Console.WriteLine("마을로 이동할게요.");
        Thread.Sleep(1000);
        Console.Clear();

        return warrior;
    }
}