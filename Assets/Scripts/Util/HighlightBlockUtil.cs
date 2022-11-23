using System.Linq;
using GameData;
using Units;
using Color = UnityEngine.Color;

namespace Util
{
    public class HighlightBlockUtil
    {
        public static void HighlightSelectableUnitOnBlocks()
        {
            foreach (Unit unit in GameDataManager.Instance.GetCurrentPlayer().UnitsList.Where(unit => !unit.hasMoved && unit.Health > 0))
            {
                unit.onBlock.SetHighlightColor(Color.green);
                GameDataManager.Instance.SelectableUnitOnBlocks.Add(unit.onBlock);
            }
        }
    }
}