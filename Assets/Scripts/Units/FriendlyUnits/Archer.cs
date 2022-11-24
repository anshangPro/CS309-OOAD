using Units.Skills;
using UnityEngine;

namespace Units.FriendlyUnits
{
    /// <summary>
    /// 弓箭手
    /// </summary>
    public class Archer : Friendly
    {
        public Archer()
        {
            this.type = 0;
            this.UnitName = "Archer";
            this.level = 1;
            this.BaseHealth = 24;
            this.BaseMp = 15;
            this.BaseMv = 4;
            this.BaseDefense = 4;
            this.BaseDamage = 15;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 1f;
            this.Skills.AddLast(new Snipe());
            foreach (Skill skill in Skills)
            {
                skill.BelongTo = this;
            }
            UpdatePanel();
        }
    }
}