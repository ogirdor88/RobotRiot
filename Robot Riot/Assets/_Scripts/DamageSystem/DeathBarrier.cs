using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(2000);
            }
        }
    }
}
