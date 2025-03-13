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
        var rbs = GetComponentInChildren<Rigidbody>();
        
        Destroy(areaColldier);
        foreach(var obj in surroundingObjects)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb != null) continue;

            rb.AddExplosionForce(_explosionForce, this.transform.position, _explosionRadius);
            Debug.Log(surroundingObjects);
        }
        
    }
}
