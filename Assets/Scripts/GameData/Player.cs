using System.Collections.Generic;
using DTO;
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
        
        public Player(){}

        public Player(PlayerDTO player)
        {
            this.Index = player.Index;
            this.FinishedUnit = player.FinishedUnit;
            this.IsRobot = player.IsRobot;
        }
    }
}