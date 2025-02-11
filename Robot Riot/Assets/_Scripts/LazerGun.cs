using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] protected Weapons weapon;
    private float timeToFire = 0f;
    private PlayerMovement playerMove;

    public void Start()
    {
        playerMove = transform.root.GetComponent<PlayerMovement>();
    }

}
