
using Units.Skills;

namespace Units.FriendlyUnits
{
    /// <summary>
    /// 刺客
    /// </summary>
    public class Assassin : Friendly
    {
        public Assassin()
        {
            this.type = 1;
            this.UnitName = "Assassin";
            this.level = 1;
            this.BaseHealth = 20;
            this.BaseMp = 10;
            this.BaseMv = 5;
            this.BaseDefense = 4;
            this.BaseDamage = 20;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 0.9f;
            this.Skills.AddLast(new GroundFissure());
            foreach (Skill skill in Skills)
            {
                skill.BelongTo = this;
            }
            UpdatePanel();
        }
    }
}