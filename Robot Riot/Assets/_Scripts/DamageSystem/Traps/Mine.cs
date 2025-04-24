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

    bool triggered;
    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(WaitForCooldown());
        if (MatchTimer.suddenDeath)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            /*other.gameObject.GetComponent<Health>().TakeDamage(WeaponDamage);
            explo.SetActive(true);
            newAudio = Instantiate(explodeSound, other.gameObject.transform);
            newAudio.gameObject.transform.parent = other.gameObject.transform;
            newAudio.Play();
            impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);
            Destroy(gameObject);
            Destroy(newAudio);*/
            if (!triggered)
            {
                impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);
                explo.SetActive(true);
                newAudio = Instantiate(explodeSound, other.gameObject.transform);
                newAudio.gameObject.transform.parent = other.gameObject.transform;
                newAudio.Play();
                other.gameObject.GetComponent<Health>().TakeDamage(WeaponDamage);
                StartCoroutine(BombBreaker());

            }
        }
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }
    private IEnumerator BombBreaker()
    {
        yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
        Destroy(newAudio.gameObject);
    }
}
