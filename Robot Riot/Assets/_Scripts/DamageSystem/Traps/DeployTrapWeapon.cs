using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTrapWeapon : Weapon
{
    [SerializeField] private Weapons weapon;
    private float timeToFire;


    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject trap;

    private void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
    }
    private void Update()
    {
        if (playerMove)
        {
            if (playerMove.isShooting && canShoot)
            {
                Debug.Log("Deployed Trap");
                GameObject deployedTrap = Instantiate(trap, this.gameObject.transform.parent.parent.transform.position, this.gameObject.transform.parent.parent.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
