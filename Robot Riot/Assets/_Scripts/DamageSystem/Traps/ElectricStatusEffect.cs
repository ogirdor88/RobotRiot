using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be applied to the player.
// For 5 seconds it causes damage per second.
public class ElectricStatusEffect : MonoBehaviour
{
    private Health health;
    private int seconds = 0;

    // Get player health and start causing damage.
    private void Awake()
    {
        health = GetComponent<Health>();
        InvokeRepeating("CauseDamage", 0.0f, 1f);
    }

    // After 5 seconds, destroy this component.
    private void CauseDamage()
    {
        if (seconds >= 5)
            Destroy(this);
        health.TakeDamage(1);
        seconds += 1;
    }
}
