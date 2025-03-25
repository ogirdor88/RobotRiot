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
