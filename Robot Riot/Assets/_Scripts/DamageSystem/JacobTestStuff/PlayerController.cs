using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private InputActionAsset _playerControls;
    private CharacterController _playerCC;
    //private Rigidbody _playerRB;

    private Vector3 _playerVelo;
    private Vector3 _jumpFoce;
    private Vector3 _moveInput = Vector3.zero;

    private float _playerSpeed = 4f;
    private float _jumpHieght = 1.5f;
    private float _gravity = -20;
    private float vertical;
    private float horizontal;

    private bool isSprinting = false;
    [SerializeField] private bool isGrounded;
    private bool isJumping;
    private bool jump;
    public bool isShooting = false;
    bool moving = false;

    float jumponce = 0;
    private void Awake()
    {
        _playerCC = gameObject.GetComponent<CharacterController>();
        //_playerRB = GetComponent<Rigidbody>();

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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.blue, 1.15f);
        updateMove();
        updateJump();
    }

    #region Movement
    private void updateMove()
    {
        _moveInput = transform.right * horizontal + transform.forward * vertical;
        _playerCC.Move(_moveInput * _playerSpeed * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {

        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
        moving = true;
    }
    #endregion
    #region Jump

    private void updateJump()
    {
        if (isGrounded && _jumpFoce.y < 0)
        {
            _jumpFoce.y = -3f;
        }
        _jumpFoce.y += _gravity * Time.deltaTime;
        _playerCC.Move(_jumpFoce * Time.deltaTime);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        _jumpFoce.y = Mathf.Sqrt(_jumpHieght * -3f * _gravity);
    }
    #endregion
}
