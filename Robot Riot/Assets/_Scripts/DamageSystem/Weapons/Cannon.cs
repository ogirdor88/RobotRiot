using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
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
        speedOfProjectile = weapon.prjectileSpeed * 100f;
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

        if (playerMove.isShooting && canShoot)
        {
            StartCoroutine(Shooting());
            Debug.Log("shot2");
            playerMove.isShooting = false;
        }
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * speedOfProjectile);
        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
}
