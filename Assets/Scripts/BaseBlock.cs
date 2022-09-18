using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseBlock : MonoBehaviour
{
    public bool IsCombatGridVisible;
    public int BlockX => (int) transform.localPosition.x;
    public int BlockZ => (int)transform.localPosition.z;
    public Vector2Int Position => new Vector2Int(BlockX, BlockZ);

    public int G;
    public int H;
    public int F => G + H;

    public BaseBlock preBlock;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount != 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(IsCombatGridVisible);
        }
    }

    private void OnMouseDown()
    {
        IsCombatGridVisible = !IsCombatGridVisible;
    }

    public GameObject GetCombatGrid()
    {
        return gameObject.transform.GetChild(0).gameObject;
    }
}