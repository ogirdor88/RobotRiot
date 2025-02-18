using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] protected Weapons weapon;
    private float timeToFire;
    private float speedOfProjectile;
    [SerializeField] private PlayerMovement playerMove;
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
        Debug.DrawRay(transform.position, Vector3.forward * 100, Color.green);
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
            Debug.Log("player detected");
        }
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.up * speedOfProjectile);
        yield return new WaitForSeconds(timeToFire);
        canShoot = true;
    }
}
