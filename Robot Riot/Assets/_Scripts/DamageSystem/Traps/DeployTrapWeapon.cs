using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployTrapWeapon : Weapon
{
    [SerializeField] private Weapons weapons;
    private float timeToFire;

    //[SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject trap;

    [SerializeField] public CinemachineImpulseSource impulseScource;


    private void Start()
    {
        canShoot = true;
        timeToFire = weapon.fireRate;
        canTrap = true;
    }
    private void Update()
    {
        if (playerMove)
        {
            if (playerMove.istrapping && canShoot)
            {
                Debug.Log("Deployed Trap");
                GameObject deployedTrap = Instantiate(trap, new Vector3(this.gameObject.transform.parent.parent.transform.position.x, this.gameObject.transform.parent.parent.transform.position.y + 0.5f, this.gameObject.transform.parent.parent.transform.position.z), this.gameObject.transform.parent.parent.transform.rotation);
                //deployedTrap.GetComponent<CinemachineImpulseSource>().m_ImpulseDefinition.m_ImpulseChannel = gameObject.GetComponent<CinemachineImpulseSource>().m_ImpulseDefinition.m_ImpulseChannel;
                impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);

                playerMove.istrapping = false;
                playerMove.isShooting = false;
                playerMove.GetComponent<InventoryManager>().inventoryUI.GetComponent<WeaponUI>().UpdateWeapon();
                Destroy(gameObject);
            }
        }

        if (MatchTimer.suddenDeath)
        {
            Destroy(gameObject);
        }
    }
}
