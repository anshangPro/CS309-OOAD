using System.Drawing;
using GameData;
using Color = UnityEngine.Color;

namespace Units.Skills
{
    public class FireSword: Skill
    {
        private readonly int _rangeEnhance = 1;
        private readonly int _damageEnhance = 8;    
        
        public FireSword()
        {
            Name = "Fire all the thing";
            SkillPoint = 8;
            RemainSkillPoint = 8;
            FontColor = Color.red;;
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