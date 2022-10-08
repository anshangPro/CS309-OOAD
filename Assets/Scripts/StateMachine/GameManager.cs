using UnityEngine;

namespace StateMachine
{
    public class GameManager : MonoBehaviour
    {
        public GameStatus Status { get; private set; }

        public static GameManager gameManager { get; private set; }

        public Unit selectedUnit;
        public Unit selectedEnemy;
        public Unit[] pieces;

        public int mainPlayer;
        public int nextPlayer;

        private void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                gameManager = this;
            }

            //pieces = ??
            selectedUnit = null;
            Status = GameStatus.Default;
            mainPlayer = 0;
            nextPlayer = 1;
        }


        private void Update()
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
            // 从Default进入待移动状态
            if (Status == GameStatus.Default && IsMyPiece(piece))
            {
                EnterMove();
                selectedUnit = piece;
            }
            // 在攻击菜单中选择敌人
            else if (Status == GameStatus.FightMenu && !IsMyPiece(piece))
            {
                selectedEnemy = piece;
            }
        }

        /// <summary>
        /// 每次单元格被点击的时候调用此方法,进行移动
        /// </summary>
        /// <param name="cell"> 单元格 </param>
        public void CellOnClick(GameObject cell)
        {
            if (Status == GameStatus.Move)
            {
                //if (cell.Moveable(selectedUnit))
                //{
                //    selectedUnit.Move(arg1, arg2, arg3);
                //    EnterMoving();
                //}
            }
        }

        /// <summary>
        /// 点击攻击按钮，从人物菜单进入攻击菜单
        /// </summary>
        public void AttackButtonOnClick()
        {
            if (Status == GameStatus.MenuAfterMove)
            {
                EnterFightMenu();
            }
        }


        /// <summary>
        /// 选定物品和敌人后，点击确定开始攻击
        /// </summary>
        public void StartAttack()
        {
            if (Status == GameStatus.FightMenu && selectedEnemy != null)
            {
                EnterFight();
                selectedUnit.Attack(selectedEnemy);
            }
        }

        /// <summary>
        /// 攻击结算后回到Default状态
        /// </summary>
        public void FinishAttack()
        {
            EnterDefault();
            selectedEnemy = null;
            selectedUnit = null;
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
            (mainPlayer, nextPlayer) = (nextPlayer, mainPlayer);

            Debug.Log("Turn end, now player is: " + mainPlayer);
        }

        private bool IsMyPiece(Unit piece)
        {
            return (mainPlayer == 0 && piece is Friendly) || (mainPlayer == 1 && piece is Enemy);
        }

        private void EnterDefault()
        {
            Status = GameStatus.Default;
        }

        private void EnterMainMenu()
        {
            Status = GameStatus.MainMenu;
        }

        private void EnterMove()
        {
            Status = GameStatus.Move;
        }

        private void EnterFight()
        {
            Status = GameStatus.Fight;
        }

        private void EnterFightMenu()
        {
            Status = GameStatus.FightMenu;
        }
    }
}