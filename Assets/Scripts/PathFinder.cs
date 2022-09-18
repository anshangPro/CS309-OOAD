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

            foreach (BaseBlock neighbor in MapManager.Instance.GetNeighborBlocks(curBlock))
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

    private int GetManhattenDistance(BaseBlock start, BaseBlock end)
    {
        return Mathf.Abs(start.BlockX - end.BlockX) + Mathf.Abs(start.BlockZ - end.BlockZ);
    }
}
