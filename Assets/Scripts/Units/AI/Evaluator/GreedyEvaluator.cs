using System.Numerics;
using UnityEngine;

namespace Units.AI.Evaluator
{
    public class GreedyEvaluator : AbstractEvaluator
    {
        /// <summary>
        /// 根据直线距离评估，越近越好
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override float Evaluate(Unit self, Unit target)
        {
            return Vector2Int.Distance(self.onBlock.Position2D, target.onBlock.Position2D);
        }
    }
}