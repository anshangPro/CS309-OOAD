using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public float base_speed_x = 10f;
    public float base_speed_y = 10f;
    public float compensate_speed_y = 100f;
    public float base_above_y = 1080f;
    public float base_below_y = -1080f;

    public float base_y = 0f;
    public float target_y = 0f;
    private RectTransform[] init_rects;
    private RectTransform[] rects;

    // Start is called before the first frame update
    void Start()
    {
        rects = GetComponentsInChildren<RectTransform>();
        init_rects = (RectTransform[])rects.Clone();
        base_y = init_rects[2].position.y;
        target_y = base_y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < rects.Length; i++)
        {
            rects[i].Translate(Vector3.left * base_speed_x * (i>>1) * Time.deltaTime);
            if (rects[i].position.x < - rects[i].rect.width / 2f)
                rects[i].Translate(Vector3.right * 2 * rects[i].rect.width);

            if (rects[i].position.y != target_y)
                rects[i].position = Vector3.MoveTowards(
                    rects[i].position,
                    new Vector3(rects[i].position.x, target_y, rects[i].position.z),
                    (base_speed_y * (i>>1) + compensate_speed_y) * Time.deltaTime
                );
        }
    }

    // public void HideAbove()
    // {
    //     base_y = init_y + above_y;
    // }

    // public void HideBelow()
    // {
    //     base_y = init_y + below_y;
    // }

    // public void ShowAtCenter()
    // {
    //     base_y = init_y;
    // }
}
