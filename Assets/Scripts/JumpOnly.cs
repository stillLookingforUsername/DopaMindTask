using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnly : MonoBehaviour
{
    public CharacterController controller;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private float yVelocity;

    private void Update()
    {
        //Jump only & no horizontal movement
        if(controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; //Small negative to keep grounded
        }
        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        yVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0,yVelocity,0) * Time.deltaTime);
    }
}
