using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCandyEffect : MonoBehaviour
{
    private PlayerController playerMovement;

    // Get player health and start causing damage.
    private void Awake()
    {
        playerMovement = GetComponent<PlayerController>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        playerMovement.bonusDamage += 2;
        yield return new WaitForSeconds(10);
        playerMovement.bonusDamage -= 2;
        Destroy(this);
    }
}
