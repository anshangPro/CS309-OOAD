using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance => _instance;

    public Dictionary<Vector2Int, Block> Map = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 position = gameObject.transform.GetChild(i).localPosition;
            Map.Add(new Vector2Int((int) position.x, (int) position.z), gameObject.transform.GetChild(i).GetComponent<Block>());
        }
    }

    private void Start()
    {
        
    }
    
    public Block GetBlock(int localX, int localZ)
    {
        Vector2Int dst = new Vector2Int(localX, localZ);
        if (Map.ContainsKey(dst))
        {
            return Map[dst];
        }

        return null;
    }

    public List<Block> FindPath(Block start, Block end, List<Block> reachableBlocks)
    {
        List<Block> openList = new List<Block>();
        List<Block> closeList = new List<Block>();
        openList.Add(start);

        Dictionary<Block, Block> preDict = new Dictionary<Block, Block>();

        while (openList.Count > 0)
        {
            Block cur = openList.OrderBy(x => x.F).First();
            openList.Remove(cur);
            closeList.Add(cur);

            if (cur == end)
            {
                List<Block> path = new List<Block>();
                while (cur != start)
                {
                    path.Add(cur);
                    cur = preDict[cur];
                }
                path.Add(start);
                path.Reverse();
                return path;
            }

            foreach (Block nxt in GetNeighborBlocks(cur, reachableBlocks))
            {
                if (closeList.Contains(nxt))
                {
                    continue;
                }

                nxt.G = GetManhattenDistance(start, nxt);
                nxt.H = GetManhattenDistance(nxt, end);
                if (preDict.Keys.Contains(nxt))
                {
                    preDict[nxt] = cur;
                }
                else
                {
                    preDict.Add(nxt, cur);
                }

                if (!openList.Contains(nxt))
                {
                    openList.Add(nxt);
                }
            }
        }

        return new List<Block>();
    }

    public List<Block> FindInRange(Block centerBlock, int range)
    {
        List<Block> inRangeBlock = new List<Block>();
        int stepCnt = 0;
        inRangeBlock.Add(centerBlock);
        
        List<Block> blocksOfPreStep = new List<Block>();
        blocksOfPreStep.Add(centerBlock);
        while (stepCnt < range)
        {
            List<Block> surroundingBlocks = new List<Block>();
            foreach (Block block in blocksOfPreStep)
            {
                surroundingBlocks.AddRange(GetNeighborBlocks(block, Map.Values.ToList()));
            }
        
            inRangeBlock.AddRange(surroundingBlocks);
            blocksOfPreStep = surroundingBlocks.Distinct().ToList();
            stepCnt++;
        }
        
        return inRangeBlock.Distinct().ToList();
    }

    public List<Block> GetNeighborBlocks(Block block, List<Block> searchableBlocks)
    {
        List<Block> neighborBlocks = new List<Block>();

        if (searchableBlocks.Contains(GetBlock(block.X + 1, block.Z)))
        {
            neighborBlocks.Add(GetBlock(block.X + 1, block.Z));
        }
        
        if (searchableBlocks.Contains(GetBlock(block.X - 1, block.Z)))
        {
            neighborBlocks.Add(GetBlock(block.X - 1, block.Z));
        }
        
        if (searchableBlocks.Contains(GetBlock(block.X, block.Z + 1)))
        {
            neighborBlocks.Add(GetBlock(block.X, block.Z + 1));
        }
        
        if (searchableBlocks.Contains(GetBlock(block.X, block.Z - 1)))
        {
            neighborBlocks.Add(GetBlock(block.X, block.Z - 1));
        }
        
        return neighborBlocks;
    }
    
    private int GetManhattenDistance(Block start, Block end)
    {
        return Mathf.Abs(start.X - end.X) + Mathf.Abs(start.Z - end.Z);
    }
}