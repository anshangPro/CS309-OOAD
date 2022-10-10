namespace Units
{
    public abstract class Skill
    {
        /// <summary>
        /// 技能总可用次数
        /// </summary>
        public abstract int SkillPoint { get; set; }
        /// <summary>
        /// 技能剩余次数
        /// </summary>
        public abstract int RemainSkillPoint { get; set; }
        /// <summary>
        /// 技能pp消耗
        /// </summary>
        public abstract int Cost { get; set; }
        /// <summary>
        /// actor 对 affected 使用技能
        /// 二者可为同一对象
        /// </summary>
        /// <param name="actor">使用技能者</param>
        /// <param name="affected">技能作用对象</param>
        /// <returns>返回是否成功使用技能(即使作用对象存在特殊效果导致无效化也返回true 仅在skill point不足时返回false)</returns>
        public abstract bool skillUse(Unit actor, Unit affected);
    }
}