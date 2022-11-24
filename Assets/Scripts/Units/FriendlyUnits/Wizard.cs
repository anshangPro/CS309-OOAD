
using System.Collections.Generic;
using Units.Skills;

namespace Units.FriendlyUnits
{
    public class Wizard : Friendly
    {
        public Wizard()
        {
            this.type = 5;
            this.UnitName = "Wizard";
            this.level = 1;
            this.BaseHealth = 24;
            this.BaseMp = 25;
            this.BaseMv = 4;
            this.BaseDefense = 4;
            this.BaseDamage = 18;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 1f;
            this.Skills.AddLast(new WaterBall());
            foreach (Skill skill in Skills)
            {
                skill.BelongTo = this;
            }
            UpdatePanel();
        }
    }
}