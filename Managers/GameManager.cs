using TextRPG.Utils;
using TextRPG.Item;

namespace TextRPG.Manager;

public static class GameManager
{
    public static event Action? OnGameInit;
    
    public static Warrior? Player { get; private set; }
    public static Inventory Inventory { get; } = new();
    
    public static void Init(Warrior player)
    {
        Player = player;
        Inventory.Init();
        
        OnGameInit?.Invoke();
    }
    
    public static bool TryPurchaseItem(int price)
    {
        int gold = GetGold();
        if (gold < price)
        {
            string message = "[System] 골드가 부족합니다.";
            Util.PrintColorMessage(Util.system, message);
            return false;
        }

        Player.RemoveGold(price);
        return true;
    }

    public static void SellItem(int price)
    {
        Player.AddGold(price);;
    }
    
    public static int GetGold()
    {
        return Player!.Gold;
    }
}

