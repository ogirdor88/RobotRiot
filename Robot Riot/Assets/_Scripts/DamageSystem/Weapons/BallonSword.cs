using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSword : Weapon
{
    [SerializeField] private Weapons weapon;
    [SerializeField] private Collider damageCollider;
    private float timeToFire;


    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject swordVFX;


    private void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
        damageCollider.GetComponent<Collider>();
        damageCollider.enabled = false;
    }
    private void Update()
    {
        if (playerMove.isShooting && canShoot)
        {
            StartCoroutine(Shooting());
            Debug.Log("shot");
            playerMove.isShooting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Health>() && other.gameObject.transform != this.gameObject.transform.parent.parent)
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(weapon.damage + bonusDamage);
            }
            Debug.Log("Hit health" + other.gameObject);
            Debug.Log("SAASSAASASA");
        }
        /*else if (other.GetComponent<Health>())
        {
            Debug.Log("Hit health" + other.gameObject);
        }*/
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        damageCollider.enabled = true;
        GameObject vfx = Instantiate(swordVFX, transform.position, transform.rotation);
        yield return new WaitForSeconds(timeToFire);
        Destroy(vfx);
        damageCollider.enabled = false;
        canShoot = true;
    }
}
