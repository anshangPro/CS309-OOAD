using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using DTO;
using GameData;
using StateMachine;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Archive
{
    public class ItemLoader: MonoBehaviour
    {
        public static ItemLoader Instance { get; private set; }
        
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
        
        private static readonly int ReloadAnime = Animator.StringToHash("reload");

        public List<AudioClip> bgmCandidate;
        public AudioClip beenHitSound;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            foreach (GameObject environment in EnvironmentPrefab)
            {
                EnvironmentPrefabDict.Add(environment.name, environment);
            }
            
            // TODO: 记得删掉这里
            if (SceneManager.GetActiveScene().name != "Scene_0") return;
            MapSaver.SaveAll();
            Debug.Log("save successfully");
            UnityEditor.EditorApplication.isPlaying = false;
        }

        public void Start()
        {
            // GameDataManager data = GameDataManager.Instance;
            // MapManager map = MapManager.Instance;
            // SaveDTO save = GameLoader.LoadSave(GameDataManager.Instance.JsonToLoad);
            // foreach (BlockDTO block in save.blocks)
            // {
            //     GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
            //         Quaternion.identity, gameObject.transform);
            //     LocToBlock.Add(new Vector2(block.coordinate[0], block.coordinate[2]), new Tuple<GameObject, BlockDTO>(blockObj, block));
            //     map.Map.Add(new Vector2Int(block.coordinate[0], block.coordinate[2]), blockObj.GetComponent<Block>());
            // }
            // GameDataManager.Instance.blockList = LocToBlock;
            //
            // foreach (PlayerDTO player in save.Players)
            // {
            //     Player p = new Player(player);
            //     foreach(UnitDTO u in player.Units)
            //     {
            //         GameObject unitObj = Instantiate(UnitPrefeb[u.type]);
            //         Unit unit = unitObj.GetComponent<Unit>();
            //         unit.CopyFrom(u);
            //         p.UnitsList.Add(unit);
            //     }
            //     data.Players[player.Index] = p;
            // }
            
            // TODO: 加载方式换成这样
            SaveDTO save = GameLoader.LoadSave(GameDataManager.Instance.JsonToLoad);
            LoadBlocksFrom(save);
            LoadEnvironmentFrom(save);
            LoadPlayersFrom(save);

            Camera.main!.GetComponent<AudioSource>().clip = bgmCandidate.OrderBy(bgm => Guid.NewGuid()).First();
            Debug.Log($"Total {bgmCandidate.Count} bgm candidates");
            Camera.main!.GetComponent<AudioSource>().Play();

            // MapSaver.SaveAll();

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

        public void GoDefault(string save)
        {
            GameDataManager data = GameDataManager.Instance;
            MapManager map = MapManager.Instance;
            data.gameStatus = GameStatus.Default;
            data.CurrentPlayer = -1;
            data.blockList.Clear();
            data.Pve = false;
            
            data.MovedUnit = null;
            data.SelectedUnit = null;
            data.MovedUnit = null;
            data.SelectedEnemy = null;
            
            data.HighlightBlocks.Clear();
            data.SelectedBlock = null;
            data.Path.Clear();
            data.SelectableUnitOnBlocks.Clear();
            
            data.SelectedSkill = null;
            data.SkillAffected = null;
            foreach (Player player in data.Players)
            {
                foreach (Unit unit in player.UnitsList)
                {
                    Destroy(unit.gameObject);
                }
                player.UnitsList.Clear();
            }

            foreach (GameObject env in map.environment)
            {
                Destroy(env);
            }
            map.environment.Clear();

            foreach (KeyValuePair<Vector2Int, Block> blockPair in map.Map)
            {
                Destroy(blockPair.Value.gameObject);
            }
            map.Map.Clear();
            data.JsonToLoad = save;
            Start();
            Animator animator = GameManager.gameManager.GetComponent<Animator>();
            animator.SetTrigger(ReloadAnime);
        }

        private void LoadBlocksFrom([NotNull] SaveDTO saveDto)
        {
            MapManager map = MapManager.Instance;
            foreach (BlockDTO block in saveDto.blocks)
            {
                GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
                    Quaternion.identity, gameObject.transform);
                blockObj.GetComponent<Block>().isWalkable = block.isWalkable;
                if (LocToBlock.Keys.ToList().Contains(new Vector2(block.coordinate[0], block.coordinate[2])))
                    continue;
                
                LocToBlock.Add(new Vector2(block.coordinate[0], block.coordinate[2]), new Tuple<GameObject, BlockDTO>(blockObj, block));
                Block b = blockObj.GetComponent<Block>();
                map.Map.Add(new Vector2Int(block.coordinate[0], block.coordinate[2]), b);
                if (b.type == 3)
                {
                    map.beacons.Add(b);
                }
            }
            GameDataManager.Instance.blockList = LocToBlock;
        }

        private void LoadEnvironmentFrom([NotNull] SaveDTO saveDto)
        {
            if (saveDto.environment == null || saveDto.environment.Count == 0)
                return;
            
            foreach (EnviromentDTO environmentDto in saveDto.environment)
            {
                GameObject gameObj = EnvironmentPrefabDict[environmentDto.type];
                for (int i = 0; i < environmentDto.coordinates.Length; i++)
                {
                    GameObject block = Instantiate(gameObj, gameObject.transform);
                    MapManager.Instance.environment.Add(block);
                    block.transform.position = new Vector3(environmentDto.coordinates[i][0], environmentDto.coordinates[i][1],
                        environmentDto.coordinates[i][2]);
                    block.transform.rotation = Quaternion.Euler(environmentDto.rotation[i][0],
                        environmentDto.rotation[i][1],
                        environmentDto.rotation[i][2]);
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