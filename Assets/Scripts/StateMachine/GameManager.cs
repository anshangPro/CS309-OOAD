using System.Collections.Generic;
using System.Linq;
using GUI;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Util;
using Unit = Units.Unit;

namespace StateMachine
{
    public class GameManager : MonoBehaviour
    {
        public GameStatus Status { get; private set; }

        public static GameManager gameManager { get; private set; }

        public Unit selectedUnit;
        public Unit selectedEnemy;
        public Unit[] pieces;


        public List<Block> movableBlocks; //当前角色能移动的方块
        public Block selectedBlock; //当前玩家选中的方块
        public List<Block> path; //角色的移动路径

        private List<Block> _copyPath = new();


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

        private void LateUpdate()
        {
            Moving();
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
                EnterMenuAfterMove();
                //假设当前只有攻击操作 不能选择技能道具等等
                UIManager.Instance.ShowMenuAfterMove();
            }
        }

        /// <summary>
        /// 显示当前被选中人物的可达路径
        /// </summary>
        /// <returns></returns>
        private bool LightBlocks()
        {
            if (Status != GameStatus.Character || selectedUnit == null) return false;
            // GameObject onBlock = selectedUnit.onBlock; //当前角色所在的方块
            // 所在方块还没有初始化
            movableBlocks = MapManager.Instance.DisplayInRange(selectedUnit);
            return true;
        }


        /// <summary>
        /// 每次棋子被点击的时候调用此方法
        /// </summary>
        /// <param name="unit"> 棋子 </param>
        public void UnitOnClick(Unit unit)
        {
            // 从Default进入待character状态
            if (Status == GameStatus.Default && IsMyPiece(unit))
            {
                selectedUnit = unit;
                EnterCharacter();
                LightBlocks();
            }
            // 在攻击菜单中选择敌人
            else if (Status == GameStatus.FightMenu && !IsMyPiece(unit))
            {
                selectedEnemy = unit;
                EnterFight();
                selectedBlock = null;
            }
        }

        /// <summary>
        /// 每次单元格被点击的时候调用此方法,进行移动
        /// 第一次点击时展示A*最短路径，第二次点击时若方块相同角色移动到目标位置
        /// </summary>
        /// <param name="block"> 单元格 </param>
        public void BlockOnClick(Block block)
        {
            if (Status == GameStatus.Character)
            {
                //第一次点击到当前方块：展示路径
                if (block != selectedBlock)
                {
                    OverlayGridUtil.SetOverlayGridToWhite(path);
                    path = null;
                    selectedBlock = block;
                    Block currentBlock = selectedUnit.onBlock.GetComponent<Block>();
                    path = MapManager.Instance.FindPath(currentBlock, selectedBlock, movableBlocks);
                    MapManager.Instance.DisplayAlongPath(path);
                }
                // 第二次点击到当前方块：移动到目标位置
                else
                {
                    //将可移动的方块清空,选中的方块清空
                    OverlayGridUtil.SetOverlayGridToNone(movableBlocks);
                    movableBlocks = null;
                    path.ForEach(item => _copyPath.Add(item));
                    EnterMove();
                }
            }
        }

        /// <summary>
        /// 点击攻击按钮，由选择菜单进入选择攻击对象的状态
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

        /// <summary>
        /// 当前状态为move,播放移动动画和路径显示，当角色到达目标位置时，进入下一个状态
        /// </summary>
        private void Moving()
        {
            Debug.Log($"unit: {selectedUnit}");
            Debug.Log($"block: {selectedBlock}");
            if (Status == GameStatus.Move && selectedUnit.onBlock != selectedBlock.gameObject)
            {
                selectedUnit.MoveAlongPath(_copyPath);
                selectedUnit.GetComponent<Animator>().SetBool("running", true);
            }
            else if (Status == GameStatus.Move && selectedUnit.onBlock == selectedBlock.gameObject)
            {
                EnterMenuAfterMove();
                UIManager.Instance.ShowMenuAfterMove();
                selectedUnit.GetComponent<Animator>().SetBool("running", false);
            }
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

        private void EnterMenuAfterMove()
        {
            Status = GameStatus.MenuAfterMove;
        }
    }
}