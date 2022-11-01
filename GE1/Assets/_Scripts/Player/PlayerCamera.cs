using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    private PlayerInputHandler playerInput;

    private Vector2 cameraInput;
    private Vector3 newCamRotation;
    private Vector3 newPlayerRotation;

    [Header("Assignables")]
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private Transform cameraHolder;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {
        newCamRotation = cameraHolder.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;

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
        //Vertical
        newCamRotation.x -= cameraInput.y * playerSettings.YSensitivity * (playerSettings.InvertCameraY ? -1 : 1) * Time.deltaTime; //Rotating x axis means rotating vertically, thus using y sensitivity
        newCamRotation.x = Mathf.Clamp(newCamRotation.x, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(newCamRotation);

        //Horizontal
        newPlayerRotation.y += cameraInput.x * playerSettings.XSensitivity * (playerSettings.InvertCameraX ? -1 : 1) * Time.deltaTime; //For rotating vertically, we want to just rotate the player instead of the camera so the character looks at the correct direction

        transform.localRotation = Quaternion.Euler(newPlayerRotation);
    }
}
