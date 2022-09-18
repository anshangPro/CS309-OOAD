using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplay
{
    public enum ArrowDirection
    {
        None = 0,
        ArrowXNeg = 1,
        ArrowXPos = 2,
        ArrowZNeg = 3,
        ArrowZPos = 4,
        CornerXNegZNeg = 5,
        CornerXNegZPos = 6,
        CornerXPosZNeg = 7,
        CornerXPosZPos = 8,
        LineX = 9,
        LineZ = 10
    }

    public ArrowDirection TranslateDirection(BaseBlock preBlock, BaseBlock curBlock, BaseBlock nxtBlock)
    {
        bool isEnd = nxtBlock == null;

        Vector2Int preDirection = preBlock != null ? curBlock.Position2D - preBlock.Position2D : new Vector2Int(0, 0);
        Vector2Int nxtDirection = nxtBlock != null ? nxtBlock.Position2D - curBlock.Position2D : new Vector2Int(0, 0);
        Vector2Int direction = preDirection != nxtDirection ? preDirection + nxtDirection : nxtDirection;

        if ((direction == new Vector2Int(1, 0) || direction == new Vector2Int(-1, 0)) && !isEnd)
        {
            return ArrowDirection.LineX;
        }
        
        if ((direction == new Vector2Int(0, 1) || direction == new Vector2Int(0, -1)) && !isEnd)
        {
            return ArrowDirection.LineZ;
        }
        
        if (direction == new Vector2Int(0, 1) && isEnd)
        {
            return ArrowDirection.ArrowZPos;
        }

        if (direction == new Vector2Int(1, 0) && isEnd)
        {
            return ArrowDirection.ArrowXPos;
        }

        if (direction == new Vector2Int(0, -1) && isEnd)
        {
            return ArrowDirection.ArrowZNeg;
        }

        if (direction == new Vector2Int(-1, 0) && isEnd)
        {
            return ArrowDirection.ArrowXNeg;
        }

        if (direction == new Vector2Int(1, 1))
        {
            if (preDirection.y < nxtDirection.y)
            {
                return ArrowDirection.CornerXPosZPos;
            }

            return ArrowDirection.CornerXNegZNeg;
        }

        if (direction == new Vector2Int(-1, 1))
        {
            if (preDirection.y < nxtDirection.y)
            {
                return ArrowDirection.CornerXNegZPos;
            }

            return ArrowDirection.CornerXPosZNeg;
        }

        if (direction == new Vector2Int(1, -1))
        {
            if (preDirection.y < nxtDirection.y)
            {
                return ArrowDirection.CornerXNegZPos;
            }

            return ArrowDirection.CornerXPosZNeg;
        }

        if (direction == new Vector2Int(-1, -1))
        {
            if (preDirection.y < nxtDirection.y)
            {
                return ArrowDirection.CornerXPosZPos;
            }

            return ArrowDirection.CornerXNegZNeg;
        }

        return ArrowDirection.None;
    }
}
