using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<BaseBlock> FindPath(BaseBlock start, BaseBlock end)
    {
        List<BaseBlock> openList = new List<BaseBlock>();
        List<BaseBlock> closeList = new List<BaseBlock>();
        
        openList.Add(start);
    
        while (openList.Count > 0)
        {
            BaseBlock curBlock = openList.OrderBy(x => x.F).First();
    
            openList.Remove(curBlock);
            closeList.Add(curBlock);
    
            if (curBlock == end)
            {
                List<BaseBlock> path = new List<BaseBlock>();
                while (curBlock != start)
                {
                    path.Add(curBlock);
                    curBlock = curBlock.preBlock;
                }
    
                path.Reverse();
    
                return path;
            }

            foreach (BaseBlock neighbor in GetNeighborBlocks(curBlock))
            {
                if (closeList.Contains(neighbor))
                {
                    continue;
                }
                
                neighbor.G = GetManhattenDistance(start, neighbor);
                neighbor.H = GetManhattenDistance(neighbor, end);
                neighbor.preBlock = curBlock;
                
                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
            }
        }

        return new List<BaseBlock>();
    }

    public List<BaseBlock> GetNeighborBlocks(BaseBlock block)
    {
        List<BaseBlock> neighborBlocks = new List<BaseBlock>();
        int blockX = (int) block.transform.localPosition.x;
        int blockZ = (int) block.transform.localPosition.z;
        Dictionary<Vector2Int, BaseBlock> map = MapManager.Instance.Map;
        
        try
        {
            neighborBlocks.Add(map[new Vector2Int(blockX + 1, blockZ)]);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
        }

        try
        {
            neighborBlocks.Add(map[new Vector2Int(blockX - 1, blockZ)]);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            neighborBlocks.Add(map[new Vector2Int(blockX, blockZ + 1)]);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
        }
        
        try
        {
            neighborBlocks.Add(map[new Vector2Int(blockX, blockZ - 1)]);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e);
        }

        return neighborBlocks;
    }

    private int GetManhattenDistance(BaseBlock start, BaseBlock end)
    {
        return Mathf.Abs(start.BlockX - end.BlockX) + Mathf.Abs(start.BlockZ - end.BlockZ);
    }
}
