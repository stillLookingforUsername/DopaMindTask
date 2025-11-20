using UnityEngine;
using System;
public class JumpOnly : MonoBehaviour
{
    public CharacterController controller;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private float yVelocity;

    public Action OnJump;

    private void Update()
    {
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            OnJump?.Invoke();
        }

        yVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }
}

