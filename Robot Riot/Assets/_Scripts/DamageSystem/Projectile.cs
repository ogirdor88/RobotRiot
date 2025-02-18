using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Weapons weapon;
    private float startDist;

    private void Start()
    {
        startDist = 0f;
    }
    private void FixedUpdate()
    {
        startDist++;
        if(startDist >= weapon.maxDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(weapon.damage);
            }
            Destroy(this.gameObject);
        }
        if (other.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
