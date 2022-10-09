using Units;
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


        private void FixedUpdate()
        {
            WithdrawChoice();
            LightBlocks();
        }

        public void MenuButtonOnClick()
        {
            if (Status == GameStatus.Default)
            {
                EnterMainMenu();
            }
            else
            {
                Debug.Log("Fail to go to MainMenu");
            }
        }

        public void BackButtonOnClick()
        {
            switch (Status)
            {
                case GameStatus.MainMenu:
                    EnterDefault();
                    break;
                ////存在其他的BackButton
                default:
                    Debug.Log("Incorrect status");
                    break;
            }
        }

        //在两个可能的状态下撤销操作
        private void WithdrawChoice()
        {
            //右键撤销选择当前角色
            if (Status is GameStatus.Character && Input.GetMouseButton(1))
            {
                selectedUnit = null;
                EnterDefault();
            }

            //右键撤销使用攻击/技能/道具
            if (Status is GameStatus.FightMenu && Input.GetMouseButton(1))
            {
                ////
            }
        }

        private bool LightBlocks()
        {
            if (Status == GameStatus.Character && selectedUnit != null)
            {
                Debug.Log(selectedUnit.onBlock.name);
                GameObject onBlock = selectedUnit.onBlock; //当前角色所在的方块
                MapManager.Instance.DisplayInRange(selectedUnit);
                return true;
            }

            return false;
        }


        /// <summary>
        /// 每次棋子被点击的时候调用此方法
        /// </summary>
        /// <param name="unit"> 棋子 </param>
        public void UnitOnClick(Unit unit)
        {
            // 从Default进入待移动状态
            if (Status == GameStatus.Default && IsMyPiece(unit))
            {
                selectedUnit = unit;
                EnterCharacter();
            }
            // 在攻击菜单中选择敌人
            else if (Status == GameStatus.FightMenu && !IsMyPiece(unit))
            {
                selectedEnemy = unit;
                EnterFight();
            }
        }

        /// <summary>
        /// 每次单元格被点击的时候调用此方法,进行移动
        /// </summary>
        /// <param name="block"> 单元格 </param>
        public void BlockOnClick(GameObject block)
        {
            if (Status == GameStatus.Character)
            {
                // if (cell.Moveable(selectedUnit))
                // {
                //     selectedUnit.Move(arg1, arg2, arg3);
                //     EnterMoving();
                // }
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

        private class StatusController
        {
        }

        private bool IsMyPiece(Unit piece)
        {
            return (mainPlayer == 0 && piece is Friendly) || (mainPlayer == 1 && piece is Enemy);
        }


        /// <summary>
        /// 用于轮转状态机的接口
        /// </summary>
        private void EnterDefault()
        {
            Status = GameStatus.Default;
        }

        private void EnterMainMenu()
        {
            Status = GameStatus.MainMenu;
        }

        private void EnterCharacter()
        {
            Status = GameStatus.Character;
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