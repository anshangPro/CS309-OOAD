using System;
using System.IO;
using DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Archive
{
    public static class GameLoader
    {
        public static Tuple<BlockDTO[], EnviromentDTO[]> LoadMap(string filepath)
        {
            string json;
            using (StreamReader sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
            }
            
            JObject jObj = JObject.Parse(json);

            BlockDTO[] blockDTOs = JsonConvert.DeserializeObject<BlockDTO[]>(jObj["blocks"]!.ToString());

            EnviromentDTO[] environmentDTOs = JsonConvert.DeserializeObject<EnviromentDTO[]>(jObj["environment"]!.ToString());

            return new Tuple<BlockDTO[], EnviromentDTO[]>(blockDTOs, environmentDTOs);
        }

        public static UnitDTO[] LoadUnit(string filepath)
        {
            string json;
            using (StreamReader sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
            }

            JObject jObj = JObject.Parse(json);
            return JsonConvert.DeserializeObject<UnitDTO[]>(jObj["units"]!.ToString());
        }

        public static SaveDTO LoadSave(string path)
        {
            string json;
            using (StreamReader sc = new StreamReader(path))
            {
                json = sc.ReadToEnd();
                SaveDTO save = JsonConvert.DeserializeObject<SaveDTO>(json);
                return save;
            }
        }
    }
}