#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using GUI;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class MouseController : MonoBehaviour
{
    public GameStatus mode = GameStatus.Default;
    private UIManager _uiManager = UIManager.Instance;

    private void Start()
    {
        _uiManager = UIManager.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // press left button of the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            String[] layers = new[] { "UI", "Units", "Map" };
            PointerEventData eData = new PointerEventData(EventSystem.current);
            eData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eData, results);
            if (results.Count > 0)
            {
                foreach (RaycastResult r in results)
                {
                    if (r.gameObject.GetComponent<IClickable>() != null)
                    {
                        Debug.Log(
                            $"Hit entity {results[0].gameObject.name} at {results[0].gameObject.transform.position} in layer EventSystem");
                        if (r.gameObject.GetComponent<IClickable>().IsClicked())
                        {
                            // _uiManager.UpdateGUI();
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (String layer in layers)
                {
                    RaycastHit hitObj;
                    LayerMask.NameToLayer("TransparentFX");
                    bool hit = Physics.Raycast(ray, out hitObj, Mathf.Infinity, 1 << LayerMask.NameToLayer(layer));
                    if (hit)
                    {
                        Debug.Log(
                            $"Hit entity {hitObj.collider.name} at {hitObj.collider.transform.position} in layer {layer}");
                        if (hitObj.collider.gameObject.GetComponent<IClickable>() != null)
                            if (hitObj.collider.gameObject.GetComponent<IClickable>().IsClicked())
                            {
                                // _uiManager.UpdateGUI();
                                break;
                            }
                    }
                }
            }
        }
    }
}