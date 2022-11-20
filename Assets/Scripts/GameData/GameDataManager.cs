using System;
using System.Collections.Generic;
using DTO;
using Units;
using StateMachine;
using UnityEngine;

namespace GameData
{
    public sealed class GameDataManager
    {
        public static int PlayerNum = 2;
        public int CurrentPlayer = -1;
        public bool Pve = false;
        public List<Player> Players = new List<Player>(PlayerNum);
        public Dictionary<int, Tuple<GameObject, BlockDTO>> blockList;

        public GameStatus gameStatus = GameStatus.Default;

        public Unit SelectedUnit = null;
        public Unit MovedUnit = null;   //移动后需要结算的角色
        public Unit SelectedEnemy = null;

        public List<Block> HighlightBlocks; //角色能移动的方块 或 角色能攻击到的范围
        public Block SelectedBlock = null;       //玩家第一次选中的方块
        public List<Block> Path; //角色的移动路径


        public bool AttackAnimeFinished = false;
        public bool TakeDamageAnimeFinished = false;
        
        
        private static GameDataManager _instance = new();

        public static Tuple<GameObject, BlockDTO> GetBlock(int x, int y)
        {
            return _instance.blockList[100 * x + y];
        }

        private GameDataManager()
        {
            HighlightBlocks = new List<Block>();
            Path = new List<Block>();
            for (int i = 0; i < Players.Capacity; i++)
            {
                Players.Add(new Player());
                Players[i].Index = i;
            }
        }

        public static GameDataManager Instance
        {
            get { return _instance; }
        }

        public Player GetCurrentPlayer()
        {
            return Players[CurrentPlayer];
        }

        public void TurnRound()
        {
            CurrentPlayer = (CurrentPlayer == 0) ? 1 : 0;
            GetCurrentPlayer().FinishedUnit = 0;
        }

    }
}