using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DTO
{
    public class BlockDTO
    {
        public int type;
        public int[] coordinate;
        public float cost;
        public bool isWalkable;
        public Dictionary<string, string> optional;
        
        public static BlockDTO InitFrom(Block block)
        {
            BlockDTO blockDto = new BlockDTO();
            blockDto.type = block.type;
            blockDto.coordinate = new[]
            {
                (int)block.transform.position.x,
                (int)block.transform.position.y,
                (int)block.transform.position.z
            };
            blockDto.cost = block.moveCost;
            blockDto.isWalkable = block.isWalkable;
            return blockDto;
        }

        public Vector3Int GetCoordinate()
        {
            return new Vector3Int(coordinate[0], coordinate[1], coordinate[2]);
        }
    }
}