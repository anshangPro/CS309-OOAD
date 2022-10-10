using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using Util;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public readonly Dictionary<Vector2Int, Block> Map = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 position = gameObject.transform.GetChild(i).localPosition;
            Map.Add(new Vector2Int((int)position.x, (int)position.z),
                gameObject.transform.GetChild(i).GetComponent<Block>());
        }
    }

    public Block startBlock;
    public Block endBlock;

    // private void Start()
    // {
    //     List<Block> path = FindPath(startBlock, endBlock, Map.Values.ToList());
    //     DisplayAlongPath(path);
    // }

    public Block GetBlock(int localX, int localZ)
    {
        Vector2Int dst = new Vector2Int(localX, localZ);
        return Map.ContainsKey(dst) ? Map[dst] : null;
    }

    /// <summary>
    ///   <para>根据行动力消耗寻找最短路径（最好先使用<c>FindInRange</c>找到最远可能到达的方块，减少搜索量）</para>
    /// </summary>
    /// <param name="start">起点</param>
    /// <param name="end">终点</param>
    /// <param name="reachableBlocks">可搜索（行走）的方块</param>
    /// <returns>起点到终点的最短路径（包括起点和终点）</returns>
    public List<Block> FindPath(Block start, Block end, List<Block> reachableBlocks)
    {
        PriorityQueue<Block> queue = new PriorityQueue<Block>();
        queue.Add(start);
        List<Block> closeList = new List<Block>();

        while (queue.Size() > 0)
        {
            Block cur = queue.PopFirst();
            closeList.Add(cur);

            if (cur == end)
            {
                List<Block> path = new List<Block>();
                while (cur != start)
                {
                    path.Add(cur);
                    cur = cur.parent;
                }

                path.Add(start);
                path.Reverse();

                foreach (Block block in path)
                {
                    block.Flush();
                }

                return path;
            }

            foreach (Block nxt in GetNeighborBlocks(cur, reachableBlocks).Where(nxt => !closeList.Contains(nxt)))
            {
                if (queue.Contains(nxt))
                {
                    if (cur.g + nxt.moveCost < nxt.g)
                    {
                        nxt.g = cur.g + nxt.moveCost;
                        nxt.parent = cur;
                    }
                }
                else
                {
                    nxt.g = cur.g + nxt.moveCost;
                    nxt.h = GetManhattenDistance(nxt, end);
                    nxt.parent = cur;
                    queue.Add(nxt);
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

        List<Block> blocksOfPreStep = new List<Block> { centerBlock };
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

    /// <summary>
    ///   <para>Display reachable blocks of <code>unit</code>. This method should be invoked before
    ///   method <c>DisplayInRange</c></para>.
    /// </summary>
    /// <param name="unit"></param>
    public List<Block> DisplayInRange(Unit unit)
    {
        List<Block> movableBlocks = FindInRange(unit.onBlock.GetComponent<Block>(), unit.Agility);
        foreach (Block block in movableBlocks)
        {
            block.SetOverlayGridType(OverlayGrid.OverlayGridType.White);
        }

        return movableBlocks;
    }

    public void DisplayAlongPath(List<Block> path)
    {
        if (path.Last() != null)
        {
            path.Add(null);
        }

        for (int i = 1; i < path.Count - 1; i++)
        {
            path[i].SetOverlayGridType(OverlayGridUtil.TranslateDirection(path[i - 1], path[i], path[i + 1]));
        }
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

    private float GetManhattenDistance(Block start, Block end)
    {
        return Mathf.Abs(start.X - end.X) + Mathf.Abs(start.Z - end.Z);
    }
}