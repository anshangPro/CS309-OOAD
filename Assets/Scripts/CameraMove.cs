using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera camera;

    //旋转速度
    public float xRotationSpeed = 250.0f;
    public float yRotationSpeed = 120.0f;
    public float moveSpeed = 1f;

    private Vector3 _cameraPosition;
    private Quaternion _rotateEuler;
    private Transform _transform;
    private float _xRotation = 0;
    private float _yRotation = 0;
    private bool _rotating = false;
    private bool _moving = false;
    
    private void Awake()
    {
        _transform = camera.transform;
        _cameraPosition = _transform.position;
        _xRotation = 0;
        _yRotation = -20;
        _rotateEuler = Quaternion.Euler(-_yRotation, -_xRotation, 0);
        _transform.rotation = _rotateEuler;
    }

    void Update()
    {
        _cameraPosition = _transform.position;
        RotateCamera();
        ChangeCameraHeight();
        MoveCamera();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _rotating = true;
        }

        if (_rotating)
        {
            //Input.GetAxis("MouseX")获取鼠标移动的X轴的距离
            _xRotation -= Input.GetAxis("Mouse X") * xRotationSpeed * 0.02f;
            _yRotation += Input.GetAxis("Mouse Y") * yRotationSpeed * 0.02f;

            _yRotation = ClampValue(_yRotation, -60, 5); //限制y角度
            //欧拉角转化为四元数
            _rotateEuler = Quaternion.Euler(-_yRotation, -_xRotation, 0);
        }

        _transform.rotation = _rotateEuler;
        if (Input.GetMouseButtonUp(1))
        {
            _rotating = false;
        }
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _moving = true;
        }

        if (_moving)
        {
            Vector3 front = _transform.forward;
            front.y *= -1;
            _transform.Translate(Input.GetAxis("Mouse Y") * -0.2f * moveSpeed * front, Space.Self);
            _transform.Translate(Input.GetAxis("Mouse X") * 0.2f * moveSpeed * Vector3.left, Space.Self);
        }

        if (Input.GetMouseButtonUp(2))
        {
            _moving = false;
        }
    }

    private void ChangeCameraHeight()
    {
        _cameraPosition.y += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 400f;
        _cameraPosition.y = ClampValue(_cameraPosition.y, 8, 20);
        camera.transform.position = _cameraPosition;
    }

    private static float GetDistance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    float ClampValue(float value, float min, float max) //控制旋转的角度
    {
        if (value < -360)
            value += 360;
        if (value > 360)
            value -= 360;
        return Mathf.Clamp(value, min, max); //限制value的值在min和max之间， 如果value小于min，返回min。 如果value大于max，返回max，否则返回value
    }
}