using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LazerGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] protected Weapons weapon;
    private float timeToFire = 0f;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool canShoot = true;

    public void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
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

    private void Shoot(GameObject projectile)
    {
        Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        projectile.transform.position = muzzle.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player detected");
        }
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * 25f);
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
}
