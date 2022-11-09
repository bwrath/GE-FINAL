using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    private PlayerInputHandler playerInput;

    private Vector2 cameraInput;
    private Vector3 currentCamRotation;
    private float xRotation;
    private float yRotation;

    [Header("Assignables")]
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private Transform cameraHolder;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ReadCameraInput();
        ProcessCameraRotation();
    }

    private void ReadCameraInput() => cameraInput = playerInput.CameraInput;

    private void ProcessCameraRotation()
    {
        xRotation -= cameraInput.y * playerSettings.YSensitivity * (playerSettings.InvertCameraY ? -1 : 1) * Time.deltaTime;
        yRotation += cameraInput.x * playerSettings.XSensitivity * (playerSettings.InvertCameraX ? -1 : 1) * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f ,0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
