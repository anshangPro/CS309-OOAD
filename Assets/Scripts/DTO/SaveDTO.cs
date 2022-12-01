using System.Collections.Generic;
using Newtonsoft.Json;

namespace DTO
{
    public class SaveDTO
    {
        public float[] CameraPosition;
        public float[] CameraRotation;
        public List<PlayerDTO> Players;
        public List<BlockDTO> blocks;
        public List<EnviromentDTO> environment;

        public int currentPlayer;
        public bool pve;

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}