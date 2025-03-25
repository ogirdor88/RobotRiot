using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    [SerializeField] private Transform muzzle;
    [SerializeField] protected Weapons weapon;
    private float timeToFire;
    private float speedOfProjectile;
    [SerializeField] private GameObject projectile;
    //[SerializeField] private bool canShoot = true;


    private void Awake()
    {
        speedOfProjectile = weapon.prjectileSpeed * 100f;
    }
    public void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
    }

    private void Update()
    {

        Debug.Log(weapon.damage);
        if (playerMove)
        {
            if (playerMove.isShooting && canShoot)
            {
                StartCoroutine(Shooting());
                Debug.Log("shot2");
                playerMove.isShooting = false;
            }
        }
    }

    IEnumerator Shooting()
    {
        playerMove.animator.Play("L3 Shoot");
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * speedOfProjectile);
        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
        //playerMove.animator.SetBool("Shoot", !playerMove.isShooting);
    }
}
