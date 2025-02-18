using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The main trap, give the player a status effect when touched.
public class ElectricTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.AddComponent<ElectricStatusEffect>();
            Destroy(gameObject);
        }
    }
}
