using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().transform.LookAt(Camera.main.transform);
    }
}
