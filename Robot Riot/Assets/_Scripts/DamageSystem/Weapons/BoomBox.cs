using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;


// Make sure to track how long it's being used and shrink as needed
public class BoomBox : Weapon
{
    [SerializeField] private Weapons weapon;
    [SerializeField] private Collider damageCollider;
    private float timeToFire;

    [SerializeField] private int power;

    [SerializeField] private int maxPower;

    private bool isRecharging;

    private Vector3 originalScale;

    private bool attemptCharge;

    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject boomBoxVFX;

    private GameObject currentProjectile;

    [SerializeField] private GameObject flameLocation;


    private void Start()
    {
        canShoot = true;
        attemptCharge = true;
        timeToFire = weapon.fireRate;
        power = maxPower;
        damageCollider.GetComponent<Collider>();
        damageCollider.enabled = false;
        originalScale = boomBoxVFX.transform.root.localScale;
    }
    private void Update()
    {
        if (playerMove.isShooting && !isRecharging)
        {
            FireWeapon();
        }
        if (!playerMove.isShooting)
        {
            isRecharging = true;
            StopFiring();
        }
        if (power == maxPower)
            isRecharging = false;
    }

    private void FireWeapon()
    {
        playerMove.animator.Play("L3 Swing");
        damageCollider.enabled = true;
        if (!currentProjectile)
        {
            currentProjectile = Instantiate(boomBoxVFX, flameLocation.transform.position, transform.rotation);
            currentProjectile.GetComponent<HitboxDamage>().weapon = weapon;
            currentProjectile.GetComponent<HitboxDamage>().playerMove = playerMove;
        }
        //currentProjectile.transform.localScale += new Vector3(power / maxPower, power / maxPower, power / maxPower);
        currentProjectile.transform.localScale = new Vector3(originalScale.x + (power * 0.01f), originalScale.y + (power * 0.01f), originalScale.z + (power * 0.01f));
        currentProjectile.GetComponent<BoxCollider>().size = currentProjectile.transform.localScale;
        currentProjectile.transform.position = flameLocation.transform.position;
        currentProjectile.transform.rotation = transform.rotation;
        if (power > 0 && attemptCharge)
            StartCoroutine(Recharge(false));
    }

    private void StopFiring()
    {
        Destroy(currentProjectile);
        damageCollider.enabled = false;
        if (power < maxPower && attemptCharge)
            StartCoroutine(Recharge(true));
    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/

    IEnumerator Recharge(bool Recharging)
    {
        attemptCharge = false;
        if (Recharging)
        {
            power++;
        }
        if (!Recharging)
        {
            power--;
            if (power == 0)
            {
                isRecharging = true;
                StopFiring();
            }
        }
    
        yield return new WaitForSeconds(timeToFire);
        attemptCharge = true;
    }
}
