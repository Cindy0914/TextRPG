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
        Gold = 100;
        Stats = stats;
        EnhancedStats = new CharacterStats(0, 0, 0);
    }
    
    public void EquipItem(Equipment equipment)
    {
        if (equipment.IsEquipped)
            return;
        
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