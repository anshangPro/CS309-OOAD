using System.Collections.Generic;
using DTO;

namespace Units
{
    public record UnitSnapshot
    {
        public readonly UnitDTO UnitDto;
        public readonly int BelongTo;
        public readonly int type;
        public readonly float MaxHealth;
        public readonly float Health;
        public readonly float MaxMp;
        public readonly float Mp;
        public readonly float Damage;
        public readonly float Defense;
        public readonly int Level;
        public readonly int AtkRange;
        public readonly int Exp;
        public readonly int Mv;
        public readonly bool HasMoved;
        public readonly bool HasAttacked;
        public readonly Block OnBlock;
        public readonly Dictionary<string, int> SkillCounts = new();

        public UnitSnapshot(Unit unit)
        {
            BelongTo = unit.ofPlayer;
            UnitDto = new UnitDTO(unit);
            type = unit.type;
            MaxHealth = unit.MaxHealth;
            Health = unit.Health;
            MaxMp = unit.MaxMp;
            Mp = unit.Mp;
            Damage = unit.Damage;
            Defense = unit.Defense;
            Level = unit.level;
            AtkRange = unit.AtkRange;
            Exp = unit.Exp;
            Mv = unit.Mv;
            HasMoved = unit.hasMoved;
            HasAttacked = unit.hasAttacked;
            OnBlock = unit.onBlock;
            foreach (Skill skill in unit.Skills)
            {
                SkillCounts.Add(skill.Name, skill.RemainSkillPoint);
            }
        }
    }
}