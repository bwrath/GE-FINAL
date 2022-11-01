using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Vector2 CameraInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool SprintInput { get; private set; }

    [SerializeField] private float inputBuffer = 0.2f;

    private float lastJumpInputTime;

    private void Update()
    {
        CheckJumpBuffer();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnCameraInput(InputAction.CallbackContext context)
    {
        CameraInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpInput = true;
            lastJumpInputTime = Time.time;
        }     
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpBuffer()
    {
        if (JumpInput && Time.time > lastJumpInputTime + inputBuffer)
        {
            JumpInput = false;
        }
    }

    public void OnFireInput(InputAction.CallbackContext context) => FireInput = context.performed;

    public void OnSprintInput(InputAction.CallbackContext context) => SprintInput = context.performed;
}
