
using UnityEngine;

namespace Units
{
    public abstract class Skill
    {
        public Unit BelongTo;
        public string Name;
        
        /// <summary>
        /// 技能总可用次数
        /// </summary>
        public int SkillPoint;

        /// <summary>
        /// 技能剩余次数
        /// </summary>
        public int RemainSkillPoint;

        /// <summary>
        /// 技能pp消耗
        /// </summary>
        public int Cost;

        public Color FontColor;
        
        /// <summary>
        /// actor 对 affected 使用技能
        /// 二者可为同一对象
        /// </summary>
        /// <param name="actor">使用技能者</param>
        /// <param name="affected">技能作用对象</param>
        /// <returns>返回是否成功使用技能(即使作用对象存在特殊效果导致无效化也返回true 仅在skill point不足时返回false)</returns>
        public abstract bool SkillUse(Unit actor, Unit affected);

        public abstract bool TakeEffect();

        public abstract bool CancelEffect();
    }
}