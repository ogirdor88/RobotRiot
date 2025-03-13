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
                GameObject deployedTrap = Instantiate(trap, new Vector3(this.gameObject.transform.parent.parent.transform.position.x, this.gameObject.transform.parent.parent.transform.position.y - 0.05f, this.gameObject.transform.parent.parent.transform.position.z), this.gameObject.transform.parent.parent.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
