using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    [SerializeField] private Transform muzzle;
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

        //Debug.Log(weapon.damage);
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
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.localRotation);
        Projectile projectileContainer = newProjectile.GetComponent<Projectile>();
        projectileContainer.owner = playerMove.gameObject;

        RaycastHit shootHit;
        if (Physics.Raycast(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward), out shootHit, 100f, playerMove.layerMask))
        {
            //using forward cause we are we know the where it is going
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * shootHit.distance, Color.blue);
            projectileContainer.target = shootHit.point;
            projectileContainer.hitShot = true;
            muzzle.transform.LookAt(projectileContainer.target);
            newProjectile.transform.LookAt(projectileContainer.target);
            //muzzle.transform.Rotate(100f, 0f, 0f);
        }
        else
        {
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * 50f, Color.red);
            projectileContainer.target = playerMove.canvas.transform.position + playerMove.canvas.transform.forward * weapon.maxDistance;
            projectileContainer.hitShot = true;
            muzzle.transform.LookAt(projectileContainer.target);
            newProjectile.transform.LookAt(projectileContainer.target);
        }

        //newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * speedOfProjectile);
        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
        //playerMove.animator.SetBool("Shoot", !playerMove.isShooting);
    }
}
