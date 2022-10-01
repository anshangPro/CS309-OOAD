using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float edgeSize = 50; // how close to the edge will make the camera move (unit px)
    public float speed = 1; // how fast to move
    public bool isMovable = false;

    private Vector3 cameraPosition;

    void Start()
    {
        cameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovable = !isMovable;
        }
        if (isMovable)
        {
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                cameraPosition.x += speed * Time.deltaTime;
            }

            if (Input.mousePosition.x < edgeSize)
            {
                cameraPosition.x -= speed * Time.deltaTime;
            }

            if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                cameraPosition.z += speed * Time.deltaTime;
            }

            if (Input.mousePosition.y < edgeSize)
            {
                cameraPosition.z -= speed * Time.deltaTime;
            }
        }
        Camera.main.transform.position = cameraPosition;
    }
}