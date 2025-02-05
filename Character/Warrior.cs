using System.Text;
using TextRPG.Data;
using TextRPG.Manager;
using TextRPG.Utils;

namespace TextRPG;

public class Warrior : Character
{
    // 장착 슬롯을 key값으로 장비를 저장하는 Dictionary
    public Dictionary<EquipmentSlot, Equipment> Equipments { get; } = new();
    // 기본 스탯은 레벨업 외에 set을 막기 위한 강화용 스탯 (장비 등)
    public CharacterStats EnhancedStats { get; }
    private StringBuilder statusSb = new();

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
        // 선택한 장비가 장착되어 있으면 해제
        if (equipment.IsEquipped)
        {
            equipment.UnEquip(EnhancedStats);
            UpEquipItem(equipment);
            return;
        }

        // 이미 장착된 장비가 있으면 해제 후 새 장비 장착
        if (Equipments.TryGetValue(equipment.Slot, out var equippedItem))
        {
            equippedItem.UnEquip(EnhancedStats);
            UpEquipItem(equipment);
        }
        
        Equipments[equipment.Slot] = equipment;
        equipment.Equip(EnhancedStats);
    }
    
    // 던전에서 사용 할 총 스탯을 반환
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

    // 데이터 로드에서 사용하기 위해 bool값을 매개변수로 받음
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

    // 던전에서 사망 시 호출되며 마을로 돌아와서 시작
    public void Revive()
    {
        IsDead = false;
        CurrentHp = 5;
    }
}