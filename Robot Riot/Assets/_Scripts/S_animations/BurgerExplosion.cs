using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerExplosion : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Collider areaColldier;

    private void Start()
    {
        explode();
    }

    void explode()
    {
        var surroundingObjects = Physics.OverlapSphere(transform.position, _explosionRadius);

        // Destroy the collider of the current object if applicable
        Destroy(areaColldier);

        foreach (var obj in surroundingObjects)
        {
            // Get the Rigidbody component of the object
            var rb = obj.GetComponent<Rigidbody>();

            // Check if the object has a Rigidbody
            if (rb != null)
            {
                // Apply explosion force to the Rigidbody
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

    }
}
