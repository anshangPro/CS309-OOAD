using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance => _instance;

    public Dictionary<Vector2Int, BaseBlock> Map = new();

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
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 position = gameObject.transform.GetChild(i).localPosition;
            Map.Add(new Vector2Int((int) position.x, (int) position.z), gameObject.transform.GetChild(i).GetComponent<BaseBlock>());
        }
    }

    public List<BaseBlock> GetNeighborBlocks(BaseBlock block)
    {
        List<BaseBlock> neighborBlocks = new List<BaseBlock>();
        int blockX = (int) block.transform.localPosition.x;
        int blockZ = (int) block.transform.localPosition.z;
        Dictionary<Vector2Int, BaseBlock> map = MapManager.Instance.Map;
        
        try { neighborBlocks.Add(map[new Vector2Int(blockX + 1, blockZ)]); }
        catch (KeyNotFoundException e) { Console.WriteLine(e); }

        try { neighborBlocks.Add(map[new Vector2Int(blockX - 1, blockZ)]); }
        catch (KeyNotFoundException e) { Console.WriteLine(e); }
        
        try { neighborBlocks.Add(map[new Vector2Int(blockX, blockZ + 1)]); }
        catch (KeyNotFoundException e) { Console.WriteLine(e); }
        
        try { neighborBlocks.Add(map[new Vector2Int(blockX, blockZ - 1)]); }
        catch (KeyNotFoundException e) { Console.WriteLine(e); }

        return neighborBlocks;
    }
}