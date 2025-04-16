using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallonSword : Weapon
{
    [SerializeField] private Collider damageCollider;
    private float timeToFire;
    [SerializeField] private GameObject owner;


    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject swordVFX;
    [SerializeField] public CinemachineImpulseSource impulseScource;

    [SerializeField] private AudioSource hitSound;

    private void Start()
    {
        canShoot = true;
        damageCollider.enabled = false;
        owner = playerMove.gameObject;
        impulseScource.m_ImpulseDefinition.m_ImpulseChannel = owner.GetComponentInChildren<CinemachineIndependentImpulseListener>().m_ChannelMask;
    }
    private void Update()
    {
        if (playerMove)
        {
            owner = playerMove.gameObject;
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
            if(other.gameObject != owner)
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

        Vector3 direction = new Vector3(1, 1, -1);
        impulseScource.GenerateImpulse(direction * 3);


        Destroy(vfx);
        damageCollider.enabled = false;
        canShoot = true;
        //playerMove.animator.SetBool("Swing", !playerMove.isShooting);
    }
}
