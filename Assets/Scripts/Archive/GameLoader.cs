using System;
using System.IO;
using DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Archive
{
    public static class GameLoader
    {
        public static Tuple<BlockDTO[], EnviromentDto[]> LoadMap(string filepath)
        {
            string json;
            using (StreamReader sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
            }
            
            JObject jObj = JObject.Parse(json);

            BlockDTO[] blockDTOs = JsonConvert.DeserializeObject<BlockDTO[]>(jObj["blocks"]!.ToString());

            EnviromentDto[] environmentDTOs = JsonConvert.DeserializeObject<EnviromentDto[]>(jObj["environment"]!.ToString());

            return new Tuple<BlockDTO[], EnviromentDto[]>(blockDTOs, environmentDTOs);
        }

        public static UniDto[] LoadUnit(string filepath)
        {
            string json;
            using (StreamReader sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
            }

            JObject jObj = JObject.Parse(json);
            return JsonConvert.DeserializeObject<UniDto[]>(jObj["units"]!.ToString());
        }
    }
}