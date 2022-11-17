#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using GUI;
using Interfaces;
using StateMachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class MouseController : MonoBehaviour
{
    private UIManager _uiManager = UIManager.Instance;
    public static String GameObjectName = "";

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
                        GameObjectName = r.gameObject.name;
                        Debug.Log(
                            $"Hit entity {results[0].gameObject.name} at {results[0].gameObject.transform.position} in layer EventSystem");
                        if (r.gameObject.GetComponent<IClickable>().IsClicked())
                        {
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
                        GameObjectName = hitObj.collider.gameObject.name;
                        if (hitObj.collider.gameObject.GetComponent<IClickable>() != null)
                            if (hitObj.collider.gameObject.GetComponent<IClickable>().IsClicked())
                            {
                                break;
                            }
                    }
                }
            }
        }

        FloatPane();
    }

    private float _deltaTime = -1;
    private bool _canShowFloatPanel = true;
    private void FloatPane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        String layer = "Units";
        RaycastHit hitObj;
        LayerMask.NameToLayer("TransparentFX");
        bool hit = Physics.Raycast(ray, out hitObj, Mathf.Infinity, 1 << LayerMask.NameToLayer(layer));
        if (hit)
        {
            GameObject hitGameObject = hitObj.collider.gameObject; 
            GameObjectName = hitGameObject.name;
            if (hitGameObject.GetComponent<IFloatPanel>() != null)
            {
                hitGameObject.GetComponent<IFloatPanel>().ShowPanel();
                _canShowFloatPanel = false;
                _deltaTime = Time.time;
            }
        }
        
        if (!_canShowFloatPanel){
            LeftDownInfoPanelController floatPanel = LeftDownInfoPanelController.Instance;
            Vector3 ori = floatPanel.transform.position;
            if (Input.mousePosition.x < 800 && Input.mousePosition.y < 400)
            {
                ori.x = 1650;
                floatPanel.transform.position = ori;
            }
            else
            {
                ori.x = 270;
                floatPanel.transform.position = ori;
            }
        }

        if (!_canShowFloatPanel && Time.time - _deltaTime > 0.1)
        {
            _canShowFloatPanel = true;
            GameObject panel = LeftDownInfoPanelController.FloatPanel;
            panel.SetActive(false);
        }
    }
}