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