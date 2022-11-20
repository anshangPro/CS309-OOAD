using System;
using System.Collections.Generic;
using System.Linq;
using GameData;

namespace Units
{
    public class Enemy : Unit
    {
        public Enemy()
        {
            this.BaseHealth = 20;
            this.BaseMp = 10;
            UpdatePanel();
        }
        
        public override void Attack(Unit target)
        {
            base.Attack(target);
        }

        public Unit ChooseUnitToAttack()
        {
            //Dictionary<Unit, double> disMap = new Dictionary<Unit, double>();
            //GameDataManager.Instance.AllUnits.ForEach(enemy =>
            //{
            //    Block block = enemy.onBlock;
            //    double dis = Math.Sqrt(Math.Pow(onBlock.X - block.X, 2) + Math.Pow(onBlock.Z - block.Z, 2));
            //    disMap.Add(enemy, dis);
            //});
            //
            //disMap = disMap.OrderBy(elem => elem.Value).ToDictionary(elem => elem.Key, elem => elem.Value);
            //
            //return disMap.ElementAt(0).Key;
            return null;
        }

        public override string GetType()
        {
            return "Enemy";
        }

        public override bool CanFightWith()
        {
            int opposite = GameDataManager.Instance.MovedUnit.ofPlayer;
            
            //TODO 需要判断是否在攻击范围内 by 周凡卜 2022/11/3
            Unit movedUnit = GameDataManager.Instance.MovedUnit;
            if (!MapManager.Instance.FindInRange(movedUnit.onBlock, movedUnit.AtkRange).Contains(onBlock))
            {
                return false;
            }
            
            return opposite.Equals(this.type);
        }
    }
}