using UnityEngine;

namespace DTO
{
    public class EnviromentDTO
    {
        public string type;
        public int[][] coordinates;

        public Vector3Int[] GetCoordinates()
        {
            Vector3Int[] res = new Vector3Int[coordinates.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new Vector3Int(coordinates[i][0], coordinates[i][1], coordinates[i][2]);
            }

            return res;
        }
    }
}