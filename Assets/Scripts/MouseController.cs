#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MouseController : MonoBehaviour
{
    private PathFinder _pathFinder;
    private RangeFinder _rangeFinder;
    private PathDisplay _pathDisplay;

    public BaseBlock? StartBlock;

    private void Awake()
    {
        _pathFinder = new PathFinder();
        _rangeFinder = new RangeFinder();
        _pathDisplay = new PathDisplay();
    }

    private void Update()
    {
        BaseBlock? focusedOnBlock = GetFocusedOnBlock();
        if (focusedOnBlock != null)
        {
            transform.position = new Vector3(focusedOnBlock.GetCombatGrid().transform.position.x,
                focusedOnBlock.GetCombatGrid().transform.position.y + 0.002f,
                focusedOnBlock.GetCombatGrid().transform.position.z);
        }

        if (Input.GetMouseButtonDown(0) && focusedOnBlock != null)
        {
            if (MapManager.Instance.IsRangeShow)
            {
                foreach (BaseBlock block in MapManager.Instance.InRangeBlocks)
                {
                    block.IsCombatGridVisible = false;
                }
                MapManager.Instance.InRangeBlocks.Clear();

                MapManager.Instance.IsRangeShow = false;
            }
            else
            {
                for (int i = 0; i < focusedOnBlock.transform.childCount; i++)
                {
                    Debug.Log(focusedOnBlock.transform.GetChild(i).ToString());
                }
                
                MapManager.Instance.InRangeBlocks = _rangeFinder.GetBlockInRange(focusedOnBlock, 3);
                foreach (BaseBlock block in MapManager.Instance.InRangeBlocks)
                {
                    block.IsCombatGridVisible = true;
                }

                MapManager.Instance.IsRangeShow = true;
            }
        }
    }
    
    // Test function
    private void LateUpdate()
    {
        List<BaseBlock> tmp = new List<BaseBlock>();
        foreach (KeyValuePair<Vector2Int,BaseBlock> keyValuePair in MapManager.Instance.Map)
        {
            tmp.Add(keyValuePair.Value);
        }
        List<BaseBlock> path = new List<BaseBlock>();
        path.Add(MapManager.Instance.Map[new Vector2Int(0, 0)]);
        path.AddRange(_pathFinder.FindPath(MapManager.Instance.Map[new Vector2Int(8, 8)],
            MapManager.Instance.Map[new Vector2Int(0, 15)], tmp));
        path.Add(null);

        for (int i = 1; i < path.Count - 1; i++)
        {
            path[i].SetPathDisplaySprite(_pathDisplay.TranslateDirection(path[i-1], path[i], path[i+1]));
        }
    }

    private BaseBlock? GetFocusedOnBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            return hitInfo.collider.GetComponent<BaseBlock>();
        }

        return null;
    }
}