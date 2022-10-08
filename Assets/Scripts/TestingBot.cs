using System;
using UnityEngine;

public class TestingBot : MonoBehaviour
{
    // Start is called before the first frame update
    public new Rigidbody rigidbody;

    void Start()    
    {
        rigidbody = GetComponent<Rigidbody>();
        Console.WriteLine("Game start");
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 10);
    }
}