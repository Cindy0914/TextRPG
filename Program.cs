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
        // Warrior warrior = SetCharacter();
        
        // test
        Warrior warrior = new Warrior("테스트용사", new CharacterStats(100, 10, 5));
        GameManager.Init(warrior);

        var item1 = DataManager.ConsumeItemDatas.Dict[3000];
        GameManager.Inventory.GetConsumeItem(item1);
        
        Thread.Sleep(500);
        Title.Run();
    }

    private static Warrior SetCharacter()
    {
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