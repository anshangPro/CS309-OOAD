using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseBlock : MonoBehaviour
{
    public bool IsCombatGridVisible = false;
    public int BlockX => (int) transform.localPosition.x;
    public int BlockZ => (int)transform.localPosition.z;

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
        Debug.Log($"Block {transform.localPosition.ToString()} clicked");
    }
}