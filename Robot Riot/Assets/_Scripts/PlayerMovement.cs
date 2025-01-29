using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Controls movePlayer;
    private Rigidbody playerRB;
    private Vector2 moveDirection;

    private bool botMode;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction morph;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    private void Awake()
    {
        movePlayer = new Controls();
        playerRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        botMode = true;
    }
    private void OnEnable()
    {
        // Set up movement
        move = movePlayer.Player.Movement;
        move.Enable();

        //set up Shooting
        fire = movePlayer.Player.Fire;
        fire.Enable();
        fire.performed += Shoot;

        //set up jumping
        jump = movePlayer.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        //set up the transformation
        morph = movePlayer.Player.Transform;
        morph.Enable();
        morph.performed += SwitchModes;

    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * moveSpeed;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Pew");
    }

    private void Jump(InputAction.CallbackContext context) 
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("jump");
    }

    private void SwitchModes(InputAction.CallbackContext context)
    {
        botMode = !botMode;

        if(botMode)
        {

        }
    }

}
