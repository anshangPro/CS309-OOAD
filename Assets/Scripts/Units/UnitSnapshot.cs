namespace Units
{
    public record UnitSnapshot
    {
        public readonly Unit Unit;
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

        public UnitSnapshot(Unit unit)
        {
            Unit = unit;
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
        }
    }
}