using System;
using System.Collections.Generic;
using System.Linq;
using GameData;
using Units.AI.Evaluator;

namespace Units.AI
{
    public class Searcher
    {
        private AbstractEvaluator _evaluator;

        public Searcher(AbstractEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        /// <summary>
        /// 根据当前的Evaluator进行对每个敌对棋子评估，选出最优的目标棋子
        /// </summary>
        /// <param name="selfUnits">己方units，就是AI方的units</param>
        /// <param name="enemyUnits"></param>
        /// <returns>unit二元组。第一个unit表示己方棋子，第二个unit表示地方棋子</returns>
        public Tuple<Unit, Unit> Search(List<Unit> selfUnits, List<Unit> enemyUnits)
        {
            List<Tuple<Unit, Unit>> unitPairs = (from selfUnit in selfUnits from enemyUnit in enemyUnits select new Tuple<Unit, Unit>(selfUnit, enemyUnit)).ToList();
            unitPairs.Sort((a, b) =>
            {
                float valueOfPairA = _evaluator.Evaluate(a.Item1, a.Item2);
                float valueOfPairB = _evaluator.Evaluate(b.Item1, b.Item2);
                return valueOfPairA.CompareTo(valueOfPairB);
            });

            return unitPairs.First(pair => (!pair.Item1.hasMoved && pair.Item1.Health > 0 && pair.Item2.Health > 0));
        }

        /// <summary>
        /// 找到去目标棋子的路径，由于目标可能不在self的移动范围之内，多加一个方法到时候内部处理目标不在可移动范围的情况
        /// </summary>
        /// <param name="self">要移动的棋子</param>
        /// <param name="target">目标棋子</param>
        /// <returns></returns>
        private List<Block> FindPathTo(Unit self, Unit target)
        {
            Block start = self.onBlock;
            Block end = target.onBlock;
            List<Block> reachableBlocks = MapManager.Instance.FindInRange(start, self.AtkRange);
            // TODO: 当目标不在可移动范围内时需要处理
            
            return MapManager.Instance.FindPath(start, end, reachableBlocks);
        }
    }
}