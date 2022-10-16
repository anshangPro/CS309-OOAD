using System.Collections.Generic;
using System.Linq;
using GameData;
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

        /// <summary>
        /// 当前状态为move,播放移动动画和路径显示，当角色到达目标位置时，进入下一个状态
        /// </summary>
        public void Moving()
        {
            selectedUnit = GameDataManager.Instance.SelectedUnit;
            selectedBlock = GameDataManager.Instance.SelectedBlock;
            Debug.Log($"unit: {selectedUnit}");
            Debug.Log($"block: {selectedBlock}");
            if (selectedUnit.onBlock != selectedBlock)
            {
                selectedUnit.MoveAlongPath(GameData.GameDataManager.Instance.Path);
                selectedUnit.GetComponent<Animator>().SetBool("running", true);
            }
            else if (selectedUnit.onBlock == selectedBlock)
            {
                // EnterMenuAfterMove();
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
    }
}