using System.Text;
using TextRPG.Data;

namespace TextRPG;

public class Warrior : Character
{
    public int Gold { get; private set; }
    public CharacterStats EnhancedStats { get; }
    public Dictionary<EquipmentSlot, Equipment> Equipments { get; } = new();

    public Warrior(string name, CharacterStats stats) : base(name, stats)
    {
        Level = 1;
        Gold = 1500;
        Stats = stats;
        EnhancedStats = new CharacterStats(0, 0, 0);

        Stats.CurrentHp -= 50;
    }

    public StringBuilder ShowStats()
    {
        StringBuilder sb = new();
        sb.AppendLine($" {Name} (전사)");
        sb.AppendLine($" Lv.{Level}");
        
        sb.Append($" HP : {Stats.CurrentHp} / {Stats.MaxHp}");
        if (EnhancedStats.MaxHp > 0) sb.Append($" (+{EnhancedStats.MaxHp})");
        sb.AppendLine();
        
        sb.Append($" Atk : {Stats.Attack}");
        if (EnhancedStats.Attack > 0) sb.Append($" (+{EnhancedStats.Attack})");
        sb.AppendLine();
        
        sb.Append($" Dfs : {Stats.Defense}");
        if (EnhancedStats.Defense > 0) sb.Append($" (+{EnhancedStats.Defense})");
        sb.AppendLine();
        
        sb.AppendLine($" Gold : {Gold} G\n");
        
        return sb;
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
    
    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}