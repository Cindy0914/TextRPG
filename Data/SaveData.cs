using TextRPG.Manager;
using TextRPG.Stage;

namespace TextRPG.Data;

// 데이터를 저장하고 불러올 때 생성하는 클래스
public class SaveData
{
    public string Name { get; }
    public int Level { get; }

    public int Exp { get; }
    public int Gold { get; }
    public int CurrentHp { get; }

    // DataManager가 데이터들을 가지고 있기 때문에 Id만 저장
    public int[]? Equipped { get; set; }
    public int[]? Equipments { get; set; }
    public int[]? Items { get; set; }

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
        SaveEquipments(); // 현재 가진 장비 저장
        SaveItems();      // 현재 가진 소모품 저장
        SaveEquipped();   // 현재 장착중인 장비 저장
    }
    
    public void Load()
    {
        Warrior warrior = new(Name, new CharacterStats(100, 10, 5));
        GameManager.Instance.SetPlayer(warrior);
        
        // 레벨업을 위해 레벨만큼 레벨업
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
        var equips = GameManager.Instance.Player!.Equipments;
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
            
            // 플레이어가 구매한 장비라면 Shop에서 매진 처리
            Shop.Instance.Purchased.Remove(equipment.Id);
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