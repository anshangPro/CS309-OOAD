using System;
using System.Collections.Generic;
using DTO;
using GameData;
using Units;
using UnityEngine;

namespace Archive
{
    public class ItemLoader: MonoBehaviour
    {
        /// <summary>
        /// 方块预制体
        /// </summary>
        public List<GameObject> blocks = new List<GameObject>();
        
        /// <summary>
        /// 人物预制体
        /// </summary>
        public List<GameObject> UnitPrefeb = new List<GameObject>();

        /// <summary>
        /// 全部载入方块的gameObject以及类型
        /// </summary>
        public Dictionary<Vector2, Tuple<GameObject, BlockDTO>> LocToBlock = new Dictionary<Vector2, Tuple<GameObject, BlockDTO>>();

        private void Start()
        {
            GameDataManager data = GameDataManager.Instance;
            MapManager map = MapManager.Instance;
            SaveDTO save = GameLoader.LoadSave("Save/save1.json");
            foreach (BlockDTO block in save.blocks)
            {
                GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
                    Quaternion.identity, gameObject.transform);
                LocToBlock.Add(new Vector2(block.coordinate[0], block.coordinate[2]), new Tuple<GameObject, BlockDTO>(blockObj, block));
                map.Map.Add(new Vector2Int(block.coordinate[0], block.coordinate[2]), blockObj.GetComponent<Block>());
            }
            GameDataManager.Instance.blockList = LocToBlock;
            
            foreach (PlayerDTO player in save.Players)
            {
                Player p = new Player(player);
                foreach(UnitDTO u in player.Units)
                {
                    GameObject unitObj = Instantiate(UnitPrefeb[u.type]);
                    Unit unit = unitObj.GetComponent<Unit>();
                    unit.CopyFrom(u);
                    p.UnitsList.Add(unit);
                }
                data.Players[player.Index] = p;
            }

            // for (int i = 0; i < 6; i++)
            // {
            //     GameObject unitObj = Instantiate(UnitPrefeb[i]);
            //     Unit unit = unitObj.GetComponent<Unit>();
            //
            //     unit.onBlock = LocToBlock[new Vector2(0, i * 2)].Item1.GetComponent<Block>();
            //     GameDataManager.Instance.Players[0].UnitsList.Add(unit);
            //     unitObj = Instantiate(UnitPrefeb[i]);
            //     unit = unitObj.GetComponent<Unit>();
            //     unit.onBlock = LocToBlock[new Vector2(2, i * 2)].Item1.GetComponent<Block>();
            //     GameDataManager.Instance.Players[1].UnitsList.Add(unit);
            // }
        }
    }
}