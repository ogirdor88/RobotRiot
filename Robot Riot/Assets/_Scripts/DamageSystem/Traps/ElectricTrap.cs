using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The main trap, give the player a status effect when touched.
public class ElectricTrap : MonoBehaviour
{
    bool triggered;
    public GameObject ElectricVFX;

    [SerializeField] private int coolDown;
    private bool canDeploy = false;

    [SerializeField] public CinemachineImpulseSource impulseScource;


    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            if (!triggered)
            {

                other.gameObject.AddComponent<ElectricStatusEffect>();
                triggered = true;
                StartCoroutine(ElectricTrapVFX());

                impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);

            }
        }
    }

    IEnumerator ElectricTrapVFX()
    {
        ElectricVFX.SetActive(true);
        yield return new WaitForSeconds(3f);
        ElectricVFX.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }
}
