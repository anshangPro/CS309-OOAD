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
            GameDataManager.Instance.SelectableUnitOnBlocks.Clear();
            foreach (Unit unit in GameDataManager.Instance.GetCurrentPlayer().UnitsList.Where(unit => unit.Health > 0))
            {
                unit.onBlock.SetHighlightColor(unit.hasMoved ? Color.yellow: Color.green);
                GameDataManager.Instance.SelectableUnitOnBlocks.Add(unit.onBlock);
            }
        }
        
        public static void DeHighlightSelectableUnitOnBlocks()
        {
            GameDataManager.Instance.SelectableUnitOnBlocks.Clear();
            foreach (Unit unit in GameDataManager.Instance.GetCurrentPlayer().UnitsList.Where(unit => /*!unit.hasMoved && */unit.Health > 0))
            {
                unit.onBlock.SetOverlayGridType(OverlayGrid.OverlayGridType.None);
            }
        }
    }
}