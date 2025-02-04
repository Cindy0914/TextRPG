using TextRPG.Data;

namespace TextRPG.Utils;

public static class Extensions
{
    public static string GetName(this EquipmentSlot slot)
    {
        return slot switch
        {
            EquipmentSlot.Weapon => "무기",
            EquipmentSlot.Head   => "헬멧",
            EquipmentSlot.Armor  => "갑옷",
            EquipmentSlot.Acc    => "악세사리",
            _                    => string.Empty
        };
    }
    
    public static string GetName(this StatType statType)
    {
        return statType switch
        {
            StatType.MaxHp   => "최대체력",
            StatType.Attack  => "공격력",
            StatType.Defense => "방어력",
            _                => string.Empty
        };
    }
}