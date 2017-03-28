using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class character_controller : MonoBehaviour {
    //character horizontal speed
    public float speed = 5.0f;
    //vertical speed of jumps
    public float jumpspeed = 10.0f;
    //vertical negative speed of gravity
    public float gravity = 20.0F;
    //vector of character's movement
    private Vector3 move_direction = Vector3.zero;
    //easing of the rotation controlled by a camera
    public float rotation_easing;

    void Start () {
       
    }
	
	
	void Update () {
        
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {   //getting input
            float v_input = Input.GetAxis("Vertical");
            float h_input = Input.GetAxis("Horizontal");
            //fixing a notorious diagonal bug
            if ((h_input*v_input)!=0)
            {
                h_input /= (float)Math.Sqrt(2);
                v_input /= (float)Math.Sqrt(2);
            }
            move_direction = new Vector3(h_input, 0, v_input);

            move_direction = transform.TransformDirection(move_direction);
            //multiplying by speed
            move_direction *= speed;
            //adding vertical jump
            if (Input.GetButton("Jump"))
                move_direction.y = jumpspeed;

        }
        //adding gravity
        move_direction.y -= gravity * Time.deltaTime;
        //rotation controlled by the camera
        Quaternion rotation = Camera.main.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        //adding easing to rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotation_easing * Time.deltaTime);
        controller.Move(move_direction * Time.deltaTime);
        
    }
}
