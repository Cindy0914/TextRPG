using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG.Stage;

public class Shop : Singleton<Shop>
{
    public Dictionary<int, bool> Purchased { get; } = new();
    
    private List<Equipment> equipments = new();
    private StringBuilder shopSb = new();
    private StringBuilder itemSb = new();

    public override void Init()
    {
        shopSb.AppendLine();
        shopSb.AppendLine(" == 상점 ==");
        shopSb.AppendLine();
        shopSb.AppendLine("1. 장비 구매");
        shopSb.AppendLine("2. 장비 판매");
        shopSb.AppendLine("0. 나가기");
        shopSb.AppendLine();
        shopSb.AppendLine("어떤 행동을 하시겠습니까?");
        
        var buyEquipments = DataManager.Instance.GetBuyEquipments();
        equipments.AddRange(buyEquipments);
        foreach (var equip in buyEquipments)
        {
            Purchased.Add(equip.Id, false);
        }
    }

    public void Run()
    {
        const string message = "[System] 아이템을 구입하거나 판매합니다.";
     
        Console.Clear();
        Util.PrintColorMessage(Util.system, message);
        Console.Write(shopSb.ToString());
        
        int input = Util.GetUserInput(3);
        ShopAction(input);
    }
    
    private void ShopAction(int input)
    {
        switch (input)
        {
            case 0: Town.Instance.Run();
                break;
            case 1: BuyEquipments();
                break;
            case 2: SellEquipments();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private void BuyEquipments()
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
                bool isPurchase = Purchased[equip.Id];
                PrintIsPurchased(i, isPurchase, equip);
            }

            itemSb.AppendLine();
            itemSb.AppendLine("0. 나가기");
            itemSb.AppendLine();
            itemSb.AppendLine("어떤 아이템을 구매하시겠습니까?");
            Console.Write(itemSb.ToString());
            int input = Util.GetUserInput(equipments.Count);
            
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

    private void SellEquipments()
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

            var playerEquipments = GameManager.Instance.Inventory.GetPlayerEquipments();
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
            int input = Util.GetUserInput(playerEquipments.Count);
            
            if (input == 0)
            {
                Run();
                break;
            }
            
            var sellEquip = playerEquipments[input - 1];
            SellEquip(sellEquip);
        }
    }

    private void PrintPlayerGold()
    {
        var gold = GameManager.Instance.Player!.Gold;
        string message = $"[System] 현재 소지금 : {gold} Gold\n";
        Util.PrintColorMessage(Util.system, message);
    }

    private void PurchaseEquip(Equipment equip)
    {
        if (GameManager.Instance.TryPurchaseItem(equip.Price))
        {
            Purchased.Remove(equip.Id);
            Purchased.Add(equip.Id, true);
            GameManager.Instance.Inventory.AddEquipment(equip);
        }
        else
        {
            Thread.Sleep(1000);
        }
    }
    
    private void SellEquip(Equipment equip)
    {
        GameManager.Instance.SellItem(equip.SellPrice);
        GameManager.Instance.Inventory.RemoveEquipment(equip);
        
        if (equip.IsEquipped)
        {
            equip.UnEquip(GameManager.Instance.Player!.Stats);
            GameManager.Instance.Player!.UpEquipItem(equip);
        }
        
        Purchased.Remove(equip.Id);
        Purchased.Add(equip.Id, false);
    }
}