
namespace Units.FriendlyUnits
{
    public class Wizard : Unit
    {
        public Wizard()
        {
            this.UnitName = "Wizard";
            this.level = 1;
            this.BaseHealth = 24;
            this.BaseMp = 25;
            this.BaseMv = 4;
            this.BaseDefense = 4;
            this.BaseDamage = 18;
            this.BaseAtkRange = 1;
            this.DefenseUpdateRate = 1f;
            UpdatePanel();
        }
    }
}