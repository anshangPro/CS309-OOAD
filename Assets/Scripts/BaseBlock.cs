using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseBlock : MonoBehaviour
{
    public bool IsGridVisible = false;
    public GameObject Block;
    
    // Start is called before the first frame update
    void Start()
    {
        Block = transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Block.transform.childCount != 0)
        {
            Block.transform.GetChild(0).gameObject.SetActive(IsGridVisible);
        }
        // if (Input.GetMouseButton(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         print("hit:" + hit.collider.gameObject.name);
        //     }
        // }
    }

    private void OnMouseDown()
    {
        IsGridVisible = !IsGridVisible;
        Debug.Log($"Block {transform.localPosition.ToString()} clicked");
    }
}