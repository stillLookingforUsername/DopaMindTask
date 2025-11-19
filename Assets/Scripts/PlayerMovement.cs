using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [Range(1f,7f)]
    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float crouchHeight = 1f;
    public float normalHeight = 2f;

    private PlayerControls inputActions;
    private Vector2 moveInput;
    private float yVelocity;


    private void Awake()
    {
        inputActions = new PlayerControls();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => Jump();
        //inputActions.Player.Crouch.performed += ctx => Crouch();
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void Update()
    {
        //Movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Gravity
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; //Small negative value to keep the player grounded
        }
        yVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);

        //Crouch
        if (inputActions.Player.Crouch.IsPressed())
        {
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 15f);

        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, normalHeight, Time.deltaTime * 15f);
        }
    }

    private void Jump()
    {
        if (!controller.isGrounded) return;
        yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
