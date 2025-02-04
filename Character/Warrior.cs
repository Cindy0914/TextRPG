using System.Text;
using TextRPG.Data;

namespace TextRPG;

public class Warrior : Character
{
    public int Gold { get; private set; }
    public CharacterStats EnhancedStats { get; }
    public StringBuilder statusSb { get; } = new();
    public Dictionary<EquipmentSlot, Equipment> Equipments { get; } = new();

    public Warrior(string name, CharacterStats stats) : base(name, stats)
    {
        Level = 1;
        Gold = 1500;
        Stats = stats;
        EnhancedStats = new CharacterStats(0, 0, 0);
    }

    public StringBuilder ShowStats()
    {
        statusSb.Clear();
        statusSb.AppendLine($" {Name} (전사)");
        statusSb.AppendLine($" Lv.{Level}");
        
        statusSb.Append($" HP : {Stats.CurrentHp} / {Stats.MaxHp}");
        if (EnhancedStats.MaxHp > 0) statusSb.Append($" (+{EnhancedStats.MaxHp})");
        statusSb.AppendLine();
        
        statusSb.Append($" Atk : {Stats.Attack}");
        if (EnhancedStats.Attack > 0) statusSb.Append($" (+{EnhancedStats.Attack})");
        statusSb.AppendLine();
        
        statusSb.Append($" Dfs : {Stats.Defense}");
        if (EnhancedStats.Defense > 0) statusSb.Append($" (+{EnhancedStats.Defense})");
        statusSb.AppendLine();
        
        statusSb.AppendLine($" Gold : {Gold} G\n");
        
        return statusSb;
    }
    
    public void EquipItem(Equipment equipment)
    {
        if (equipment.IsEquipped)
        {
            equipment.UnEquip(EnhancedStats);
            return;
        }
        
        if (Equipments.TryGetValue(equipment.Slot, out var equippedItem))
            equippedItem.UnEquip(EnhancedStats);
        
        Equipments[equipment.Slot] = equipment;
        equipment.Equip(EnhancedStats);
    }
    
    public void AddGold(int gold)
    {
        Gold += gold;
    }
    
    public void RemoveGold(int gold)
    {
        Gold -= gold;
    }
    
    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}