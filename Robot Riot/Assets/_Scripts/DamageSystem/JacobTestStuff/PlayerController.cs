using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;
    private CharacterController _playerCC;
    //private Rigidbody _playerRB;

    private Vector3 _playerVelo;
    private Vector3 _jumpFoce;
    private Vector2 _moveInput = Vector2.zero;

    private float _playerSpeed = 4f;
    private float _jumpHieght = 1.5f;
    private float _gravity = -20f;

    private bool isSprinting = false;
    private bool isGrounded;
    public bool isShooting = false;

    private void Awake()
    {
        _playerCC = gameObject.GetComponent<CharacterController>();
        //_playerRB = GetComponent<Rigidbody>();

        isShooting = false;
    }
    private void Update()
    {
        isGrounded = _playerCC.isGrounded;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        _playerCC.Move(move * Time.deltaTime * _playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && _jumpFoce.y < 0)
        {
            isGrounded = false;
            _jumpFoce.y = -2f;
        }
        _jumpFoce.y += _gravity * Time.deltaTime;
        _playerCC.Move(_jumpFoce * Time.deltaTime);
    }
}
