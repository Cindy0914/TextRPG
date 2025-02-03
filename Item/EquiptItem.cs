namespace TextRPG;

public abstract class Equipment
{
    public EquipmentSlot Slot { get; set; }
    public StatType EnchantedStat { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public int Price { get; set; }

    public int SellPrice => Price / 2;
    public bool IsEquipped { get; set; }
}

public enum EquipmentSlot
{
    Weapon,
    Head,
    Armor,
    Foot
}