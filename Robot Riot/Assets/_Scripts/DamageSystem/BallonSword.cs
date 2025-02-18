using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSword : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private Weapons weapon;
    [SerializeField] private BoxCollider damageCollider;
    private float timeToFire;
    //[SerializeField] private bool canShoot = true;

    // Temporarily public so we can stop cooldown issues when swapping with the placeholder system
    public bool canShoot = true;

    private void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
        damageCollider.GetComponent<BoxCollider>();
        damageCollider.enabled = false;
    }
    private void Update()
    {
        if (playerMove == null)
        {
            playerMove = transform.parent.GetComponent<PlayerMovement>();
            Debug.Log("yes");
        }
        if (playerMove.shot && canShoot)
        {
            StartCoroutine(Shooting());
            Debug.Log("shot");
            playerMove.shot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(weapon.damage);
            }
            Debug.Log("SAASSAASASA");
        }
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        damageCollider.enabled = true;
        yield return new WaitForSeconds(timeToFire);
        damageCollider.enabled = false;
        canShoot = true;
    }
}
