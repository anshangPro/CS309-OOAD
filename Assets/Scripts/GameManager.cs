using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private GameStatus _status;
    public GameStatus status
    {
        get
        {
            return _status;
        }
    }

    public Unit selected;
    public Unit[] pieces;

    public int mainPlayer;
    public int nextPlayer;

    void Awake()
    {
        //pieces = ??
        selected = null;
        _status = GameStatus.Default;
        mainPlayer = 0;
        nextPlayer = 1;
    }


    void Update()
    {
        //右键回到default,已选中清空
        if (Input.GetMouseButton(1))
        {
            selected = null;
            EnterDefault();
        }
    }


    /// <summary>
    /// 每次棋子被点击的时候调用此方法
    /// </summary>
    /// <param name="piece"> 棋子 </param>
    public void PieceOnClick(Unit piece)
    {
        if (_status == GameStatus.Default || _status == GameStatus.Menu)
        {
            EnterMenu();
            selected = piece;
        }
        else if (_status == GameStatus.Fight)
        {
            selected.Attack(piece);
            EnterDefault();
        }
    }


    /// <summary>
    /// 每次单元格被点击的时候调用此方法
    /// </summary>
    /// <param name="cell"> 单元格 </param>
    public void CellOnClick(GameObject cell)
    {
        if (_status == GameStatus.Move)
        {   
            //if (cell.Moveable(selected))
            //{
            //    selected.Move(arg1, arg2, arg3);
            //    EnterMoving();
            //}
        }
    }

    public void Moving()
    {

    }


    /// <summary>
    /// 交换玩家，重设棋子行动力
    /// </summary>
    public void TurnEnd()
    {
        selected = null;

        foreach (Unit piece in pieces)
        {
            //把每个棋子的hasMoved属性设为false
            //piece.Deploy();
        }

        //交换玩家
        int temp = mainPlayer;
        mainPlayer = nextPlayer;
        nextPlayer = temp;

        Debug.Log("Turn end, now player is: " + mainPlayer);
    }

    void EnterDefault()
    {
        _status = GameStatus.Default;
    }

    void EnterMoving()
    {
        _status = GameStatus.Moving;
    }

    void EnterMenu()
    {
        _status = GameStatus.Menu;
    }

    void EnterMove()
    {
        _status = GameStatus.Move;
    }

    void EnterFight()
    {
        _status = GameStatus.Fight;
    }
}
