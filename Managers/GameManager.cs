using TextRPG.Data;
using TextRPG.Utils;
using TextRPG.Item;

namespace TextRPG.Manager;

public class GameManager : Singleton<GameManager>
{
    // 게임 시작 전 싱글턴 초기화 이벤트
    public event Action? OnGameInit;
    
    public Warrior? Player { get; private set; }
    public Inventory Inventory { get; } = new();
    public LevelData LevelData { get; } = new();
    
    public override void Init()
    {
        Inventory.Init();
        OnGameInit?.Invoke();
    }
    
    public void SetPlayer(Warrior player)
    {
        Player = player;
    }
    
    public bool TryPurchaseItem(int price)
    {
        if (Player!.Gold < price)
        {
            string message = "[System] 골드가 부족합니다.";
            Util.PrintColorMessage(Util.error, message, false, true);
            return false;
        }

        Player!.Gold -= price;
        return true;
    }

    public void SellItem(int price)
    {
        Player!.Gold += price;
    }
}

