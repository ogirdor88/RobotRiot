using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class LaserGun : Weapon
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform muzzle2;
    private float timeToFire;
    private float speedOfProjectile;
    [SerializeField] private GameObject projectile;
    //[SerializeField] private bool canShoot = true;

    [SerializeField] private AudioSource firingSound;
    [SerializeField] private AudioSource dualFiringSound1;
    [SerializeField] private AudioSource dualFiringSound2;

    [SerializeField] private GameObject owner;

    [SerializeField] public CinemachineImpulseSource impulseScource;

    private void Awake()
    {
        speedOfProjectile = weapon.prjectileSpeed * 300f;
    }
    public void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
        remainingAmmo = weapon.ammo;
        owner = playerMove.gameObject;
        impulseScource.m_ImpulseDefinition.m_ImpulseChannel = owner.GetComponentInChildren<CinemachineIndependentImpulseListener>().m_ChannelMask;
    }

    private void Update()
    {

        //Debug.Log(weapon.damage);
        if (playerMove)
        {
            if (muzzle2 != null)
            {
                playerMove.owner = gameObject;
                if (playerMove.isShooting && canShoot)
                {
                    StartCoroutine(DuealShooting());
                    Debug.Log("shot2");
                    playerMove.isShooting = false;
                    

                }
            }
            else
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
    }

    IEnumerator Shooting()
    {
        playerMove.animator.Play("L3 Shoot");
        //playerMove.GetComponent<CinemachineImpulseSource>
        canShoot = false;

        //Vector3 direction = new Vector3(-1, 1, 1);
        impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);

        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.transform.localRotation);
        //newProjectile.transform.SetParent(muzzle);
        Projectile projectileController = newProjectile.GetComponent<Projectile>();
        projectileController.owner = playerMove.gameObject;
        remainingAmmo--;
        playerMove.GetComponent<InventoryManager>().inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();

        RaycastHit shootHit;
        if (Physics.Raycast(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward), out shootHit, 100f, playerMove.layerMask))
        {
            //using forward cause we are we know the where it is going
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * shootHit.distance, Color.blue);
            projectileController.target = shootHit.point;
            projectileController.hitShot = true;
            muzzle.transform.LookAt(projectileController.target);
            newProjectile.transform.LookAt(projectileController.target);
            //muzzle.transform.Rotate(100f, 0f, 0f);
        }
        else
        {
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * 50f, Color.red);
            
            Vector3 forwardDirection = playerMove.canvas.transform.forward;
            Vector3 fallbackTarget = playerMove.canvas.transform.position + forwardDirection * weapon.maxDistance;

            projectileController.target = fallbackTarget;
            projectileController.hitShot = true;
        }

        if (transform.root.GetComponent<PlayerController>())
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        firingSound.Play();
        if (remainingAmmo == 0)
            Destroy(gameObject);
        else
        {
            yield return new WaitForSeconds(timeToFire);
            canShoot = true;
        }

        //playerMove.animator.SetBool("Shoot", !playerMove.isShooting);
    }
    IEnumerator DuealShooting()
    {
        playerMove.animator.Play("L3 Shoot");
        canShoot = false;
        GameObject newProjectile = Instantiate(projectile, muzzle.transform.position, muzzle.rotation);
        GameObject newProjectile2 = Instantiate(projectile, muzzle2.transform.position, muzzle2.rotation);
        //newProjectile.transform.SetParent(muzzle);
        //newProjectile2.transform.SetParent(muzzle2);
        Projectile projectileController = newProjectile.GetComponent<Projectile>();
        Projectile projectileController2 = newProjectile.GetComponent<Projectile>();
        projectileController.owner = playerMove.gameObject;
        projectileController2.owner = playerMove.gameObject;
        remainingAmmo -= 2;
        playerMove.GetComponent<InventoryManager>().inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();

        RaycastHit shootHit;
        if (Physics.Raycast(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward), out shootHit, 100f, playerMove.layerMask))
        {
            //using forward cause we are we know the where it is going
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * shootHit.distance, Color.blue);

            projectileController.target = shootHit.point;
            projectileController2.target = shootHit.point;
            muzzle.transform.LookAt(projectileController.target);
            muzzle2.transform.LookAt(projectileController2.target);
            projectileController.hitShot = true;
            projectileController2.hitShot = true;
        }
        else
        {
            Debug.DrawRay(playerMove.canvas.transform.position, playerMove.canvas.transform.TransformDirection(Vector3.forward) * 50f, Color.red);
            //up is used here cause the prefab is messed up
            Vector3 forwardDirection = playerMove.canvas.transform.forward;
            Vector3 fallbackTarget = playerMove.canvas.transform.position + forwardDirection * weapon.maxDistance;

            projectileController.target = fallbackTarget;
            projectileController2.target = fallbackTarget;
            projectileController.hitShot = true;
            projectileController2.hitShot = true;

            muzzle.transform.LookAt(fallbackTarget);
            muzzle2.transform.LookAt(fallbackTarget);
        }
        if (transform.root.GetComponent<PlayerController>())
        {
            newProjectile.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
            newProjectile2.GetComponent<Projectile>().bonusDamage = transform.root.GetComponent<PlayerController>().bonusDamage;
        }
        dualFiringSound1.Play();
        dualFiringSound2.Play();

        if (remainingAmmo <= 0)
            Destroy(gameObject);
        else
        {
            yield return new WaitForSeconds(timeToFire);
            canShoot = true;
        }
        //playerMove.animator.SetBool("Shoot", !canShoot);
    }
}
