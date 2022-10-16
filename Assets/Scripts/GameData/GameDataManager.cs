using System.Collections.Generic;
using Units;

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

        public Unit SelectedUnit = null;
        public Unit SelectedEnemy = null;
        public Unit[] Pieces = null;


        public List<Block> MovableBlocks = new(); //当前角色能移动的方块
        public Block SelectedBlock = null;       //玩家第一次选中的方块
        public List<Block> Path = new(); //角色的移动路径

        public List<Block> CopyPath = new();


        public int MainPlayer = 0;
        public int NextPlayer = 0;
    }
}