using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{

    public Controls movePlayer;
    public float rotationPowerX;
    public float rotationPowerY;
    private InputAction LookCamera;

    public GameObject Camera;
    public GameObject CameraLookTarget;

    private int ham;


    private Vector2 moveInput;


    private void Awake()
    {
        movePlayer = new Controls();
        //Screen.showCursor.MustBeFalse();
    }

    private void OnEnable()
    {
        LookCamera = movePlayer.Player.LookCamera;
        LookCamera.Enable();
        LookCamera.performed += Look;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        CameraLookTarget.transform.rotation = Quaternion.Euler(CameraLookTarget.transform.rotation.x + (-mousePosition.y * rotationPowerX), CameraLookTarget.transform.rotation.x + (mousePosition.x * rotationPowerY), 0);

        /*
        CameraLookTarget.transform.rotation *= Quaternion.AngleAxis(mousePosition.x * rotationPower, Vector3.up);
        CameraLookTarget.transform.rotation *= Quaternion.AngleAxis(mousePosition.y * rotationPower, Vector3.right);
        */
    }

    private void Look(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput.ToString());
    }

}
