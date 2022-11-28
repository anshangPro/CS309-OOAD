using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public float base_speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] layer_trans = GetComponentsInChildren<Transform>();
        RectTransform[] layer_rect = GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < layer_trans.Length; i++)
        {
            layer_trans[i].transform.Translate(Vector3.left * base_speed * (i>>1) * Time.deltaTime);
            if (layer_trans[i].transform.position.x < - layer_rect[i].rect.width / 2f)
                layer_trans[i].transform.Translate(Vector3.right * (layer_rect[i].rect.width + 1920f));
        }
    }
}
