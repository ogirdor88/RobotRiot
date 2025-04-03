using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    public Weapons weapon;
    public PlayerController playerMove;

    public int bonusDamage;

    [SerializeField] private float timeToFire = 1f; 

    private bool canShoot;

    public AudioSource hitSound;

    private void Start()
    {
        canShoot = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canShoot && (other.GetComponent<Health>() && other.gameObject.transform != playerMove.gameObject.transform))
            StartCoroutine(AttemptDamage(other));
    }

    private void OnTriggerStay(Collider other)
    {
        if (canShoot && (other.GetComponent<Health>() && other.gameObject.transform != playerMove.gameObject.transform))
            StartCoroutine(AttemptDamage(other));
    }

    IEnumerator AttemptDamage(Collider other)
    {
        canShoot = false;
        if (other.GetComponent<Health>() && other.gameObject.transform != playerMove.gameObject.transform)
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(weapon.damage + bonusDamage);
                if (hitSound)
                    hitSound.Play();
            }
            Debug.Log("Hit health" + other.gameObject);
            Debug.Log("SAASSAASASA");
        }
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
}
