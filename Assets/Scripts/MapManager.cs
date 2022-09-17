using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance => _instance;

    public Dictionary<Vector2Int, BaseBlock> Map = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Vector3 position = gameObject.transform.GetChild(i).localPosition;
            Map.Add(new Vector2Int((int) position.x, (int) position.z), gameObject.transform.GetChild(i).GetComponent<BaseBlock>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}