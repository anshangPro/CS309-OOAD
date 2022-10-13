using System;
using Interfaces;
using StateMachine;
using UnityEngine;
using static Util.OverlayGrid;
using Units;

public class Block : MonoBehaviour, IComparable<Block>, IClickable
{
    public int X => (int)transform.localPosition.x;
    public int Z => (int)transform.localPosition.z;
    public Vector2Int Position2D => new(X, Z);
    public bool isWalkable = true;
    public Unit standUnit;
    public float g;
    public float h;

    /// <summary>
    /// a星评估函数
    /// </summary>
    public float F => g + h;

    public Block parent;
    public float moveCost;

    private static readonly Quaternion YRotate0 = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    private static readonly Quaternion YRotate90 = Quaternion.Euler(90.0f, 90.0f, 0.0f);
    private static readonly Quaternion YRotate180 = Quaternion.Euler(90.0f, 180.0f, 0.0f);
    private static readonly Quaternion YRotate270 = Quaternion.Euler(90.0f, 270.0f, 0.0f);

    public void SetOverlayGridType(OverlayGridType type)
    {
        SpriteRenderer overlayRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case OverlayGridType.White:
                overlayRenderer.sprite = WhiteGrid;
                break;
            case OverlayGridType.ArrowXNeg:
                overlayRenderer.sprite = Arrow;
                overlayRenderer.gameObject.transform.rotation = YRotate270;
                break;
            case OverlayGridType.ArrowXPos:
                overlayRenderer.sprite = Arrow;
                overlayRenderer.gameObject.transform.rotation = YRotate90;
                break;
            case OverlayGridType.ArrowZNeg:
                overlayRenderer.sprite = Arrow;
                overlayRenderer.gameObject.transform.rotation = YRotate180;
                break;
            case OverlayGridType.ArrowZPos:
                overlayRenderer.sprite = Arrow;
                break;
            case OverlayGridType.CornerXNegZNeg:
                overlayRenderer.sprite = Corner;
                break;
            case OverlayGridType.CornerXNegZPos:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = YRotate270;
                break;
            case OverlayGridType.CornerXPosZNeg:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = YRotate90;
                break;
            case OverlayGridType.CornerXPosZPos:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = YRotate180;
                break;
            case OverlayGridType.LineX:
                overlayRenderer.sprite = Line;
                overlayRenderer.gameObject.transform.rotation = YRotate90;
                break;
            case OverlayGridType.LineZ:
                overlayRenderer.sprite = Line;
                break;
            default:
                overlayRenderer.sprite = null;
                break;
        }
    }

    public void Flush()
    {
        g = h = 0;
        parent = null;
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().gameObject.transform.rotation = YRotate0;
    }

    public int CompareTo(Block other)
    {
        if (F < other.F)
        {
            return -1;
        }

        if (F > other.F)
        {
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// 当前一个方块被点击
    /// </summary>
    public bool IsClicked()
    {
        GameManager.gameManager.BlockOnClick(this);
        return true;
    }
}