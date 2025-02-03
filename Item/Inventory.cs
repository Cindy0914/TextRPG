using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Stage;
using TextRPG.Utils;

namespace TextRPG;

public class Inventory
{
    private StringBuilder inventorySb = new();
    private List<Equipment> equipments = new();
    private List<ConsumeItem> consumeItems = new();

    public void Init()
    {
        inventorySb.AppendLine();
        inventorySb.AppendLine(" == 인벤토리 ==");
        inventorySb.AppendLine();
        inventorySb.AppendLine("1. 소비 아이템");
        inventorySb.AppendLine("2. 장비 관리");
        inventorySb.AppendLine("3. 나가기");
        inventorySb.AppendLine();
        inventorySb.AppendLine("어떤 행동을 하시겠습니까?");
    }

    public void ShowInventory()
    {
        Console.Clear();

        string message = "[System] 인벤토리를 확인합니다.";
        Util.PrintColorMessage(Util.system, message);

        Console.Write(inventorySb.ToString());
        int input = Util.GetUserInput(3);
        InventoryAction(input);
    }

    private void InventoryAction(int input)
    {
        switch (input)
        {
            case 1:
                UseConsumeItems();
                break;
            case 2:
                ManageEquipment();
                break;
            case 3:
                Title.Run();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(input), input, null);
        }
    }

    private void UseConsumeItems()
    {
        StringBuilder itemSb = new();
        Warrior player = GameManager.Player!;
        string message = "[System] 아이템을 사용합니다.";
        string hpMessage = $"[System] 현재 HP : {player.Stats.CurrentHp} / {player.Stats.MaxHp}\n";

        while (true)
        {
            hpMessage = $"[System] 현재 HP : {player.Stats.CurrentHp} / {player.Stats.MaxHp}\n";
            
            Console.Clear();
            itemSb.Clear();
            Util.PrintColorMessage(Util.system, message);
            Util.PrintColorMessage(Util.system, hpMessage);
            
            itemSb.AppendLine(" == 아이템 목록 ==\n");
            for (int i = 0; i < consumeItems.Count; i++)
            {
                var consumeItemitem = consumeItems[i];
                itemSb.AppendLine($"{i + 1}. {consumeItemitem.Name} | {consumeItemitem.GetDescription()}");
            }

            itemSb.AppendLine($"\n{consumeItems.Count + 1}. 나가기\n");
            itemSb.AppendLine("어떤 아이템을 사용하시겠습니까?");
            Console.Write(itemSb.ToString());

            int input = Util.GetUserInput(consumeItems.Count + 1);
            if (input == consumeItems.Count + 1)
            {
                ShowInventory();
                return;
            }

            var selectedItem = consumeItems[input - 1];
            selectedItem.Use(player.Stats);
            consumeItems.Remove(selectedItem);
        }
    }

    private void ManageEquipment()
    {
        StringBuilder equipmentSb = new();
        Warrior player = GameManager.Player!;
        const string message = "[System] 장비를 장착하거나 해제합니다.\n";

        while (true)
        {
            Console.Clear();
            equipmentSb.Clear();
            Util.PrintColorMessage(Util.system, message);
            
            equipmentSb.AppendLine(" == 장비 관리 ==\n");
            for (int i = 0; i < equipments.Count; i++)
            {
                var equip = equipments[i];
                equipmentSb.Append($"{i + 1}. ");

                Console.Write(equipmentSb.ToString());
                if (equipments[i].IsEquipped) Util.PrintColorMessage(Util.success, "[E]", false);

                equipmentSb.Clear();
                equipmentSb.AppendLine($"{equip.Name} +({equip.Stat.GetName()}{equip.Value}) | {equip.Desc}");
            }

            equipmentSb.AppendLine($"\n{equipments.Count + 1}. 나가기\n");
            equipmentSb.AppendLine("어떤 장비를 장착하시겠습니까?");
            Console.Write(equipmentSb.ToString());

            int input = Util.GetUserInput(equipments.Count + 1);

            if (input == equipments.Count + 1)
            {
                ShowInventory();
                return;
            }

            var item = equipments[input - 1];
            player.EquipItem(item);
        }
    }

    public void GetEquipment(Equipment equipment)
    {
        equipments.Add(equipment);
    }

    public void GetConsumeItem(ConsumeItem consumeItem)
    {
        consumeItems.Add(consumeItem);
    }
}