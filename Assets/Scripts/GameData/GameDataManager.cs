using System.Collections.Generic;
using Units;
using StateMachine;

namespace GameData
{
    public sealed class GameDataManager
    {
        public static int PlayerNum = 2;
        public int MainPlayer = 0;
        public int NextPlayer = 1;
        public int CurrentPlayer = -1;


        public GameStatus gameStatus = GameStatus.Default;

        public Unit SelectedUnit = null;
        public Unit MovedUnit = null;   //移动后需要结算的角色
        public Unit SelectedEnemy = null;
        public List<Unit>[] UnitsOfPlayers;


        public List<Block> HighlightBlocks; //角色能移动的方块 或 角色能攻击到的范围
        public Block SelectedBlock = null;       //玩家第一次选中的方块
        public List<Block> Path; //角色的移动路径
        
        
        private static GameDataManager _instance = new();

        private GameDataManager()
        {
            UnitsOfPlayers = new List<Unit>[PlayerNum];
            HighlightBlocks = new List<Block>();
            Path = new List<Block>();
        }

        public static GameDataManager Instance
        {
            get { return _instance; }
        }

    }
}