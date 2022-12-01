using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factory;
using DTO;
using GUI;
using GUI.Skills;
using Units;
using StateMachine;
using Units.AI;
using Units.AI.Evaluator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using Unit = Units.Unit;

namespace GameData
{
    public sealed class GameDataManager
    {
        public static int PlayerNum = 2;
        public int CurrentPlayer = -1;
        public bool Pve = false;
        public List<Player> Players = new List<Player>(PlayerNum);
        public Dictionary<Vector2, Tuple<GameObject, BlockDTO>> blockList = new();
        public bool Started = false;

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
        public bool isSave = false; //是否为存档操作
        public readonly string SavePath = "./Save";

        public string JsonToLoad = "Save/SampleScene.json";
        //存档加载槽用

        public readonly Stack<List<List<UnitSnapshot>>> CurPlayerGameHistory = new();


        public bool AttackAnimeFinished = false;
        public bool TakeDamageAnimeFinished = false;

        public readonly Agent Agent = new(new Searcher(new GreedyEvaluator()));


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
            CurPlayerGameHistory.Clear();
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

        // TODO: 快照的方式需要改变，需要处理有人物死亡的情况
        public void TakeSnapshot()
        {
            List<List<UnitSnapshot>> allPlayersSnapshot = Players
                .Select(player => player.UnitsList.Select(unit => new UnitSnapshot(unit)).ToList()).ToList();
            CurPlayerGameHistory.Push(allPlayersSnapshot);
        }

        public static readonly int WithdrawAnime = Animator.StringToHash("withdrawClicked");

        public void WithdrawMove()
        {
            if (CurPlayerGameHistory.Count == 1)
                return;

            Debug.Log("Withdraw move");
            CurPlayerGameHistory.Pop();
            List<List<UnitSnapshot>> config = CurPlayerGameHistory.Peek(); // 棋面格局
            foreach (Player p in Players)
            {
                foreach (Unit unit in p.UnitsList)
                {
                    unit.onBlock.SetOverlayGridType(OverlayGrid.OverlayGridType.None);
                    unit.DestroySelf();
                }

                p.UnitsList.Clear();
            }

            foreach (Block block in MapManager.Instance.Map.Values.ToList())
                block.standUnit = null;


            foreach (UnitSnapshot unitToRecover in config.SelectMany(unitsToRecover => unitsToRecover))
            {
                Unit unit = UnitFactory.Instance.GetUnit(unitToRecover.type);
                unit.CopyFrom(unitToRecover.UnitDto);
                unit.SetTo(unitToRecover);
                unit.ofPlayer = unitToRecover.BelongTo;
                Players[unit.ofPlayer].UnitsList.Add(unit);
            }

            List<Unit> unitAfterLoad = GameDataManager.Instance.GetCurrentPlayer().UnitsList
                .Where(unit => !unit.hasMoved && unit.Health > 0).ToList();
            GetCurrentPlayer().FinishedUnit = GetCurrentPlayer().UnitsList.Count - unitAfterLoad.Count;
            CurPlayerGameHistory.Pop();
            GameManager.gameManager.GetComponent<Animator>().SetTrigger(WithdrawAnime);
        }

        public static readonly int ReloadAnime = Animator.StringToHash("reload");

        public void SkipRound()
        {
            HighlightBlockUtil.DeHighlightSelectableUnitOnBlocks();
            foreach (Unit unit in GetCurrentPlayer().UnitsList)
            {
                unit.OnTurnEnd();
            }
            GetCurrentPlayer().FinishedUnit = GetCurrentPlayer().UnitsList.Count;
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(ReloadAnime);
        }
    }
}