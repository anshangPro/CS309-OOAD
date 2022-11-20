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
        /// <param name="self">即将移动的棋子</param>
        /// <returns>返回去往目标棋子的路径</returns>
        public List<Block> Search(Unit self)
        {
            List<Unit> enemies = GameDataManager.Instance.GetOppositePlayer().UnitsList;
            enemies.Sort((a, b) =>
            {
                float valueOfA = _evaluator.Evaluate(self, a);
                float valueOfB = _evaluator.Evaluate(self, b);
                return valueOfA.CompareTo(valueOfB);
            });
            return FindPathTo(self, enemies[0]);
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