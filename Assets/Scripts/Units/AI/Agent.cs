using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameData;
using GUI;
using Units.AI.Evaluator;
using UnityEngine;

namespace Units.AI
{
    public class Agent
    {
        private Searcher _searcher;
        private Player _robot;

        private Tuple<Unit, Unit> _pair;
        private Block _blockToMoveOn;

        public Agent(Searcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// 在AI的回合内，调用此方法，AI自动下棋
        /// </summary>
        /// <param name="selfUnits"></param>
        /// <param name="enemyUnits"></param>
        public void Think()
        {
            _pair = _searcher.Search(GameDataManager.Instance.GetCurrentPlayer().UnitsList,
                GameDataManager.Instance.GetOppositePlayer().UnitsList);
        }

        public void ClickUnitToMove()
        {
            _pair.Item1.IsClicked();
            Debug.Log($"Click unit {_pair.Item1.gameObject.name} at {_pair.Item1.onBlock.gameObject.transform.position}");
        }

        public void ClickBlockToMoveOn()
        {
            _blockToMoveOn = ChooseBlockMoveTo(_pair.Item1, _pair.Item2);
            _blockToMoveOn.IsClicked();
            Debug.Log($"Click block {_blockToMoveOn.gameObject.name} at {_blockToMoveOn.gameObject.transform.position}");
        }

        public void ConfirmBlockToMoveOn()
        {
            _blockToMoveOn.IsClicked();
            Debug.Log($"Confirm block {_blockToMoveOn.gameObject.name} at {_blockToMoveOn.gameObject.transform.position}");
        }

        public void ClickAttackButton()
        {
            MouseController.GameObjectName = "AttackButton";
            GameObject.Find("AttackButton").GetComponent<ButtonScript>().IsClicked();
        }

        public void ClickEnemyToAttackOrSkipAttack()
        {
            if (!MapManager.Instance.GetFightBlocks(_pair.Item1.onBlock, _pair.Item1, _pair.Item1.AtkRange)
                    .Contains(_pair.Item2.onBlock))
            {
                UIManager.SkipAttackButton();
                return;
            }
            _pair.Item2.IsClicked();
            Debug.Log($"Click unit {_blockToMoveOn.gameObject.name} at {_blockToMoveOn.gameObject.transform.position}");
        }

        private Block ChooseBlockMoveTo(Unit self, Unit target)
        {
            List<Block> neighbors = MapManager.Instance.GetNeighborBlocks(target.onBlock,
                MapManager.Instance.Map.Values.ToList());
            neighbors.Sort((a, b) => Vector3.Distance(self.onBlock.gameObject.transform.position, a.gameObject.transform.position)
                .CompareTo(Vector3.Distance(self.onBlock.gameObject.transform.position,
                    b.gameObject.transform.position)));

            Block dstBlock = neighbors.First(block => block.standUnit == null);
            List<Block> inRangeBlocks = MapManager.Instance.FindInRange(self.onBlock, target.Mv);
            if (inRangeBlocks.Contains(dstBlock)) return neighbors.First(block => block.standUnit == null);
            
            // TODO: 逻辑需要加强，有时候这里的neighbors会为空
            inRangeBlocks.Sort((a, b) =>
            {
                float distA = Vector3.Distance(a.transform.position, dstBlock.transform.position);
                float distB = Vector3.Distance(b.transform.position, dstBlock.transform.position);
                return distA.CompareTo(distB);
            });

            return inRangeBlocks.First();
        }
    }
}