#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private PathFinder _pathFinder;
    private RangeFinder _rangeFinder;

    private void Awake()
    {
        _pathFinder = new PathFinder();
        _rangeFinder = new RangeFinder();
    }

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
        BaseBlock? focusedOnBlock = GetFocusedOnBlock();
        if (focusedOnBlock != null)
        {
            transform.position = new Vector3(focusedOnBlock.GetCombatGrid().transform.position.x,
                focusedOnBlock.GetCombatGrid().transform.position.y + 0.002f,
                focusedOnBlock.GetCombatGrid().transform.position.z);

            // gameObject.GetComponent<SpriteRenderer>().sortingOrder =
            //     focusedOnBlock.GetCombatGrid().GetComponent<SpriteRenderer>().sortingOrder;
        }
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        // {
        //     // <===================Test=================>
        //     List<BaseBlock> range = _rangeFinder.GetBlockInRange(hit.transform.GetComponent<BaseBlock>(), 3);
        //     foreach (BaseBlock block in range)
        //     {
        //         block.IsCombatGridVisible = true;
        //     }
        //     // <========================================>
        // }
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