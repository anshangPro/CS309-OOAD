using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DTO;
using GameData;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Units;
using UnityEditor;
using UnityEngine;

namespace Archive
{
    public class MapSaver : MonoBehaviour
    {
        private void Start()
        {
            // SaveAll();
            // 上面这行代码用于unity编辑器编辑地图导出
        }

        private static Regex _regex = new Regex("save(\\d+)\\.json");

        public static void Save()
        {
            GameDataManager data = GameDataManager.Instance;
            SaveDTO save = new SaveDTO();
            List<PlayerDTO> players = new List<PlayerDTO>();
            foreach (Player p in data.Players)
            {
                PlayerDTO player = new PlayerDTO(p);
                players.Add(player);
            }

            save.Players = players;
            save.blocks = new List<BlockDTO>();
            
            foreach (KeyValuePair<Vector2, Tuple<GameObject, BlockDTO>> pair in data.blockList)
            {
                save.blocks.Add(pair.Value.Item2);
            }
            save.environment = new List<EnviromentDTO>();
            List<IGrouping<string,GameObject>> partitionList = MapManager.Instance.environment.
                GroupBy(environment => environment.GetComponent<Environment>().type).ToList();
            foreach (IGrouping<string,GameObject> environmentsInOneGroup in partitionList)
            {
                List<GameObject> certainTypeEnvironments = environmentsInOneGroup.ToList();
                save.environment.Add(EnviromentDTO.InitFrom(environmentsInOneGroup.Key, certainTypeEnvironments));
            }
            String json = JsonConvert.SerializeObject(save);

            int idx = 0;
            string[] files = Directory.GetFiles(data.SavePath, "*.json");
            foreach (string path in files)
            {
                GroupCollection groups = _regex.Match(path).Groups;
                for (int i = 1; i < groups.Count; i++)
                {
                    idx = Mathf.Max(Int32.Parse(groups[i].Value), idx);
                }
            }
            idx++;
            using (StreamWriter writer = new StreamWriter($"{data.SavePath}/save{idx}.json"))
            {
                writer.Write(json);
                writer.Flush();
            }
        }

        public static void SaveAll()
        {
            SaveDTO saveDto = new SaveDTO();

            saveDto.CameraPosition = new float[3];
            saveDto.CameraPosition[0] = Camera.main!.transform.position.x;
            saveDto.CameraPosition[1] = Camera.main!.transform.position.y;
            saveDto.CameraPosition[2] = Camera.main!.transform.position.z;
            saveDto.CameraRotation = new float[3];
            saveDto.CameraRotation[0] = Camera.main!.transform.rotation.eulerAngles.x;
            saveDto.CameraRotation[1] = Camera.main!.transform.rotation.eulerAngles.y;
            saveDto.CameraRotation[2] = Camera.main!.transform.rotation.eulerAngles.z;
            
            SaveMapInto(saveDto);
            SavePlayersInto(saveDto);

            string json = saveDto.ToJsonString();
            using StreamWriter writer = new StreamWriter("game-scene/Map-Windmill-Surrounded-by-Mountain.json");
            writer.Write(json);
            writer.Flush();
        }

        /// <summary>
        /// 保存人物，用于地图编辑模式
        /// </summary>
        /// <param name="saveDto"></param>
        public static void SavePlayersInto([NotNull] SaveDTO saveDto)
        {
            GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");
            List<IGrouping<int,GameObject>> groupPlayers = allUnits.GroupBy(unit => unit.GetComponent<Units.Unit>().ofPlayer).ToList();
            List<Player> players = new List<Player>();
            foreach (IGrouping<int,GameObject> unitsOfOnePlayer in groupPlayers)
            {
                Player player = new Player();
                player.UnitsList = unitsOfOnePlayer.Select(unit => unit.GetComponent<Unit>()).ToList();
                player.Index = unitsOfOnePlayer.Key;
                players.Add(player);
            }

            saveDto.Players = players.Select(player => new PlayerDTO(player)).ToList();
        }

        public static void SaveMapInto([NotNull] SaveDTO saveDto)
        {
            GameObject map = GameObject.Find("Map");
            List<GameObject> environments = new List<GameObject>();
            saveDto.blocks ??= new List<BlockDTO>();
            foreach (Transform transform in map.transform)
            {
                GameObject mapElement = transform.gameObject;
                Block block;
                if ((block = mapElement.GetComponent<Block>()) != null)
                {
                    saveDto.blocks.Add(BlockDTO.InitFrom(block));
                }
                else
                {
                    environments.Add(mapElement);
                }
            }

            saveDto.environment ??= new List<EnviromentDTO>();
            List<IGrouping<string,GameObject>> partitionList = environments.
                GroupBy(environment => environment.GetComponent<Environment>().type).ToList();
            foreach (IGrouping<string,GameObject> environmentsInOneGroup in partitionList)
            {
                List<GameObject> certainTypeEnvironments = environmentsInOneGroup.ToList();
                saveDto.environment.Add(EnviromentDTO.InitFrom(environmentsInOneGroup.Key, certainTypeEnvironments));
            }
        }

    }
}