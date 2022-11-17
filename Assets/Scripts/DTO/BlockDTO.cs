using System;
using System.Collections.Generic;
using UnityEngine;

namespace DTO
{
    public class BlockDTO
    {
        public int type;
        public int[] coordinate;
        public int cost;
        public Dictionary<string, string> optional;

        public Vector3Int GetCoordinate()
        {
            return new Vector3Int(coordinate[0], coordinate[1], coordinate[2]);
        }
    }
}