using System;
using System.Collections.Generic;
using DTO;
using UnityEngine;

namespace Archive
{
    public class ItemLoader: MonoBehaviour
    {
        public List<GameObject> blocks = new List<GameObject>();

        public List<GameObject> blockList = new List<GameObject>();
        private void Awake()
        {
            Tuple<BlockDTO[], EnviromentDto[]> save = MapLoader.Load("Save/save1.json");
            foreach (BlockDTO block in save.Item1)
            {
                Debug.Log(block.type);
                GameObject blockObj = Instantiate(blocks[block.type], block.GetCoordinate(), 
                    Quaternion.identity, gameObject.transform);
                blockList.Add(blockObj);
            }
        }
    }
}