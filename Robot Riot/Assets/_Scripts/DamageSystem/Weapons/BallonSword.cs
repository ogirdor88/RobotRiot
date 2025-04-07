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

    [SerializeField] private AudioSource hitSound;

    private void Start()
    {
        canShoot = true;
        damageCollider.enabled = false;
    }
    private void Update()
    {
        if (playerMove)
        {
            playerMove.owner = gameObject;
            if (playerMove.isShooting && canShoot)
            {
                StartCoroutine(Shooting());
                Debug.Log("shot");
                playerMove.isShooting = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Health>())
        {
            if(other.gameObject != playerMove.owner)
            {
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                hitSound.Play();
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
        playerMove.animator.Play("L3 Swing");
        canShoot = false;
        damageCollider.enabled = true;
        GameObject vfx = Instantiate(swordVFX, transform.position, transform.rotation);
        vfx.GetComponent<HitboxDamage>().weapon = weapon;
        vfx.GetComponent<HitboxDamage>().playerMove = playerMove;
        vfx.GetComponent<HitboxDamage>().hitSound = hitSound;
        yield return new WaitForSeconds(timeToFire);
        Destroy(vfx);
        damageCollider.enabled = false;
        canShoot = true;
        //playerMove.animator.SetBool("Swing", !playerMove.isShooting);
    }
}
