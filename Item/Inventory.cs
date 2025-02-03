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

    public void Init()
    {
        inventorySb.AppendLine();
        inventorySb.AppendLine(" == 인벤토리 ==");
        inventorySb.AppendLine();
        inventorySb.AppendLine("1. 아이템 목록");
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
                ShowItems();
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

    // TODO
    private void ShowItems()
    {
        Console.Clear();

        StringBuilder itemSb = new();
        string message = "[System] 아이템 목록을 확인합니다.";
        Util.PrintColorMessage(Util.system, message);

        itemSb.AppendLine();
        itemSb.AppendLine("아이템 목록");
        itemSb.AppendLine();
        itemSb.AppendLine("1. 나가기");
        itemSb.AppendLine();
        int input = Util.GetUserInput(1);
        ShowInventory();
    }

    private void ManageEquipment()
    {
        StringBuilder equipmentSb = new();
        Warrior player = GameManager.Player!;
        const string message = "[System] 장비를 장착하거나 해제합니다.";

        while (true)
        {
            Console.Clear();
            equipmentSb.Clear();
            
            Util.PrintColorMessage(Util.system, message);
            equipmentSb.AppendLine();
            equipmentSb.AppendLine(" == 장비 관리 ==");
            equipmentSb.AppendLine();

            for (int i = 0; i < equipments.Count; i++)
            {
                var equip = equipments[i];
                equipmentSb.Append($"{i + 1}. ");

                Console.Write(equipmentSb.ToString());
                if (equipments[i].IsEquipped) Util.PrintColorMessage(Util.success, "[E]", false);

                equipmentSb.Clear();
                equipmentSb.AppendLine($"{equip.Name} +({equip.Stat.GetName()}{equip.Value}) | {equip.Desc}");
            }

            equipmentSb.AppendLine();
            equipmentSb.AppendLine($" {equipments.Count + 1}. 나가기");
            equipmentSb.AppendLine();
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
}