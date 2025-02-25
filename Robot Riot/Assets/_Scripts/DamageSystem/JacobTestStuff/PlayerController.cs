using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private CharacterController _playerCC;
    [SerializeField] private Transform _camera;

    private Vector3 _playerVelo;
    private Vector3 _jumpFoce;
    private Vector3 _moveInput = Vector3.zero;

    private Vector2 _cameraMove;

    private float _playerSpeed = 4f;
    private float _jumpHieght = 1f;
    private float _gravity = -20;
    private float vertical;
    private float horizontal;
    private float originalMoveSpeed;

    private float xRotaion = 0f;
    private float lookSens = 1.8f;

    private bool isSprinting = false;
    private bool isGrounded;
    private bool isJumping;
    private bool jump;
    private bool botMode = false;

    public bool isShooting = false;

    private void Awake()
    {
        _playerCC = gameObject.GetComponent<CharacterController>();

        originalMoveSpeed = _playerSpeed;

        botMode = false;
        isShooting = false;
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.15f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        UpdateMove();
        UpdateJump();
        UpdateCamera();
    }

    #region Movement
    private void UpdateMove()
    {
        _moveInput = transform.right * horizontal + transform.forward * vertical;
        _playerCC.Move(_moveInput * _playerSpeed * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
    #endregion
    #region Jump

    private void UpdateJump()
    {
        if (isGrounded && _jumpFoce.y < 0)
        {
            _jumpFoce.y = -3f;
            jump = true;
        }
        _jumpFoce.y += _gravity * Time.deltaTime;
        _playerCC.Move(_jumpFoce * Time.deltaTime);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (jump)
        {
            _jumpFoce.y = Mathf.Sqrt(_jumpHieght * -3f * _gravity);
        }
        jump = false;
    }
    #endregion
    #region Camera
    public void CamMove(InputAction.CallbackContext context)
    {
        _cameraMove = context.ReadValue<Vector2>();
    }
    public void UpdateCamera()
    {
        float rotateX = _cameraMove.x * lookSens;
        float rotateY = _cameraMove.y * lookSens;

        transform.Rotate(Vector3.up * rotateX);

        xRotaion -= rotateY;
        xRotaion = Mathf.Clamp(xRotaion, -50f, 60f);

        _camera.transform.localRotation = Quaternion.Euler(xRotaion, 0f, 0f);
    }
    #endregion
    #region Shooting/Reload
    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if (botMode)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = this.transform.position;
            }
            else
            {
                Debug.Log("Pew");
                isShooting = true;
            }
        }
    }
    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Reloading");
    }
    #endregion
    #region Bot Mode
    public void SwitchModes(InputAction.CallbackContext context)
    {
        botMode = !botMode;
        // this is set up just for inital prototyping purposes
        // will be changed later
        if (botMode)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            _playerSpeed = _playerSpeed * 1.25f;

            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
            _playerSpeed = originalMoveSpeed;
            Debug.Log("Combat Mode");
        }
    }
    #endregion
    #region Sprinting
    public void SpeedBoost(InputAction.CallbackContext context)
    {
        isSprinting = true;
        Debug.Log("Boost");
        _playerSpeed = _playerSpeed * 3f;
    }
    public void EndBoost(InputAction.CallbackContext context)
    {
        isSprinting = false;
        Debug.Log("BoostStopped");
        _playerSpeed = originalMoveSpeed;
    }
    #endregion
}
