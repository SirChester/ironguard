using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class character_controller : MonoBehaviour {
    //character "stat" speed
    public float max_speed;
    //current character horizontal and vertical  speed
    private float v_speed;
    private float h_speed;
    //vertical speed of jumps
    public float jumpspeed = 10.0f;
    //vertical negative speed of gravity
    public float gravity = 20.0F;
    //vector of character's movement
    private Vector3 move_direction = Vector3.zero;
    //easing of the rotation controlled by a camera
    public float rotation_easing;
    //temp var for vertical and horizontal damp (in terms of oXZ)
    private float damp_velocity_v = 0.0f;
    private float damp_velocity_h = 0.0f;


    //temp var for tracking the last input from player
    private float last_v_input = 0;
    private float last_h_input = 0;
    //time in seconds for damping
    public float damp_time;

    private float v_input;
    private float h_input;

    void Start () {
        
    }
	
	
	void Update () {
    

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {

            v_input = 0;
            h_input = 0;
            //getting input
            if ((Mathf.Abs(Input.GetAxis("Vertical")) == 1))
            {
               v_input = Input.GetAxis("Vertical");
               //damping vertical speed
               v_speed = Mathf.SmoothDamp(v_speed, max_speed, ref damp_velocity_v, damp_time);
                //checking for a sharp turn
                if (last_v_input == (v_input * (-1)))
                    v_speed = 0;
                last_v_input = v_input;
            }else
            {   // damping to zero when no input
                v_speed = Mathf.SmoothDamp(v_speed, 0, ref damp_velocity_v, damp_time);
            }
            //same idea
            if ((Mathf.Abs(Input.GetAxis("Horizontal")) == 1))
            {
                h_input = Input.GetAxis("Horizontal");
                h_speed = Mathf.SmoothDamp(h_speed, max_speed, ref damp_velocity_h, damp_time);
                if (last_h_input == (h_input * (-1)))
                    h_speed = 0;
                last_h_input = h_input;
            }
            else
            {
                h_speed = Mathf.SmoothDamp(h_speed, 0, ref damp_velocity_h, damp_time);
            }
           
            // implementing movement
            move_direction = new Vector3(last_h_input*h_speed, 0, last_v_input*v_speed);
            //fixing a notorious diagonal bug
            if ((h_input * v_input) != 0)
            { move_direction /= Mathf.Sqrt(2); }

                move_direction = transform.TransformDirection(move_direction);
            //multiplying by speed
            
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
