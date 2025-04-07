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

    [SerializeField] private AudioSource firingSound;
    [SerializeField] private AudioSource dualFiringSound1;
    [SerializeField] private AudioSource dualFiringSound2;


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
        Projectile projectileController = newProjectile.GetComponent<Projectile>();

        RaycastHit shootHit;
        if (Physics.Raycast(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward), out shootHit, 100f, playerMove.layerMask))
        {
            //using forward cause we are we know the where it is going
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * shootHit.distance, Color.blue);
            projectileController.target = shootHit.point;
            projectileController.hitShot = true;
        }
        else
        {
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * 50f, Color.red);
            projectileController.target = playerMove.canvas.transform.position + playerMove.canvas.transform.forward * weapon.prjectileSpeed;
            projectileController.hitShot = true;
        }

        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        firingSound.Play();
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;

        //playerMove.animator.SetBool("Shoot", !playerMove.isShooting);
    }
    IEnumerator DuealShooting()
    {
        playerMove.animator.Play("L3 Shoot");
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        GameObject newProjectile2 = Instantiate(projectile, muzzle2.transform.position, muzzle2.rotation);
        Projectile projectileController = newProjectile.GetComponent<Projectile>();
        Projectile projectileController2 = newProjectile.GetComponent<Projectile>();

        RaycastHit shootHit;
        if (Physics.Raycast(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward), out shootHit, 100f, playerMove.layerMask))
        {
            //using forward cause we are we know the where it is going
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * shootHit.distance, Color.blue);
            projectileController.target = shootHit.point;
            projectileController2.target = shootHit.point;
            projectileController.hitShot = true;
            projectileController2.hitShot = true;
        }
        else
        {
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * 50f, Color.red);
            //up is used here cause the prefab is messed up
            projectileController.target = playerMove.canvas.transform.position + playerMove.canvas.transform.up * weapon.prjectileSpeed;
            projectileController2.target = playerMove.canvas.transform.position + playerMove.canvas.transform.up * weapon.prjectileSpeed;
            projectileController.hitShot = true;
            projectileController2.hitShot = true;
        }
        if (transform.root.GetComponent<PlayerController>())
        {
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
            newProjectile2.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        }
        dualFiringSound1.Play();
        dualFiringSound2.Play();

        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
        //playerMove.animator.SetBool("Shoot", !canShoot);
    }
}
