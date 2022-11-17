using System.Collections.Generic;
using Units;

namespace GameData
{
    public class Player
    {
        public List<Unit> UnitsList = new List<Unit>();
        public int Index;
        public int FinishedUnit = 0;
        public bool IsRobot;

        public bool TurnFinish()
        {
            return FinishedUnit == UnitsList.Count;
        }
    }
}