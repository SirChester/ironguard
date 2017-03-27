using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class character_controller : MonoBehaviour {
    public float speed = 5.0f;
    public float jumpspeed = 10.0f;
    public float gravity = 20.0F;
    private Vector3 move_direction = Vector3.zero;
    


    void Start () {
       
    }
	
	
	void Update () {
       
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move_direction = transform.TransformDirection(move_direction);
            move_direction *= speed;
            if (Input.GetButton("Jump"))
                move_direction.y = jumpspeed;

        }
        move_direction.y -= gravity * Time.deltaTime;
        Quaternion rotation  = Camera.main.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        controller.Move(move_direction * Time.deltaTime);

    }
}
