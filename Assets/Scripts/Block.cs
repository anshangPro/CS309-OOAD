using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int X => (int) transform.localPosition.x;
    public int Z => (int) transform.localPosition.z;
    public Vector2Int Position2D => new Vector2Int(X, Z);

    public int G;
    public int H;
    public int F => G + H;
    
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

    public void SetOverlayGridType(OverlayGridType type)
    {
        Sprite arrow = Resources.Load<Sprite>("Sprites/Arrow");
        Sprite corner = Resources.Load<Sprite>("Sprites/Corner");
        Sprite line = Resources.Load<Sprite>("Sprites/Line");

        Quaternion yRotate90 = Quaternion.Euler(90.0f, 90.0f, 0.0f);
        Quaternion yRotate180 = Quaternion.Euler(90.0f, 180.0f, 0.0f);
        Quaternion yRotate270 = Quaternion.Euler(90.0f, 270.0f, 0.0f);
        
        SpriteRenderer overlayRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case OverlayGridType.White:
                overlayRenderer.sprite = Resources.Load<Sprite>("Sprites/WhiteGrid");
                break;
            case OverlayGridType.ArrowXNeg:
                overlayRenderer.sprite = arrow;
                overlayRenderer.gameObject.transform.rotation = yRotate270;
                break;
            case OverlayGridType.ArrowXPos:
                overlayRenderer.sprite = arrow;
                overlayRenderer.gameObject.transform.rotation = yRotate90;
                break;
            case OverlayGridType.ArrowZNeg:
                overlayRenderer.sprite = arrow;
                overlayRenderer.gameObject.transform.rotation = yRotate180;
                break;
            case OverlayGridType.ArrowZPos:
                overlayRenderer.sprite = arrow;
                break;
            case OverlayGridType.CornerXNegZNeg:
                overlayRenderer.sprite = corner;
                break;
            case OverlayGridType.CornerXNegZPos:
                overlayRenderer.sprite = corner;
                overlayRenderer.gameObject.transform.rotation = yRotate270;
                break;
            case OverlayGridType.CornerXPosZNeg:
                overlayRenderer.sprite = corner;
                overlayRenderer.gameObject.transform.rotation = yRotate90;
                break;
            case OverlayGridType.CornerXPosZPos:
                overlayRenderer.sprite = corner;
                overlayRenderer.gameObject.transform.rotation = yRotate180;
                break;
            case OverlayGridType.LineX:
                overlayRenderer.sprite = line;
                overlayRenderer.gameObject.transform.rotation = yRotate90;
                break;
            case OverlayGridType.LineZ:
                overlayRenderer.sprite = line;
                break;
            default:
                overlayRenderer.sprite = null;
                break;
        }
    }
}
