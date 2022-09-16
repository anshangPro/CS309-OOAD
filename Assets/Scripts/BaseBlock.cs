using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print("hit:" + hit.collider.gameObject.name);
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"Block {transform.localPosition.ToString()} clicked");
    }
}