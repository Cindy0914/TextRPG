using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG.Item;

public class Inventory
{
    private StringBuilder inventorySb = new();
    private StringBuilder itemSb = new();
    private StringBuilder equipSb = new();
    public List<Equipment> Equipments { get; } = new();
    public List<ConsumeItem> ConsumeItems { get; } = new();

    public void Init()
    {
        inventorySb.AppendLine();
        inventorySb.AppendLine(" == 인벤토리 ==");
        inventorySb.AppendLine();
        inventorySb.AppendLine("1. 소비 아이템");
        inventorySb.AppendLine("2. 장비 관리");
        inventorySb.AppendLine("0. 나가기");
        inventorySb.AppendLine();
        inventorySb.AppendLine("어떤 행동을 하시겠습니까?");
    }

    public void ShowInventory()
    {
        Console.Clear();

        string message = "[System] 인벤토리를 확인합니다.";
        Util.PrintColorMessage(Util.system, message);

        Console.Write(inventorySb.ToString());
        int input = Util.GetUserInput(2);
        InventoryAction(input);
    }

    private void InventoryAction(int input)
    {
        switch (input)
        {
            case 0: Town.Instance.Run();
                break;
            case 1: UseConsumeItems();
                break;
            case 2: ManageEquipment();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private void UseConsumeItems()
    {
        Warrior player = GameManager.Instance.Player!;
        string message = "[System] 아이템을 사용합니다.";
        string hpMessage = $"[System] 현재 HP : {player.CurrentHp} / {player.Stats.MaxHp}";

        // 아이템을 계속해서 사용할 수 있도록 무한 루프
        while (true)
        {
            hpMessage = $"[System] 현재 HP : {player.CurrentHp} / {player.Stats.MaxHp}";
            
            Console.Clear();
            itemSb.Clear();
            Util.PrintColorMessage(Util.system, message);
            Util.PrintColorMessage(Util.system, hpMessage);

            itemSb.AppendLine();
            itemSb.AppendLine(" == 인벤토리_아이템 목록 ==");
            itemSb.AppendLine();
            for (int i = 0; i < ConsumeItems.Count; i++)
            {
                var consumeItemitem = ConsumeItems[i];
                itemSb.AppendLine($"{i + 1}. {consumeItemitem.Name} | {consumeItemitem.GetDescription()}");
            }

            itemSb.AppendLine();
            itemSb.AppendLine($"0. 나가기");
            itemSb.AppendLine();
            itemSb.AppendLine("어떤 아이템을 사용하시겠습니까?");
            Console.Write(itemSb.ToString());

            int input = Util.GetUserInput(ConsumeItems.Count);
            if (input == 0)
            {
                ShowInventory();
                return;
            }

            var selectedItem = ConsumeItems[input - 1];
            selectedItem.Use(player);
            ConsumeItems.Remove(selectedItem);
        }
    }

    private void ManageEquipment()
    {
        Warrior player = GameManager.Instance.Player!;
        const string message = "[System] 장비를 장착하거나 해제합니다.";

        while (true)
        {
            Console.Clear();
            equipSb.Clear();
            Util.PrintColorMessage(Util.system, message);
            
            equipSb.AppendLine();
            equipSb.AppendLine(" == 인벤토리_장비 관리 ==");
            equipSb.AppendLine();
            for (int i = 0; i < Equipments.Count; i++)
            {
                var equip = Equipments[i];
                equipSb.Append($"{i + 1}. ");

                Console.Write(equipSb.ToString());
                if (Equipments[i].IsEquipped) Util.PrintColorMessage(Util.success, "[E]", false);

                equipSb.Clear();
                equipSb.AppendLine($"{equip.Name} +({equip.Stat.GetName()}{equip.Value}) | {equip.Desc}");
            }

            equipSb.AppendLine();
            equipSb.AppendLine($"0. 나가기");
            equipSb.AppendLine();
            equipSb.AppendLine("어떤 장비를 장착하시겠습니까?");
            Console.Write(equipSb.ToString());

            int input = Util.GetUserInput(Equipments.Count);

            if (input == 0)
            {
                ShowInventory();
                return;
            }

            var item = Equipments[input - 1];
            player.EquipItem(item);
        }
    }

    public List<Equipment> GetPlayerEquipments()
    {
        return Equipments;
    }
    
    public void AddEquipment(Equipment equipment)
    {
        Equipments.Add(equipment);
    }
    
    public void RemoveEquipment(Equipment equipment)
    {
        Equipments.Remove(equipment);
    }

    public void AddConsumeItem(ConsumeItem consumeItem)
    {
        ConsumeItems.Add(consumeItem);
    }
}