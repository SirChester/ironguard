using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{   //minimum and maximum angle of camera looking at the object without being zoomed
    private const float min_angle = 0.1f;
    private const float max_angle = 50.0f;
    //object of observation
    public Transform look_at;
    //transform of the camera
    public Transform cam_transform;
    //the camera itself
    private Camera cam;
    //distance to object
    public float max_distance;
 
    private float distance;
    //current x and y of the camera
    private float current_x = 0.0f;
    private float current_y = 0.0f;
    //sensitivity of camera on both axis
    public float sens_x = 4.0f;
    public float sens_y = 1.0f;
   

    void Start()
    {   //edit if the target camera needs to be changed
        cam_transform = transform;
        cam = Camera.main;
        distance = max_distance;

    }

    void Update()
    {   //getting input 
        current_x += Input.GetAxis("Mouse X") * sens_x;
        current_y += -Input.GetAxis("Mouse Y") * sens_y;
        current_y = Mathf.Clamp(current_y, -80, 80);
        if (current_y>max_angle)
        {
            distance = max_distance * ((90 - current_y) / (90 - max_angle));
        }

        if (current_y < min_angle)
        {
            distance = max_distance * ((90 + current_y) / (90 + min_angle));
        }





    }
    void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(current_y, current_x, 0);
        cam_transform.position = look_at.position + rotation * direction;
        cam_transform.LookAt(look_at.position); 
    }
}
