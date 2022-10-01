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
    private static GameManager _gameManager;
    public static GameManager gameManager
    {
        get
        {
            return _gameManager;
        }
    }

    public Unit selectedUnit;
    public Unit selectedEnemy;
    public Unit[] pieces;

    public int mainPlayer;
    public int nextPlayer;

    void Awake()
    {
        if (_gameManager != null && _gameManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _gameManager = this;
        }
        //pieces = ??
        selectedUnit = null;
        _status = GameStatus.Default;
        mainPlayer = 0;
        nextPlayer = 1;
    }


    void Update()
    {
        //右键回到default,已选中清空
        if (Input.GetMouseButton(1))
        {
            selectedUnit = null;
            EnterDefault();
        }
    }


    /// <summary>
    /// 每次棋子被点击的时候调用此方法
    /// </summary>
    /// <param name="piece"> 棋子 </param>
    public void PieceOnClick(Unit piece)
    {
        if (_status == GameStatus.Default)
        {
            EnterMainMenu();
            selectedUnit = piece;
        }
        else if (_status == GameStatus.Fight)
        {
            selectedUnit.Attack(piece);
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
            //if (cell.Moveable(selectedUnit))
            //{
            //    selectedUnit.Move(arg1, arg2, arg3);
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
        selectedUnit = null;

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

    void EnterMainMenu()
    {
        _status = GameStatus.MainMenu;
    }

    void EnterMove()
    {
        _status = GameStatus.Move;
    }

    void EnterFight()
    {
        _status = GameStatus.Fight;
    }

    void EnterFightMenu()
    {
        _status = GameStatus.FightMenu;
    }
}
