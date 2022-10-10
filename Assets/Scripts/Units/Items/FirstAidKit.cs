using System;

namespace Units.Items
{
    public class FirstAidKit: Item
    {
        public override int Quantity { get; protected set; }
        public override int RemainQuantity { get; protected set; }
        public int HealingAmount;

        public override bool itemUse(Unit unit)
        {
            if (RemainQuantity <= 0) return false;
            RemainQuantity--;
            unit.Health += HealingAmount;
            unit.Health = Math.Min(unit.Health, unit.MaxHealth);
            return true;
        }
    }
}