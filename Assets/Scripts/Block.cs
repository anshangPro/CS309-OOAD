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
    public int type;
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
    private static readonly int EnemyClicked = Animator.StringToHash("enemyClicked");

    public void SetHighlightColor(Color color)
    {
        SpriteRenderer spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = WhiteGrid;
        spriteRenderer.color = color;
    }

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
                overlayRenderer.color = Color.white;
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
    /// 当前角色是否可处于可被选择的状态下
    /// </summary>
    /// <param name="status"></param>
    /// <returns>boolean </returns>
    private bool CanChoose(GameStatus status)
    {
        List<GameStatus> gameStatusList = new List<GameStatus>()
            { GameStatus.Default, GameStatus.BlockSelected, GameStatus.UnitChosen, GameStatus.FightMenu };
        return gameStatusList.Contains(status);
    }

    /// <summary>
    /// 每次单元格被点击的时候调用此方法,进行移动
    /// 第一次点击时展示A*最短路径，第二次点击时若方块相同角色移动到目标位置
    /// </summary>
    public bool IsClicked()
    {
        GameDataManager gameData = GameDataManager.Instance;
        if (CanChoose(gameData.gameStatus))
        {
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            if (gameData.gameStatus == GameStatus.FightMenu)
            {
                if (standUnit is not null && standUnit.CanFightWith())
                {
                    gameData.SelectedEnemy = this.standUnit;
                    animator.SetTrigger(EnemyClicked);
                    return true;
                }
                
            }
            else if (gameData.SelectedBlock == this && gameData.HighlightBlocks.Contains(this)) // 当前方块是第二次被点击
            {
                animator.SetTrigger(BlockConfirmed);
            }
            else if (standUnit is null)
            {
                gameData.SelectedBlock = this;
                animator.SetTrigger(BlockSelected);
            }
            return true;
        }
        return false;
    }
}