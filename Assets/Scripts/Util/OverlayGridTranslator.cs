using UnityEngine;
using static Block;


namespace Util
{
    public class OverlayGridTranslator
    {
        public OverlayGridType TranslateDirection(Block preBlock, Block curBlock, Block nxtBlock)
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
                if (preDirection.y < nxtDirection.y)
                {
                    return OverlayGridType.CornerXPosZPos;
                }
    
                return OverlayGridType.CornerXNegZNeg;
            }
    
            if (direction == new Vector2Int(-1, 1))
            {
                if (preDirection.y < nxtDirection.y)
                {
                    return OverlayGridType.CornerXNegZPos;
                }
    
                return OverlayGridType.CornerXPosZNeg;
            }
    
            if (direction == new Vector2Int(1, -1))
            {
                if (preDirection.y < nxtDirection.y)
                {
                    return OverlayGridType.CornerXNegZPos;
                }
    
                return OverlayGridType.CornerXPosZNeg;
            }
    
            if (direction == new Vector2Int(-1, -1))
            {
                if (preDirection.y < nxtDirection.y)
                {
                    return OverlayGridType.CornerXPosZPos;
                }
    
                return OverlayGridType.CornerXNegZNeg;
            }
    
            return OverlayGridType.None;
        }
    }
}
