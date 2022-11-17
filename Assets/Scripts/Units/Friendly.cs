using System;
using System.Collections.Generic;
using GameData;
using Interfaces;

namespace Units
{
    public class Friendly : Unit
    {
        public LinkedList<Item> Package = new();
        public LinkedList<Skill> Skills = new();

        public override string GetType()
        {
            return "Friendly";
        }

        public override bool CanFightWith()
        {
            String opposite = GameDataManager.Instance.MovedUnit.GetType();
            //TODO 需要判断是否在攻击范围内 by 周凡卜 2022/11/3
            return opposite.Equals("Enemy");
        }
    }
}