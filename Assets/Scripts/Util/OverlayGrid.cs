using UnityEngine;

namespace Util
{
    public static class OverlayGrid
    {
        public enum OverlayGridType
        {
            None = 0,
            White = 1,
            ArrowXNeg = 2,
            ArrowXPos = 3,
            ArrowZNeg = 4,
            ArrowZPos = 5,
            CornerXNegZNeg = 6,
            CornerXNegZPos = 7,
            CornerXPosZNeg = 8,
            CornerXPosZPos = 9,
            LineX = 10,
            LineZ = 11
        }

        public static readonly Sprite Arrow = Resources.Load<Sprite>("Sprites/Arrow");
        public static readonly Sprite Corner = Resources.Load<Sprite>("Sprites/Corner");
        public static readonly Sprite Line = Resources.Load<Sprite>("Sprites/Line");
        public static readonly Sprite WhiteGrid = Resources.Load<Sprite>("Sprites/WhiteGrid");
    }
}