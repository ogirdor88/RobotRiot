using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCandyEffect : MonoBehaviour
{
    private PlayerController playerController;

    // Get player health and start causing damage.
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        playerController.bonusDamage += 2;
        yield return new WaitForSeconds(10);
        playerController.bonusDamage -= 2;
        Destroy(this);
    }
}
