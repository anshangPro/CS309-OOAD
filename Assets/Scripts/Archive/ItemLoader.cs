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
        public Dictionary<int, Tuple<GameObject, BlockDTO>> LocToBlock = new Dictionary<int, Tuple<GameObject, BlockDTO>>();

        private void Awake()
        {
            Tuple<BlockDTO[], EnviromentDTO[]> save = GameLoader.LoadMap("Save/save1.json");
            foreach (BlockDTO block in save.Item1)
            {
                GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
                    Quaternion.identity, gameObject.transform);
                LocToBlock.Add(block.coordinate[0] * 100 + block.coordinate[2], new Tuple<GameObject, BlockDTO>(blockObj, block));
            }

            for (int i = 0; i < 6; i++)
            {
                GameObject unitObj = Instantiate(UnitPrefeb[i]);
                Unit unit = unitObj.GetComponent<Unit>();

                unit.onBlock = LocToBlock[0 * 100 + i * 2].Item1.GetComponent<Block>();
                GameDataManager.Instance.Players[0].UnitsList.Add(unit);
                unitObj = Instantiate(UnitPrefeb[i]);
                unit = unitObj.GetComponent<Unit>();
                unit.onBlock = LocToBlock[2 * 100 + i * 2].Item1.GetComponent<Block>();
                GameDataManager.Instance.Players[1].UnitsList.Add(unit);
            }
            GameDataManager.Instance.blockList = LocToBlock;
        }
    }
}