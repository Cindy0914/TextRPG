using System.Text;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class Dungeon : Singleton<Dungeon>
{
    private Warrior player;
    private StringBuilder dungeonSb = new();
    private StringBuilder playSb = new();
    private StringBuilder battleSb = new();
    private const int MinHpLoss = 20;
    private const int MaxHpLoss = 30;
    private const int printDelay = 300;

    private DungeonData EasyDungeon = new(DungeonLevel.Easy, 0.1f, 8, 1000, 100);
    private DungeonData NormalDungeon = new(DungeonLevel.Normal, 0.3f, 13, 1700, 200);
    private DungeonData HardDungeon = new(DungeonLevel.Hard, 0.5f, 20, 2500, 300);

    public override void Init()
    {
        player = GameManager.Instance.Player!;

        dungeonSb.AppendLine();
        dungeonSb.AppendLine(" == 던전 ==");
        dungeonSb.AppendLine();
        dungeonSb.AppendLine($"1. 쉬운 던전 - 권장 방어력 : {EasyDungeon.RecommendedDefense}");
        dungeonSb.AppendLine($"2. 일반 던전 - 권장 방어력 : {NormalDungeon.RecommendedDefense}");
        dungeonSb.AppendLine($"3. 어려운 던전 - 권장 방어력 : {HardDungeon.RecommendedDefense}");
        dungeonSb.AppendLine();
        dungeonSb.AppendLine("0. 나가기");
        dungeonSb.AppendLine();
        dungeonSb.AppendLine("어느 던전으로 입장하시겠습니까?");
    }

    public void Run()
    {
        Console.Clear();
        string message = "[System] 던전에 입장합니다.";
        Util.PrintColorMessage(Util.system, message);
        message = $"[System] 현재 방어력 : {player.GetTotalStats().Defense} G";
        Util.PrintColorMessage(Util.system, message);

        Console.Write(dungeonSb.ToString());
        int input = Util.GetUserInput(3);
        DungeonAction(input);
    }

    private void DungeonAction(int input)
    {
        switch (input)
        {
            case 0:
                Town.Instance.Run();
                break;
            case 1:
                PlayDungeon(EasyDungeon);
                break;
            case 2:
                PlayDungeon(NormalDungeon);
                break;
            case 3:
                PlayDungeon(HardDungeon);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    // 던전 입장 시 호출되는 메서드(난이도에 따라 해당 던전 데이터를 전달)
    private void PlayDungeon(DungeonData dungeonData)
    {
        Console.Clear();
        playSb.Clear();

        playSb.AppendLine("== 던전 ==");
        playSb.AppendLine();
        playSb.AppendLine("[System] 던전에 입장했습니다.");
        playSb.AppendLine("[System] 던전 안쪽으로 이동합니다.");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] 무언가 나타났습니다!");
        PrintLineByLine(playSb);
        playSb.Clear();
        Battle(dungeonData.Level, dungeonData.LossGoldRate);

        playSb.AppendLine("[System] 다시 던전 안쪽으로 이동합니다.");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] 무언가 나타났습니다!");
        PrintLineByLine(playSb);
        playSb.Clear();
        Battle(dungeonData.Level, dungeonData.LossGoldRate);

        playSb.AppendLine("[System] 던전 깊은 곳에 도착했습니다.");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] ......");
        playSb.AppendLine("[System] 호에엥? 무언가가 나타났다!");
        PrintLineByLine(playSb);
        playSb.Clear();
        Battle(dungeonData.Level, dungeonData.LossGoldRate);

        playSb.AppendLine("[System] 바깥으로 향하는 출구를 발견했습니다.");
        playSb.AppendLine("[System] 던전을 빠져나갑니다.");
        PrintLineByLine(playSb);

        // 게임 오버가 아닌 경우 클리어 여부 확인
        CheckClearDungeon(dungeonData);
    }

    // 던전 진행 중 몬스터와 전투하는 메서드
    private void Battle(DungeonLevel level, float lossGoldRate)
    {
        var monster = DataManager.Instance.GetMonsterData(level);
        var playerStats = player.GetTotalStats();

        var rand = new Random();
        int minHpLoss = MinHpLoss - (playerStats.Defense - monster.Defense);
        int maxHpLoss = MaxHpLoss - (playerStats.Defense - monster.Defense);
        int lossHp = rand.Next(minHpLoss, maxHpLoss + 1);

        battleSb.Clear();
        Util.PrintColorMessage(Util.system, $"[System] Lv.{monster.Level} {monster.Name}을(를) 만났습니다.");
        Thread.Sleep(printDelay);
        Util.PrintColorMessage(Util.system, $"[System] {monster.Name} 공격력 : {monster.Attack}");
        Thread.Sleep(printDelay);
        Util.PrintColorMessage(Util.system, $"[System] {monster.Name} 방어력 : {monster.Defense}");
        Thread.Sleep(printDelay);
        battleSb.AppendLine("[System] 전투를 시작합니다.");
        battleSb.AppendLine("[System] 전투 중...");
        battleSb.AppendLine("[System] 전투 종료!");
        PrintLineByLine(battleSb);

        player.TakeDamage(lossHp); // 플레이어가 몬스터에게 받은 데미지만큼 체력 감소
        Util.PrintColorMessage(Util.error, $"[System] 전투 도중 체력 {lossHp}을(를) 잃었습니다.");
        Thread.Sleep(printDelay);
        Util.PrintColorMessage(Util.system, $"[System] 남은 체력 : {player.CurrentHp} / {playerStats.MaxHp}");
        Thread.Sleep(printDelay);

        if (player.IsDead)
        {
            // 플레이어가 사망한 경우 게임 오버
            GameOverInDungeon(lossGoldRate);
        }
    }

    // 게임 오버 패널티를 받으며 마을로 돌아감
    private void GameOverInDungeon(float lossGoldRate)
    {
        int lossGold = (int)(player.Gold * lossGoldRate);

        Util.PrintColorMessage(Util.error, "[System] 체력이 0이 되어 더는 움직일 수 없습니다..");
        Thread.Sleep(printDelay);
        Util.PrintColorMessage(Util.error, "[System] 던전을 탈출합니다.");
        Thread.Sleep(printDelay);
        Util.PrintColorMessage(Util.error, $"[System] 급히 탈출하면서 {lossGold}G 를 잃었습니다.");
        Thread.Sleep(printDelay);

        player.Gold -= lossGold;
        player.Revive();
        ReturnToTitle();
    }

    // 던전 클리어 여부를 확인하고 보상과 특별보상 지급
    private void CheckClearDungeon(DungeonData dungeonData)
    {
        var playerStats = player.GetTotalStats();
        bool EnoughDefense = playerStats.Defense >= dungeonData.RecommendedDefense;
        int clearRate = EnoughDefense ? 100 : 60; // 권장 방어력 이상일 경우 100% 클리어 확률, 미만일 경우 60% 클리어 확률
        var rand = new Random();

        if (rand.Next(0, 101) <= clearRate)
        {
            Util.PrintColorMessage(Util.success, "[System] 무사히 던전을 클리어했습니다!");
            Thread.Sleep(printDelay);
            Util.PrintColorMessage(Util.success, $"[System] 보상으로 {dungeonData.RewardGold}G 획득했습니다.");
            Thread.Sleep(printDelay);
            Util.PrintColorMessage(Util.success, $"[System] 보상으로 {dungeonData.RewardExp} 경험치를 획득했습니다.");
            Thread.Sleep(printDelay);

            player.Gold += dungeonData.RewardGold;
            player.Exp += dungeonData.RewardExp;
            SpecialReward();
        }
        else
        {
            Util.PrintColorMessage(Util.error, "[System] 던전에서 아무런 보상을 얻지 못했습니다.");
            Thread.Sleep(printDelay);
            Util.PrintColorMessage(Util.error, "[System] 던전을 탈출합니다.");
            Thread.Sleep(printDelay);
        }

        ReturnToTitle();
        return;

        void SpecialReward()
        {
            // 특별 보상을 받을 확률은 기본 50% + 공격력 * 2
            int specialRate = 50;
            specialRate += playerStats.Attack * 2;

            if (rand.Next(0, 101) > specialRate) return;
            
            var item = DataManager.Instance.GetConsumeItem(dungeonData.Level, out var count);
            for (int i = 0; i < count; i++)
            {
                GameManager.Instance.Inventory.AddConsumeItem(item);
            }
                
            Util.PrintColorMessage(Util.success, "[System] 공격력 보너스로 추가 보상을 획득합니다!");
            Thread.Sleep(printDelay);
            Util.PrintColorMessage(Util.success, $"[System] 보상으로 {item.Name} {count}개를 획득했습니다.");
            Thread.Sleep(printDelay);
        }
    }

    // 던전에서 출력되는 메시지를 한 줄씩 출력
    private void PrintLineByLine(StringBuilder sb)
    {
        foreach (string line in sb.ToString().Split('\n'))
        {
            Console.WriteLine(line.Trim());
            Thread.Sleep(printDelay);
        }
    }

    private void ReturnToTitle()
    {
        Console.WriteLine();
        Console.WriteLine("[System] 아무 키나 누르면 마을로 돌아갑니다.");
        Console.ReadKey();
        Town.Instance.Run();
    }
}

public enum DungeonLevel
{
    Easy = 1,
    Normal,
    Hard
}

// 던전을 구성하기 위한 데이터 클래스
public class DungeonData
{
    public DungeonLevel Level;
    public float LossGoldRate;
    public int RecommendedDefense;
    public int RewardGold;
    public int RewardExp;

    public DungeonData(DungeonLevel level, float lossGoldRate, int recommendedDefense, int rewardGold,
                       int rewardExp)
    {
        Level = level;
        LossGoldRate = lossGoldRate;
        RecommendedDefense = recommendedDefense;
        RewardGold = rewardGold;
        RewardExp = rewardExp;
    }
}