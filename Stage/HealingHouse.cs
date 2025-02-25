using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class HealingHouse : Singleton<HealingHouse>
{
    private StringBuilder healingSb = new();

    public override void Init()
    {
        healingSb.AppendLine();
        healingSb.AppendLine(" == 치료소 ==");
        healingSb.AppendLine();
        healingSb.AppendLine("1. 치료하기");
        healingSb.AppendLine("0. 나가기");
        healingSb.AppendLine();
        healingSb.AppendLine("어떤 행동을 하시겠습니까?");
    }

    public void Run()
    {
        Console.Clear();
        string message = "[System] 500 G를 지불하면 회복할 수 있습니다.\n" +
                         $"[System] 현재 골드 : {GameManager.Instance.Player!.Gold} G";
                
        Util.PrintColorMessage(Util.system, message);

        Console.Write(healingSb.ToString());
        int input = Util.GetUserInput(1);
        HealingHouseAction(input);
    }

    private void HealingHouseAction(int input)
    {
        switch (input)
        {
            case 0: Town.Instance.Run();
                break;
            case 1: Heal();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private void Heal()
    {
        Warrior player = GameManager.Instance.Player!;
        string message;
        const int price = 500;
        if (player.CurrentHp == player.Stats.MaxHp)
        {
            message = "[System] 이미 체력이 가득 찼습니다.";
            Util.PrintColorMessage(Util.error, message, false, true);
        }
        else if(player.Gold < price)
        {
            message = "[System] 골드가 부족합니다.";
            Util.PrintColorMessage(Util.error, message, false, true);
        }
        else
        {
            player.CurrentHp = player.Stats.MaxHp;
            player.Gold -= price;
            message = "[System] 체력을 모두 회복했습니다.";
            Util.PrintColorMessage(Util.success, message);
        }

        Thread.Sleep(1000);
        Run();
    }
}