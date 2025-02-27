using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSword : MonoBehaviour
{
    [SerializeField] private PlayerController playerMove;
    [SerializeField] private Weapons weapon;
    [SerializeField] private Collider damageCollider;
    private float timeToFire;

    public int bonusDamage = 0;

    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject swordVFX;

    // Temporarily public so we can stop cooldown issues when swapping with the placeholder system
    public bool canShoot = true;

    private void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
        damageCollider.GetComponent<Collider>();
        damageCollider.enabled = false;
    }
    private void Update()
    {
        if (playerMove == null)
        {
            playerMove = transform.parent.GetComponentInParent<PlayerController>();
            Debug.Log("yes");
        }
        if (playerMove.isShooting && canShoot)
        {
            StartCoroutine(Shooting());
            Debug.Log("shot");
            playerMove.isShooting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.transform != this.gameObject.transform.parent.parent)
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(weapon.damage + bonusDamage);
            }
            Debug.Log("SAASSAASASA");
        }
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
