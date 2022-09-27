#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class MouseController : MonoBehaviour
{
    public GameStatus mode = GameStatus.Default;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){ // press left button of the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            String[] layers = new[] { "UI", "Units", "Map" };
            foreach (String layer in layers)
            {
                RaycastHit hitObj;
                bool hit = Physics.Raycast(ray, out hitObj,Mathf.Infinity, 1 << LayerMask.NameToLayer(layer));
                if (hit)
                {
                    Debug.Log($"Hit entity {hitObj.collider.name} at {hitObj.collider.transform.position} in layer {layer}");
                }
            }
        }
    }
}