
namespace Units.FriendlyUnits
{
    /// <summary>
    /// 剑士
    /// </summary>
    public class Knight : Friendly
    {
        public Knight()
        {
            this.type = 3;
            this.UnitName = "Knight";
            this.level = 1;
            this.BaseHealth = 30;
            this.BaseMp = 12;
            this.BaseMv = 4;
            this.BaseDefense = 5;
            this.BaseDamage = 17;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 1.3f;
            UpdatePanel();
        }
    }
}