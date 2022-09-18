using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            // <===================Test=================>
            List<BaseBlock> range = RangeFinder.GetBlockInRange(hit.transform.GetComponent<BaseBlock>(), 3);
            foreach (BaseBlock block in range)
            {
                block.IsCombatGridVisible = true;
            }
            // <========================================>
        }
    }
}