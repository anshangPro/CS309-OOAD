using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using static Util.OverlayGrid;

public class Block : MonoBehaviour
{
    public int X => (int) transform.localPosition.x;
    public int Z => (int) transform.localPosition.z;
    public Vector2Int Position2D => new(X, Z);

    public int G;
    public int H;
    public int F => G + H;

    
    private static readonly Quaternion _yRotate90 = Quaternion.Euler(90.0f, 90.0f, 0.0f);
    private static readonly Quaternion _yRotate180 = Quaternion.Euler(90.0f, 180.0f, 0.0f);
    private static readonly Quaternion _yRotate270 = Quaternion.Euler(90.0f, 270.0f, 0.0f);
    
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
                overlayRenderer.gameObject.transform.rotation = _yRotate270;
                break;
            case OverlayGridType.ArrowXPos:
                overlayRenderer.sprite = Arrow;
                overlayRenderer.gameObject.transform.rotation = _yRotate90;
                break;
            case OverlayGridType.ArrowZNeg:
                overlayRenderer.sprite = Arrow;
                overlayRenderer.gameObject.transform.rotation = _yRotate180;
                break;
            case OverlayGridType.ArrowZPos:
                overlayRenderer.sprite = Arrow;
                break;
            case OverlayGridType.CornerXNegZNeg:
                overlayRenderer.sprite = Corner;
                break;
            case OverlayGridType.CornerXNegZPos:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = _yRotate270;
                break;
            case OverlayGridType.CornerXPosZNeg:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = _yRotate90;
                break;
            case OverlayGridType.CornerXPosZPos:
                overlayRenderer.sprite = Corner;
                overlayRenderer.gameObject.transform.rotation = _yRotate180;
                break;
            case OverlayGridType.LineX:
                overlayRenderer.sprite = Line;
                overlayRenderer.gameObject.transform.rotation = _yRotate90;
                break;
            case OverlayGridType.LineZ:
                overlayRenderer.sprite = Line;
                break;
            default:
                overlayRenderer.sprite = null;
                break;
        }
    }
}
