using System.Collections.Generic;
using UnityEngine;

namespace DTO
{
    public class EnviromentDTO
    {
        public string type;
        public float[][] coordinates;
        public float[][] rotation;

        public static EnviromentDTO InitFrom(string pType, List<GameObject> environmentList)
        {
            EnviromentDTO enviromentDto = new EnviromentDTO();
            enviromentDto.type = pType;
            enviromentDto.coordinates = new float[environmentList.Count][];
            enviromentDto.rotation = new float[environmentList.Count][];
            for (int i = 0; i < environmentList.Count; i++)
            {
                enviromentDto.coordinates[i] = new float[3];
                enviromentDto.coordinates[i][0] = environmentList[i].transform.position.x;
                enviromentDto.coordinates[i][1] = environmentList[i].transform.position.y;
                enviromentDto.coordinates[i][2] = environmentList[i].transform.position.z;

                enviromentDto.rotation[i] = new float[3];
                enviromentDto.rotation[i][0] = environmentList[i].transform.rotation.eulerAngles.x;
                enviromentDto.rotation[i][1] = environmentList[i].transform.rotation.eulerAngles.y;
                enviromentDto.rotation[i][2] = environmentList[i].transform.rotation.eulerAngles.z;
            }

            return enviromentDto;
        }

        public Vector3[] GetCoordinates()
        {
            Vector3[] res = new Vector3[coordinates.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new Vector3(coordinates[i][0], coordinates[i][1], coordinates[i][2]);
            }

            return res;
        }
    }
}