using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public static class Shop
{
    private static readonly Dictionary<int, bool> purchased = new();
    private static readonly List<Equipment> equipments = new();
    private static readonly StringBuilder shopSb = new();
    private static readonly StringBuilder itemSb = new();

    public static void Init()
    {
        shopSb.AppendLine();
        shopSb.AppendLine(" == 상점 ==");
        shopSb.AppendLine();
        shopSb.AppendLine("1. 장비 구매");
        shopSb.AppendLine("2. 장비 판매");
        shopSb.AppendLine("0. 나가기");
        shopSb.AppendLine();
        shopSb.AppendLine("어떤 행동을 하시겠습니까?");
        
        var buyEquipments = DataManager.GetBuyEquipments();
        equipments.AddRange(buyEquipments);
        foreach (var equip in buyEquipments)
        {
            purchased.Add(equip.Id, false);
        }
    }

    public static void Run()
    {
        const string message = "[System] 아이템을 구입하거나 판매합니다.";
     
        Console.Clear();
        Util.PrintColorMessage(Util.system, message);
        Console.Write(shopSb.ToString());
        
        int input = Util.GetUserInput(3, true);
        ShopAction(input);
    }
    
    private static void ShopAction(int input)
    {
        switch (input)
        {
            case 0: Title.Run();
                break;
            case 1: BuyEquipments();
                break;
            case 2: SellEquipments();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private static void BuyEquipments()
    {
        string message = "[System] 아이템을 구입합니다.";

        while (true)
        {
            Console.Clear();
            Util.PrintColorMessage(Util.system, message);
            PrintPlayerGold();
            
            itemSb.Clear();

            itemSb.AppendLine(" == 상점_아이템 목록 ==");
            itemSb.AppendLine();


            for (int i = 0; i < equipments.Count; i++)
            {
                var equip = equipments[i];
                bool isPurchase = purchased[equip.Id];
                PrintIsPurchased(i, isPurchase, equip);
            }

            itemSb.AppendLine();
            itemSb.AppendLine("0. 나가기");
            itemSb.AppendLine();
            itemSb.AppendLine("어떤 아이템을 구매하시겠습니까?");
            Console.Write(itemSb.ToString());
            int input = Util.GetUserInput(equipments.Count, true);
            
            if (input == 0)
            {
                Run();
                break;
            }
            
            var purchaseEquip = equipments[input - 1];
            PurchaseEquip(purchaseEquip);
        }

        return;

        void PrintIsPurchased(int index, bool isPurchased, Equipment equip)
        {
            string message = " [Sold Out]";
            if (isPurchased)
            {
                Console.Write(itemSb.ToString());
                itemSb.Clear();
                
                itemSb.Append($"{index + 1}. {equip.Name}");
                Console.Write(itemSb.ToString());
                itemSb.Clear();
                
                Util.PrintColorMessage(Util.error, message);
            }
            else
            {
                itemSb.AppendLine($"{index + 1}. {equip.Name} +({equip.Stat.GetName()}{equip.Value}) | {equip.Price} G | {equip.Desc}");
            }
        }
    }

    private static void SellEquipments()
    {
        // 인벤토리 아이템 목록 표시
        string message = "[System] 아이템을 판매합니다.";
        
        while (true)
        {
            Console.Clear();
            Util.PrintColorMessage(Util.system, message);
            PrintPlayerGold();
            
            itemSb.Clear();

            itemSb.AppendLine(" == 상점_판매 가능 목록 ==");
            itemSb.AppendLine();

            var playerEquipments = GameManager.Inventory.GetPlayerEquipments();
            for (int i = 0; i < playerEquipments.Count; i++)
            {
                var equip = playerEquipments[i];
                itemSb.AppendLine($"{i + 1}. {equip.Name} +({equip.Stat.GetName()}{equip.Value}) | {equip.SellPrice} G | {equip.Desc}");
            }

            itemSb.AppendLine();
            itemSb.AppendLine("0. 나가기");
            itemSb.AppendLine();
            itemSb.AppendLine("어떤 아이템을 판매하시겠습니까?");
            Console.Write(itemSb.ToString());
            int input = Util.GetUserInput(playerEquipments.Count, true);
            
            if (input == 0)
            {
                Run();
                break;
            }
            
            var sellEquip = playerEquipments[input - 1];
            SellEquip(sellEquip);
        }
    }

    private static void PrintPlayerGold()
    {
        var gold = GameManager.GetGold();
        string message = $"[System] 현재 소지금 : {gold} Gold\n";
        Util.PrintColorMessage(Util.system, message);
    }

    private static void PurchaseEquip(Equipment equip)
    {
        if (GameManager.TryPurchaseItem(equip.Price))
        {
            purchased.Remove(equip.Id);
            purchased.Add(equip.Id, true);
            GameManager.Inventory.AddEquipment(equip);
        }
        else
        {
            Thread.Sleep(1000);
        }
    }
    
    private static void SellEquip(Equipment equip)
    {
        GameManager.SellItem(equip.SellPrice);
        GameManager.Inventory.RemoveEquipment(equip);
        
        if (equip.IsEquipped)
        {
            equip.UnEquip(GameManager.Player!.Stats);
        }
        
        purchased.Remove(equip.Id);
        purchased.Add(equip.Id, false);
    }
}