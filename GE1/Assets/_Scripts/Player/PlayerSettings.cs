using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerSettings", menuName = "Scriptable Objects/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Camera Settings")]
    public float XSensitivity = 50f;
    public float YSensitivity = 50f;
    public bool InvertCameraX = false;
    public bool InvertCameraY = false;

    [Header("Movement")]
    public float WalkForwardSpeed = 4f;
    public float WalkBackwardSpeed = 2f;
    public float StrafeSpeed = 3f;
    public float SprintSpeed = 10f;
    public float movementSmoothing;
    
    [Header("Jump")]
    public float JumpSpeed = 3f;

    [Header("Gravity")]
    public float Gravity = -9.8f;
    public float MaxFallingSpeed = -30f;
}
