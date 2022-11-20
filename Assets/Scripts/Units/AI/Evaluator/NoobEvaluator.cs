using UnityEngine;

namespace Units.AI.Evaluator
{
    public class NoobEvaluator : AbstractEvaluator
    {
        /// <summary>
        /// 根据攻击一次后的剩余血量给分
        /// 分越低优先级最高
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override float Evaluate(Unit self, Unit target)
        {
            float realDamage = self.Damage - target.Defense;
            float zeroDmgWeight = 10086;
            float stillAliveWeight = 500;
            // 无法造成伤害，返回一个大数
            if (realDamage <= 0)
            {
                return zeroDmgWeight;
            }
            // 无法一击必杀，剩余血量越少优先级越高
            if (target.Health > realDamage)
            {
                return stillAliveWeight + target.Health - realDamage;
            }
            // 能够一击必杀，伤害溢出越少优先级越高
            else
            {
                return realDamage - target.Health;
            }
        }
    }
}