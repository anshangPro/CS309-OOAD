using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class SaveDTO
    {
        public List<PlayerDTO> Players;
        public List<BlockDTO> blocks;
        public List<EnviromentDTO> environment;

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}