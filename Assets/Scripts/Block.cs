using System;
using System.Collections.Generic;
using GameData;
using Interfaces;
using StateMachine;
using UnityEngine;
using static Util.OverlayGrid;
using Units;
using Util;

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
    private static readonly int BlockConfirmed = Animator.StringToHash("blockConfirmed");
    private static readonly int BlockSelected = Animator.StringToHash("blockSelected");

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
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().gameObject.transform.rotation =
            YRotate0;
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
    /// 每次单元格被点击的时候调用此方法,进行移动
    /// 第一次点击时展示A*最短路径，第二次点击时若方块相同角色移动到目标位置
    /// </summary>
    public bool IsClicked()
    {
        GameDataManager gameData = GameDataManager.Instance;
        Animator animator = GameManager.gameManager.GetComponent<Animator>();
        // 当前方块是第二次被点击
        if (gameData.SelectedBlock == this && gameData.MovableBlocks.Contains(this))
        {
            //将可移动的方块清空,选中的方块清空
            OverlayGridUtil.SetOverlayGridToNone(gameData.MovableBlocks);
            gameData.MovableBlocks.Clear();
            gameData.SelectedBlock = null;
            gameData.Path.ForEach(item => gameData.CopyPath.Add(item));
            animator.SetTrigger(BlockConfirmed);
        }
        else
        {
            // 设置路径
            if (gameData.Path != null)
            {
                OverlayGridUtil.SetOverlayGridToWhite(gameData.Path);
            }

            gameData.Path = null;

            if (gameData.SelectedUnit != null)
            {
                Block currentBlock = gameData.SelectedUnit.onBlock;
                gameData.Path = MapManager.Instance.FindPath(currentBlock, this, gameData.MovableBlocks);
                MapManager.Instance.DisplayAlongPath(gameData.Path);
            }
            animator.SetTrigger(BlockSelected);
        }

        gameData.SelectedBlock = this;


        // // TODO Validate double click
        // // TODO GameData
        // if (Util.StateMachine.GetCurrentStatus(animator) == GameStatus.UnitChosen.ToString())
        // {
        //     OverlayGridUtil.SetOverlayGridToWhite(path);
        //     path = null;
        //     selectedBlock = block;
        //     Block currentBlock = selectedUnit.onBlock.GetComponent<Block>();
        //     path = MapManager.Instance.FindPath(currentBlock, selectedBlock, movableBlocks);
        //     MapManager.Instance.DisplayAlongPath(path);
        // }
        //
        // //第一次点击到当前方块：展示路径
        // if (this != GameDataManager.Instance.SelectedBlock)
        // {
        //     OverlayGridUtil.SetOverlayGridToWhite(path);
        //     path = null;
        //     selectedBlock = block;
        //     Block currentBlock = selectedUnit.onBlock.GetComponent<Block>();
        //     path = MapManager.Instance.FindPath(currentBlock, selectedBlock, movableBlocks);
        //     MapManager.Instance.DisplayAlongPath(path);
        // }
        // // 第二次点击到当前方块：移动到目标位置
        // else
        // {
        //     //将可移动的方块清空,选中的方块清空
        //     OverlayGridUtil.SetOverlayGridToNone(movableBlocks);
        //     movableBlocks = null;
        //     path.ForEach(item => _copyPath.Add(item));
        //     EnterMove();
        // }

        return true;
    }
}