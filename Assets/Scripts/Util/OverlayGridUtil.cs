using System.Collections.Generic;
using UnityEngine;
using static Util.OverlayGrid;


namespace Util
{
    public class OverlayGridUtil
    {
        public static OverlayGridType TranslateDirection(Block preBlock, Block curBlock, Block nxtBlock)
        {
            bool isEnd = nxtBlock == null;
    
            Vector2Int preDirection = preBlock != null ? curBlock.Position2D - preBlock.Position2D : new Vector2Int(0, 0);
            Vector2Int nxtDirection = nxtBlock != null ? nxtBlock.Position2D - curBlock.Position2D : new Vector2Int(0, 0);
            Vector2Int direction = preDirection != nxtDirection ? preDirection + nxtDirection : nxtDirection;
    
            if ((direction == new Vector2Int(1, 0) || direction == new Vector2Int(-1, 0)) && !isEnd)
            {
                return OverlayGridType.LineX;
            }
        
            if ((direction == new Vector2Int(0, 1) || direction == new Vector2Int(0, -1)) && !isEnd)
            {
                return OverlayGridType.LineZ;
            }
        
            if (direction == new Vector2Int(0, 1) && isEnd)
            {
                return OverlayGridType.ArrowZPos;
            }
    
            if (direction == new Vector2Int(1, 0) && isEnd)
            {
                return OverlayGridType.ArrowXPos;
            }
    
            if (direction == new Vector2Int(0, -1) && isEnd)
            {
                return OverlayGridType.ArrowZNeg;
            }
    
            if (direction == new Vector2Int(-1, 0) && isEnd)
            {
                return OverlayGridType.ArrowXNeg;
            }
    
            if (direction == new Vector2Int(1, 1))
            {
                return preDirection.y < nxtDirection.y ? OverlayGridType.CornerXPosZPos : OverlayGridType.CornerXNegZNeg;
            }
    
            if (direction == new Vector2Int(-1, 1))
            {
                return preDirection.y < nxtDirection.y ? OverlayGridType.CornerXNegZPos : OverlayGridType.CornerXPosZNeg;
            }
    
            if (direction == new Vector2Int(1, -1))
            {
                return preDirection.y < nxtDirection.y ? OverlayGridType.CornerXNegZPos : OverlayGridType.CornerXPosZNeg;
            }
    
            if (direction == new Vector2Int(-1, -1))
            {
                return preDirection.y < nxtDirection.y ? OverlayGridType.CornerXPosZPos : OverlayGridType.CornerXNegZNeg;
            }
    
            return OverlayGridType.None;
        }

        public static void SetOverlayGridToNone(List<Block> blocks)
        {
            blocks.RemoveAll(block => block == null);
            blocks.ForEach(block => block.SetOverlayGridType(OverlayGridType.None));
        }
    }
}
