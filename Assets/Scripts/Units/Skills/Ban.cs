using GameData;
using UnityEngine;

namespace Units.Skills
{
    public class Ban : Skill
    {
        private readonly int _rangeEnhance = 0;
        private readonly int _damageEnhance = 2;

        public Ban()
        {
            Name = "Ban";
            SkillPoint = 10;
            RemainSkillPoint = 10;
            FontColor = new Color(139f/255, 69f/255, 19f/255, 1);
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
            if (RemainSkillPoint > 0)
            {
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