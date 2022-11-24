namespace Units.Skills
{
    public class GroundFissure: Skill
    {
        private readonly int _rangeEnhance = 0;
        private readonly int _damageEnhance = 5;    
        
        public GroundFissure()
        {
            Name = "Ground fissure";
            SkillPoint = 5;
            RemainSkillPoint = 5;
        }

        public override bool SkillUse(Unit actor, Unit affected)
        {
            if (RemainSkillPoint > 0)
            {
                return true;
            }
            return false;
        }

        public override bool TakeEffect()
        {
            if (RemainSkillPoint > 0){
                BelongTo.Damage += _damageEnhance;
                BelongTo.AtkRange += _rangeEnhance;
                return true;
            }
            return false;
        }

        public override bool CancelEffect()
        {
            if (RemainSkillPoint > 0)
            {
                BelongTo.Damage -= _damageEnhance;
                BelongTo.Mv -= _rangeEnhance;
                return true;
            }
            return false;
        }
    }
}