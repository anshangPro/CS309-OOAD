using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public float base_speed_x = 10f;
    public float base_speed_y = 30f;
    public float above_y = 1080f;
    public float below_y = -1080f;

    public float base_y = 0f;
    public float stop_threshold = 0.1f;
    private float init_x;
    private float init_y;

    // Start is called before the first frame update
    void Start()
    {
        init_x = GetComponentInChildren<Transform>().transform.position.x;
        init_y = GetComponentInChildren<Transform>().transform.position.y;
        base_y = init_y;
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] layer_trans = GetComponentsInChildren<Transform>();
        RectTransform[] layer_rect = GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < layer_trans.Length; i++)
        {
            layer_trans[i].transform.Translate(Vector3.left * base_speed_x * (i>>1) * Time.deltaTime);
            if (layer_trans[i].transform.position.x < - layer_rect[i].rect.width / 2f)
                layer_trans[i].transform.Translate(Vector3.right * (layer_rect[i].rect.width + 1920f));
        }

        if (layer_trans[2].transform.position.y - base_y < stop_threshold)
        {
            for (int i = 2; i < layer_trans.Length; i++)
            {
                layer_trans[i].transform.Translate(Vector3.up * base_speed_y * (i >> 1) * Time.deltaTime);
            }
        }
        else if (layer_trans[2].transform.position.y - base_y > stop_threshold)
        {
            for (int i = 2; i < layer_trans.Length; i++)
            {
                layer_trans[i].transform.Translate(Vector3.down * base_speed_y * (i >> 1) * Time.deltaTime);
            }
        }
    }

    public void HideAbove()
    {
        base_y = init_y + above_y;
    }

    public void HideBelow()
    {
        base_y = init_y + below_y;
    }

    public void ShowAtCenter()
    {
        base_y = init_y;
    }
}
