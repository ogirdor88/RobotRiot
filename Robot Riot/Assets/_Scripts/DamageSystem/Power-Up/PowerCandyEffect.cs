using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCandyEffect : MonoBehaviour
{
    private PlayerController playerMovement;
    public Texture2D powerUpTexure;

    // Get player health and start causing damage.
    private void Awake()
    {
        playerMovement = GetComponent<PlayerController>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        // Power Candy can't send the power candy texture fast enough, so this fixes that
        yield return new WaitForSeconds(0.01f);
        //gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(powerUpTexure, "Power Candy", 10);
        playerMovement.bonusDamage += 2;
        for (int i = 10; i >= 0; i--)
        {
            gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(powerUpTexure, "Power Candy", i);
            yield return new WaitForSeconds(1);
        }
        playerMovement.bonusDamage -= 2;
        gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(null, null, -1);
        Destroy(this);
    }
}
