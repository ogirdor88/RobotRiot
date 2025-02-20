using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerCC;
    private Rigidbody playerRB;


    public bool shoot = false;
    private void Awake()
    {
        playerCC = GetComponent<CharacterController>();
        playerRB = GetComponent<Rigidbody>();

        shoot = false;

    }
}
