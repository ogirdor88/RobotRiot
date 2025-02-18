using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mines deal 3 damage
public class Mine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(3);
            Destroy(gameObject);
        }
    }
}
