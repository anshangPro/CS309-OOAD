using System.Collections.Generic;
using Units;
using StateMachine;

namespace GameData
{
    public sealed class GameDataManager
    {
        private static GameDataManager _instance = null;

        private GameDataManager()
        {
        }

        public static GameDataManager Instance
        {
            get { return _instance ??= new GameDataManager(); }
        }

        public GameStatus gameStatus = GameStatus.Default;

        public Unit SelectedUnit = null;
        public Unit SelectedEnemy = null;
        public List<Unit>[] UnitsOfPlayers = new List<Unit>[PlayerNum];


        public List<Block> MovableBlocks = new(); //当前角色能移动的方块
        public Block SelectedBlock = null;       //玩家第一次选中的方块
        public List<Block> Path = new(); //角色的移动路径


        public static int PlayerNum = 2;
        public int MainPlayer = 0;
        public int NextPlayer = 1;
    }
}