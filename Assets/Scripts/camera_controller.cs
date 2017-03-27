using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    private const float min_angle = 0.0f;
    private const float max_angle = 50.0f;

    public Transform look_at;
    public Transform cam_transform;
    private Camera cam;
    public float distance = 10.0f;
    private float current_x = 0.0f;
    private float current_y = 0.0f;
    public float sens_x = 4.0f;
    public float sens_y = 1.0f;
   

    void Start()
    {
        cam_transform = transform;
        cam = Camera.main;
          
    }

    void Update()
    {
        current_x += Input.GetAxis("Mouse X")*sens_x;
        current_y += -Input.GetAxis("Mouse Y")*sens_y;
        current_y = Mathf.Clamp(current_y, min_angle, max_angle);

    }
    void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(current_y, current_x, 0);
        cam_transform.position = look_at.position + rotation * direction;
        cam_transform.LookAt(look_at.position); 
    }
}
