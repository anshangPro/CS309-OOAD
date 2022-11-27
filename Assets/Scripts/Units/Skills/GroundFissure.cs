using GameData;

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
                RemainSkillPoint--;
                return true;
            }
            else
            {
                CancelEffect();
                GameDataManager.Instance.SelectedSkill = null;
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
            BelongTo.Damage -= _damageEnhance;
            BelongTo.AtkRange -= _rangeEnhance;
            return true;
        }
    }
}