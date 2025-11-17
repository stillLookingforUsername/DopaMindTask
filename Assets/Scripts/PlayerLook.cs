using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Range(1f, 200f)]
    public float moveSensitivity = 200f;
    public Transform playerBody;

    private PlayerControls inputActions;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private void Awake()
    {
        inputActions = new PlayerControls();

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void Update()
    {
        float mouseX = lookInput.x * moveSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * moveSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
