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

    [SerializeField]
    private AudioSource explodeSound;
    private AudioSource newAudio;


    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }

    private void Start()
    {
        if (MatchTimer.suddenDeath)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(WeaponDamage);
            explo.SetActive(true);
            newAudio = Instantiate(explodeSound, other.gameObject.transform);
            newAudio.gameObject.transform.parent = other.gameObject.transform;
            newAudio.Play();
            impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);
            Destroy(gameObject);
            Destroy(newAudio);
        }
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }

}
