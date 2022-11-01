using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Dependencies
    private PlayerInputHandler playerInput;
    private CharacterController characterController;

    [Header("Assignables")]
    [SerializeField] private PlayerSettings playerSettings;

    private Vector2 movementInput;
    private bool jumpInput;
    private bool sprintInput;
    private Vector3 desiredMoveVelocity;
    private Vector3 currentMoveVelocity;
    private Vector3 currentMoveVelocityRef;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ReadInput();
        ProcessMove(); //We won't use fixedUpdate for movement since we are not using a rigidbody (nor using forces).
    }

    private void ReadInput()
    {
        movementInput = playerInput.MovementInput;
        jumpInput = playerInput.JumpInput;
        sprintInput = playerInput.SprintInput;
    }

    private void ProcessMove()
    {
        if (movementInput.y > 0)
            desiredMoveVelocity.z = (sprintInput ? playerSettings.SprintSpeed : playerSettings.WalkForwardSpeed) * movementInput.y;
        else if (movementInput.y < 0)
            desiredMoveVelocity.z = playerSettings.WalkBackwardSpeed * movementInput.y;
        else desiredMoveVelocity.z = 0;

        desiredMoveVelocity.x = playerSettings.StrafeSpeed * movementInput.x;

        if (jumpInput && characterController.isGrounded)
            Jump();

        desiredMoveVelocity.y += playerSettings.Gravity * Time.deltaTime;

        if (desiredMoveVelocity.y < -0.1f && characterController.isGrounded) //Prevent gravity buildup while grounded
            desiredMoveVelocity.y = -0.1f;

        if (desiredMoveVelocity.y < playerSettings.MaxFallingSpeed) //Clamp max falling speed
            desiredMoveVelocity.y = playerSettings.MaxFallingSpeed;

        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, desiredMoveVelocity, ref currentMoveVelocityRef, playerSettings.movementSmoothing);
        Vector3 moveVelocity = transform.TransformDirection(currentMoveVelocity); //Make the movement relative to player instead of world. It is crucial to use a new variable for this, so that in the next frame the currentMoveVelocity passed into smoothdamp is not messed up.

        characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        playerInput.UseJumpInput();
        desiredMoveVelocity.y = playerSettings.JumpSpeed;
    }
}
