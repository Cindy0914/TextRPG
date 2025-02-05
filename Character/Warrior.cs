using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG;

public class Warrior : Character
{
    public CharacterStats EnhancedStats { get; }
    public StringBuilder statusSb { get; } = new();
    public Dictionary<EquipmentSlot, Equipment> Equipments { get; } = new();

    private int _currentHp = 0;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                IsDead = true;
            }
            if (_currentHp > Stats.MaxHp + EnhancedStats.MaxHp)
            {
                _currentHp = Stats.MaxHp + EnhancedStats.MaxHp;
            }
        }
    }
    
    private int _gold;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            if (_gold < 0)
            {
                _gold = 0;
            }
        }
    }

    private int _exp;
    public int Exp
    {
        get => _exp;
        set
        {
            _exp = value;
            var levelData = GameManager.Instance.LevelData;
            if (levelData.IsMaxExp(Level, _exp))
            {
                LevelUp(false);
            }
        }
    }

    public Warrior(string name, CharacterStats stats) : base(name, stats)
    {
        Level = 1;
        Gold = 1500;
        Stats = stats;
        EnhancedStats = new CharacterStats(0, 0, 0);
        CurrentHp = stats.MaxHp;
    }

    public StringBuilder ShowStats()
    {
        statusSb.Clear();
        statusSb.Append($" {Name} (전사)");
        statusSb.AppendLine($" Lv.{Level}");
        
        statusSb.Append($" HP : {CurrentHp} / {Stats.MaxHp}");
        if (EnhancedStats.MaxHp > 0) statusSb.Append($" (+{EnhancedStats.MaxHp})");
        statusSb.AppendLine();
        
        statusSb.Append($" Atk : {Stats.Attack}");
        if (EnhancedStats.Attack > 0) statusSb.Append($" (+{EnhancedStats.Attack})");
        statusSb.AppendLine();
        
        statusSb.Append($" Dfs : {Stats.Defense}");
        if (EnhancedStats.Defense > 0) statusSb.Append($" (+{EnhancedStats.Defense})");
        statusSb.AppendLine();
        
        statusSb.AppendLine($" Exp : {Exp}");
        statusSb.AppendLine($" Gold : {Gold} G");
        
        return statusSb;
    }
    
    public void EquipItem(Equipment equipment)
    {
        if (equipment.IsEquipped)
        {
            equipment.UnEquip(EnhancedStats);
            UpEquipItem(equipment);
            return;
        }

        if (Equipments.TryGetValue(equipment.Slot, out var equippedItem))
        {
            equippedItem.UnEquip(EnhancedStats);
            UpEquipItem(equipment);
        }
        
        Equipments[equipment.Slot] = equipment;
        equipment.Equip(EnhancedStats);
    }
    
    public CharacterStats GetTotalStats()
    {
        return new CharacterStats(Stats.MaxHp + EnhancedStats.MaxHp, Stats.Attack + EnhancedStats.Attack, Stats.Defense + EnhancedStats.Defense);
    }

    public void UpEquipItem(Equipment equipment)
    {
        Equipments.Remove(equipment.Slot);
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
    }

    public void LevelUp(bool isLoad)
    {
        var levelData = GameManager.Instance.LevelData;
        levelData.LevelUp(Stats);
        Level++;

        if (isLoad) return;
        
        Exp = 0;
        string message = $"[System] 레벨이 올라 Lv.{Level} 가 되었습니다!";
        Util.PrintColorMessage(Util.success, message);
    }

    public void Revive()
    {
        IsDead = false;
        CurrentHp = 5;
    }
}