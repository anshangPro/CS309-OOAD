using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder
{
    public List<BaseBlock> GetBlockInRange(BaseBlock centerBlock, int range)
    {
        List<BaseBlock> inRangeBlock = new List<BaseBlock>();
        int stepCnt = 0;
        inRangeBlock.Add(centerBlock);

        List<BaseBlock> blocksOfPreStep = new List<BaseBlock>();
        blocksOfPreStep.Add(centerBlock);
        while (stepCnt < range)
        {
            List<BaseBlock> surroundingBlocks = new List<BaseBlock>();
            foreach (BaseBlock block in blocksOfPreStep)
            {
                surroundingBlocks.AddRange(MapManager.Instance.GetNeighborBlocks(block, MapManager.Instance.Map));
            }

            inRangeBlock.AddRange(surroundingBlocks);
            blocksOfPreStep = surroundingBlocks.Distinct().ToList();
            stepCnt++;
        }

        return inRangeBlock.Distinct().ToList();
    }
}