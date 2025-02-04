using TextRPG.Manager;
using TextRPG.Stage;

namespace TextRPG.Data;

public class SaveData
{
    public string Name;
    public int Level;
    public int Exp;
    public int Gold;
    public int CurrentHp;

    public int[]? Equipped;
    public int[]? Equipments;
    public int[]? Items;

    public SaveData(string name, int level, int exp, int gold, int currentHp)
    {
        Name = name;
        Level = level;
        Exp = exp;
        Gold = gold;
        CurrentHp = currentHp;
    }

    public void Save()
    {
        SaveEquipments();
        SaveItems();
        SaveEquipped();
    }
    
    public void Load()
    {
        Warrior warrior = new(Name, new CharacterStats(100, 10, 5));
        GameManager.Instance.SetPlayer(warrior);
        
        if (Level > 1)
        {
            for (int i = 1; i < Level; i++)
            {
                warrior.LevelUp(true);
            }
        }
        warrior.Exp = Exp;
        warrior.Gold = Gold;
        warrior.CurrentHp = CurrentHp;

        LoadEquipments();
        LoadItems();
        LoadEquipped();
    }

    private void SaveEquipments()
    {
        var equips = GameManager.Instance.Inventory.Equipments;
        if (equips.Count == 0)
        {
            return;
        }
        
        Equipments = new int[equips.Count];
        for (int i = 0; i < equips.Count; i++)
        {
            Equipments[i] = equips[i].Id;
        }
    }

    private void SaveItems()
    {
        var items = GameManager.Instance.Inventory.ConsumeItems;
        if (items.Count == 0)
        {
            return;
        }
        
        Items = new int[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            Items[i] = items[i].Id;
        }
    }

    private void SaveEquipped()
    {
        var equips = GameManager.Instance.Player.Equipments;
        if (equips.Count == 0)
        {
            return;
        }
        
        Equipped = new int[equips.Count];
        for (int i = 0; i < equips.Count; i++)
        {
            Equipped[i] = equips.ElementAt(i).Value.Id;
        }
    }
    
    private void LoadEquipments()
    {
        if (Equipments == null)
        {
            return;
        }

        for (int i = 0; i < Equipments.Length; i++)
        {
            var equipment = DataManager.Instance.EquipmentDatas.Dict![Equipments[i]];
            GameManager.Instance.Inventory.AddEquipment(equipment);
            Shop.Instance.Purchased.Add(equipment.Id, true);
        }
    }
    
    private void LoadItems()
    {
        if (Items == null)
        {
            return;
        }

        for (int i = 0; i < Items.Length; i++)
        {
            var item = DataManager.Instance.ConsumeItemDatas.Dict![Items[i]];
            GameManager.Instance.Inventory.AddConsumeItem(item);
        }
    }
    
    private void LoadEquipped()
    {
        if (Equipped == null)
        {
            return;
        }

        for (int i = 0; i < Equipped.Length; i++)
        {
            var equipment = DataManager.Instance.EquipmentDatas.Dict![Equipped[i]];
            GameManager.Instance.Player!.EquipItem(equipment);
        }
    }
}