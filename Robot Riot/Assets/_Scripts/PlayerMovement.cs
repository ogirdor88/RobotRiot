using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Controls movePlayer;
    //private Rigidbody playerRB;
    private Vector2 moveDirection;

    private InputAction move;
    private InputAction fire;

    [SerializeField]
    private float moveSpeed;

    private void Awake()
    {
        movePlayer = new Controls();
        //playerRB = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        move = movePlayer.Player.Movement;
        move.Enable();

        fire = movePlayer.Player.Fire;
        fire.Enable();
        fire.performed += Shoot;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
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
}
