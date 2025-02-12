using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LazerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] protected Weapons weapon;
    private float timeToFire = 0f;
    [SerializeField] private PlayerMovement playerMove;

    public void Start()
    {

    }
    private void Update()
    {
        if (playerMove == null)
        {
            playerMove = transform.parent.GetComponent<PlayerMovement>();
            Debug.Log("yes");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player detected");
        }
    }
}
