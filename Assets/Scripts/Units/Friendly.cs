using System;
using System.Collections.Generic;
using GameData;
using Interfaces;

namespace Units
{
    public class Friendly : Unit
    {
        public LinkedList<Item> Package = new();

        public override string GetType()
        {
            return "Friendly";
        }

        public override bool CanFightWith()
        {
            int opposite = GameDataManager.Instance.MovedUnit.ofPlayer;
            Unit movedUnit = GameDataManager.Instance.MovedUnit;
            if (!MapManager.Instance.GetFightBlocks(movedUnit.onBlock, movedUnit, movedUnit.AtkRange).Contains(onBlock))
            {
                return false;
            }
            return !opposite.Equals(this.ofPlayer);
        }
    }
}