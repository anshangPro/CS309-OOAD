using System.Collections.Generic;
using System.Linq;
using Archive;
using GameData;
using Units;
using UnityEngine;
using Util;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public readonly Dictionary<Vector2Int, Block> Map = new();
    public List<GameObject> environment = new();
    public List<Block> beacons = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

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
        if (!reachableBlocks.Contains(end))
        {
            return new List<Block>();
        }

        foreach (Block block in Map.Values)
        {
            block.Flush();
        }
        
        List<Block> openList = new List<Block> { start };
        List<Block> closeList = new List<Block>();

        while (openList.Count > 0)
        {
            openList.Sort();
            Block cur = openList.First();
            openList.Remove(cur);
            
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

                return path;
            }

            foreach (Block nxt in GetNeighborBlocks(cur, reachableBlocks).Where(nxt => !closeList.Contains(nxt) &&
                         (nxt.standUnit is null || nxt.standUnit.ofPlayer == GameDataManager.Instance.CurrentPlayer)))
            {
                if (openList.Contains(nxt))
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

        List<Block> blocksOfPreStep = new List<Block> { centerBlock };
        while (stepCnt < range)
        {
            List<Block> surroundingBlocks = new List<Block>();
            foreach (Block block in blocksOfPreStep)
            {
                surroundingBlocks.AddRange(GetNeighborBlocks(block, Map.Values.ToList())
                    .Where(tBlock => tBlock.standUnit is null || tBlock.standUnit.ofPlayer == GameDataManager.Instance.CurrentPlayer));
            }

            inRangeBlock.AddRange(surroundingBlocks);
            blocksOfPreStep = surroundingBlocks.Distinct().ToList();
            stepCnt++;
        }

        inRangeBlock = inRangeBlock.Distinct().ToList();
        List<Block> res = new List<Block>();
        foreach (Block block in inRangeBlock)
        {
            if (block.isWalkable) 
            {
                res.Add(block);
            }
        }
        return res;
    }

    /// <summary>
    ///   <para>Display reachable blocks of <code>unit</code>. This method should be invoked before
    ///   method <c>DisplayInRange</c></para>.
    /// </summary>
    /// <param name="unit"></param>
    public List<Block> DisplayInRange(Unit unit)
    {
        List<Block> movableBlocks = FindInRange(unit.onBlock, unit.Mv);
        foreach (Block block in movableBlocks)
        {
            block.SetOverlayGridType(OverlayGrid.OverlayGridType.White);
        }

        return movableBlocks;
    }

    public void DisplayAlongPath(List<Block> path)
    {
        if (path == null || path.Count == 0)
        {
            return;
        }
        
        if (path.Last() != null)
        {
            path.Add(null);
        }

        for (int i = 1; i < path.Count - 1; i++)
        {
            path[i].SetOverlayGridType(OverlayGridUtil.TranslateDirection(path[i - 1], path[i], path[i + 1]));
        }

        path.RemoveAll(block => block == null);
    }

    public List<Block> GetNeighborBlocks(Block block, List<Block> searchableBlocks)
    {
        List<Block> neighborBlocks = new List<Block>();

        Block now = GetBlock(block.X + 1, block.Z);
        if (searchableBlocks.Contains(now))
        {
            neighborBlocks.Add(now);
        }
        now = GetBlock(block.X - 1, block.Z);
        if (searchableBlocks.Contains(now))
        {
            neighborBlocks.Add(now);
        }
        now = GetBlock(block.X, block.Z + 1);
        if (searchableBlocks.Contains(now))
        {
            neighborBlocks.Add(now);
        }
        now = GetBlock(block.X, block.Z - 1);
        if (searchableBlocks.Contains(now))
        {
            neighborBlocks.Add(now);
        }

        return neighborBlocks;
    }

    private float GetManhattenDistance(Block start, Block end)
    {
        return Mathf.Abs(start.X - end.X) + Mathf.Abs(start.Z - end.Z);
    }

    public List<Block> GetFightBlocks(Block start, Unit unit, int range)
    {
        int dis = 0;
        List<Block> reachable = new List<Block>() { unit.onBlock };
        while (dis < unit.AtkRange)
        {
            List<Block> newRange = new List<Block>();
            foreach (Block block in reachable)
            {
                newRange.AddRange(GetNeighborBlocks(block, Map.Values.ToList()));
            }
            reachable.AddRange(newRange);
            reachable = reachable.Distinct().ToList();
            dis++;
        }
        return reachable;
    }

    public void HighlightUnitAtkRange(Unit unit)
    {
        if (unit == null)
        {
            return;
        }
        
        GameDataManager.Instance.HighlightBlocks.Clear();
        List<Block> reachable = GetFightBlocks(unit.onBlock, unit, unit.AtkRange);
        reachable.RemoveAt(0);
        foreach (Block highlightBlock in reachable)
        {
            if (highlightBlock.standUnit is not null && highlightBlock.standUnit.CanFightWith())
            {
                highlightBlock.SetHighlightColor(Color.red);
                GameDataManager.Instance.HighlightBlocks.Add(highlightBlock);
            }
        }
    }

    public void HighlightUnitAtkRangeExit(Unit unit)
    {
        if (unit == null)
        {
            return;
        }
        
        foreach (Block highlightBlock in GameDataManager.Instance.HighlightBlocks)
        {
            highlightBlock.SetOverlayGridType(OverlayGrid.OverlayGridType.None);
        }
    }
}