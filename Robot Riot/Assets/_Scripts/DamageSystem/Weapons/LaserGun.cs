using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform muzzle2;
    [SerializeField] protected Weapons weapon;
    private float timeToFire;
    private float speedOfProjectile;
    [SerializeField] private GameObject projectile;
    //[SerializeField] private bool canShoot = true;


    private void Awake()
    {
        speedOfProjectile = weapon.prjectileSpeed * 300f;
    }
    public void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
    }

    private void Update()
    {

        //Debug.Log(weapon.damage);
        if (playerMove)
        {
            if (muzzle2 != null)
            {
                if (playerMove.isShooting && canShoot)
                {
                    StartCoroutine(DuealShooting());
                    Debug.Log("shot2");
                    playerMove.isShooting = false;
                    

                }
            }
            else
            {
                if (playerMove.isShooting && canShoot)
                {
                    StartCoroutine(Shooting());
                    Debug.Log("shot");
                    playerMove.isShooting = false;
                    
                }
            }
        }
    }

    IEnumerator Shooting()
    {
        playerMove.animator.Play("L3 Shoot");
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        //newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.up * speedOfProjectile);
        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;

        //playerMove.animator.SetBool("Shoot", !playerMove.isShooting);
    }
    IEnumerator DuealShooting()
    {
        playerMove.animator.Play("L3 Shoot");
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.up * speedOfProjectile);
        GameObject newProjectile2 = Instantiate(projectile, muzzle2.transform.position, muzzle2.rotation);
        newProjectile2.GetComponent<Rigidbody>().AddForce(newProjectile2.transform.up * speedOfProjectile);
        if (transform.root.GetComponent<PlayerController>())
        {
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
            newProjectile2.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        }

        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
        //playerMove.animator.SetBool("Shoot", !canShoot);
    }
}
