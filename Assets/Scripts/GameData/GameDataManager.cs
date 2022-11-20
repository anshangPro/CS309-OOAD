using System;
using System.Collections.Generic;
using DTO;
using Units;
using StateMachine;
using Units.AI;
using Units.AI.Evaluator;
using UnityEngine;

namespace GameData
{
    public sealed class GameDataManager
    {
        public static int PlayerNum = 2;
        public int CurrentPlayer = -1;
        public bool Pve = false;
        public List<Player> Players = new List<Player>(PlayerNum);
        public Dictionary<Vector2, Tuple<GameObject, BlockDTO>> blockList;

        public GameStatus gameStatus = GameStatus.Default;

        public Unit SelectedUnit = null;
        public Unit MovedUnit = null;   //移动后需要结算的角色
        public Unit SelectedEnemy = null;

        public List<Block> HighlightBlocks; //角色能移动的方块 或 角色能攻击到的范围
        public Block SelectedBlock = null;       //玩家第一次选中的方块
        public List<Block> Path; //角色的移动路径


        public bool AttackAnimeFinished = false;
        public bool TakeDamageAnimeFinished = false;

        public Searcher searcher = new(new GreedyEvaluator());
        
        
        private static GameDataManager _instance = new();

        public Tuple<GameObject, BlockDTO> GetBlock(int x, int y)
        {
            Vector2Int dst = new Vector2Int(x, y);
            return blockList.ContainsKey(dst) ? blockList[dst] : null;
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

        public Player GetOppositePlayer()
        {
            return CurrentPlayer == 0 ? Players[1] : Players[0];
        }

        public void TurnRound()
        {
            CurrentPlayer = (CurrentPlayer == 0) ? 1 : 0;
            GetCurrentPlayer().FinishedUnit = 0;
        }

    }
}