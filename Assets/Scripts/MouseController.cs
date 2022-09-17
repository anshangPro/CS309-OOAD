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
            PathFinder pathFinder = new PathFinder();
            List<BaseBlock> path = pathFinder.FindPath(MapManager.Instance.Map[new Vector2Int(0, 0)],
                hit.transform.GetComponent<BaseBlock>());
            foreach (BaseBlock block in path)
            {
                block.IsCombatGridVisible = true;
            }
            // <========================================>
        }
    }
}