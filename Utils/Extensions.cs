namespace TextRPG.Utils;

public static class Extensions
{
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