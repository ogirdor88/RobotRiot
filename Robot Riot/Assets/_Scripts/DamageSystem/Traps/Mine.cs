using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mines deal 3 damage
public class Mine : MonoBehaviour
{
    [SerializeField] private int coolDown;
    private bool canDeploy = false;
    public int WeaponDamage;
    public GameObject explo;

    [SerializeField] public CinemachineImpulseSource impulseScource;


    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }

    private void Start()
    {
        if (MatchTimer.suddenDeath)
        {
            Destroy(gameObject, 7f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(WeaponDamage);
            explo.SetActive(true);
            impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }

}
