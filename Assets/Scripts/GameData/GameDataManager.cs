using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using GUI;
using GUI.Skills;
using Units;
using StateMachine;
using Units.AI;
using Units.AI.Evaluator;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public Unit MovedUnit = null; //移动后需要结算的角色
        public Unit SelectedEnemy = null;

        public List<Block> HighlightBlocks; //角色能移动的方块 或 角色能攻击到的范围
        public Block SelectedBlock = null; //玩家第一次选中的方块
        public List<Block> Path; //角色的移动路径
        public List<Block> SelectableUnitOnBlocks = new(); //用于指示当前玩家可选角色的绿色高亮方块

        //技能用私有地
        public SkillOption SelectedSkill; //使用什么技能
        public Unit SkillAffected; //对谁使用这个技能

        public bool PanelShowing = false;
        //技能用私有地
        //存档加载槽用
        public readonly string SavePath = "./Save";
        public string JsonToLoad = "Save/SampleScene.json";
        //存档加载槽用

        //玩家背包
        public readonly Backpack Backpack = new();


        public bool AttackAnimeFinished = false;
        public bool TakeDamageAnimeFinished = false;

        public readonly Agent Agent = new(new Searcher(new GreedyEvaluator()));
        public const bool RobotTest = true;


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

        public bool IsGameOver()
        {
            return GetCurrentPlayer().UnitsList.Count(unit => unit.Health > 0) == 0 ||
                   GetOppositePlayer().UnitsList.Count(unit => unit.Health > 0) == 0;
        }

        public bool ShouldAgentOperate()
        {
            return Pve && GetCurrentPlayer().IsRobot && !IsGameOver();
        }

        public void GameOver()
        {
            UIManager.Instance.GameOverMenuUI.SetActive(true);
            int winnerPlayerId = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].UnitsList.Count(unit => unit.Health > 0) > 0)
                {
                    winnerPlayerId = i;
                }
            }
            UIManager.Instance.WinnerPlayerIDText.text = winnerPlayerId.ToString();
        }
    }
}