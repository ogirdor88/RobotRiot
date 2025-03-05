using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform muzzle2;
    [SerializeField] protected Weapons weapon;
    private float timeToFire;
    private float speedOfProjectile;
    [SerializeField] private PlayerController playerMove;
    [SerializeField] private GameObject projectile;
    //[SerializeField] private bool canShoot = true;

    // Temporarily public so we can stop cooldown issues when swapping with the placeholder system
    public bool canShoot = true;

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
        if (playerMove == null)
        {
            //playerMove = transform.parent.GetComponent<PlayerMovement>();
            playerMove = transform.parent.GetComponentInParent<PlayerController>();
            Debug.Log("yes");
        }

        Debug.Log(weapon.damage);
        
        if(muzzle2 != null)
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

    IEnumerator Shooting()
    {
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        //newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.up * speedOfProjectile);
        if (transform.root.GetComponent<PlayerMovement>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerMovement>().bonusDamage;
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
    IEnumerator DuealShooting()
    {
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.up * speedOfProjectile);
        GameObject newProjectile2 = Instantiate(projectile, muzzle2.transform.position, muzzle2.rotation);
        newProjectile2.GetComponent<Rigidbody>().AddForce(newProjectile2.transform.up * speedOfProjectile);
        if (transform.root.GetComponent<PlayerMovement>())
        {
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerMovement>().bonusDamage;
            newProjectile2.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerMovement>().bonusDamage;
        }

        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
}
