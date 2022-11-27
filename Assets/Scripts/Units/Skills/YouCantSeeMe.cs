using GameData;

namespace Units.Skills
{
    public class YouCantSeeMe: Skill
    {
        private readonly int _rangeEnhance = 2;
        private readonly int _damageEnhance = 1;    
        
        public YouCantSeeMe()
        {
            Name = "You can't see me";
            SkillPoint = 10;
            RemainSkillPoint = 10;
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