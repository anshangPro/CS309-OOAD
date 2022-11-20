using System;
using System.Collections.Generic;
using System.IO;
using DTO;
using GameData;
using Newtonsoft.Json;
using UnityEngine;

namespace Archive
{
    public class MapSaver
    {
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
            String json = JsonConvert.SerializeObject(save);

            using (StreamWriter writer = new StreamWriter("save/save_test.json"))
            {
                writer.Write(json);
                writer.Flush();
            }
        }
        
    }
}