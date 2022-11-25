using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public List<GameObject> EnvironmentPrefab = new();

        public Dictionary<string, GameObject> EnvironmentPrefabDict = new();

        /// <summary>
        /// 全部载入方块的gameObject以及类型
        /// </summary>
        public Dictionary<Vector2, Tuple<GameObject, BlockDTO>> LocToBlock = new Dictionary<Vector2, Tuple<GameObject, BlockDTO>>();

        private void Awake()
        {
            foreach (GameObject environment in EnvironmentPrefab)
            {
                EnvironmentPrefabDict.Add(environment.name, environment);
            }
        }

        private void Start()
        {
            GameDataManager data = GameDataManager.Instance;
            MapManager map = MapManager.Instance;
            SaveDTO save = GameLoader.LoadSave(GameDataManager.Instance.JsonToLoad);
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
            
            // TODO: 加载方式换成这样
            // SaveDTO save = GameLoader.LoadSave(GameDataManager.Instance.JsonToLoad);
            // LoadBlocksFrom(save);
            // LoadPlayersFrom(save);
            // LoadEnvironmentFrom(save);

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

        private void LoadBlocksFrom([NotNull] SaveDTO saveDto)
        {
            MapManager map = MapManager.Instance;
            foreach (BlockDTO block in saveDto.blocks)
            {
                GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
                    Quaternion.identity, gameObject.transform);
                blockObj.GetComponent<Block>().isWalkable = block.isWalkable;
                LocToBlock.Add(new Vector2(block.coordinate[0], block.coordinate[2]), new Tuple<GameObject, BlockDTO>(blockObj, block));
                map.Map.Add(new Vector2Int(block.coordinate[0], block.coordinate[2]), blockObj.GetComponent<Block>());
            }
            GameDataManager.Instance.blockList = LocToBlock;
        }

        private void LoadEnvironmentFrom([NotNull] SaveDTO saveDto)
        {
            if (saveDto.environment == null || saveDto.environment.Count == 0)
                return;
            
            foreach (EnviromentDTO enviromentDto in saveDto.environment)
            {
                GameObject gameObj = EnvironmentPrefabDict[enviromentDto.type];
                for (int i = 0; i < enviromentDto.coordinates.Length; i++)
                {
                    GameObject block = Instantiate(gameObj, gameObject.transform);
                    block.transform.position = new Vector3(enviromentDto.coordinates[i][0], enviromentDto.coordinates[i][1],
                        enviromentDto.coordinates[i][2]);
                    block.transform.rotation = Quaternion.identity;
                }
            }
        }

        private void LoadPlayersFrom([NotNull] SaveDTO saveDto)
        {
            if (saveDto.Players == null)
                return;

            GameDataManager data = GameDataManager.Instance;
            foreach (PlayerDTO player in saveDto.Players)
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
        }
    }
}