#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

public class MouseController : MonoBehaviour
{
    public Block start;
    public Block end;

    public OverlayGridTranslator Translator = new();

    private void Start()
    {
        List<Block?> path = MapManager.Instance.FindPath(start, end, MapManager.Instance.Map.Values.ToList());
        path.Add(null);
        for (int i = 1; i < path.Count - 1; i++)
        {
            path[i]?.SetOverlayGridType(Translator.TranslateDirection(path[i - 1], path[i], path[i + 1]));
        }
    }
}