using System.Collections.Generic;
using GameData;
using Units;

namespace DTO
{
    public class PlayerDTO
    {
        public List<UnitDTO> Units;
        public int Index;
        public int FinishedUnit = 0;
        public bool IsRobot;

        public PlayerDTO(){}

        public PlayerDTO(Player player)
        {
            this.Index = player.Index;
            this.FinishedUnit = player.FinishedUnit;
            this.IsRobot = player.IsRobot;
            this.Units = new List<UnitDTO>();
            foreach (Unit unit in player.UnitsList)
            {
                UnitDTO u = new UnitDTO(unit);
                u.ofPlayer = Index;
                Units.Add(u);
            }
        }
    }
}