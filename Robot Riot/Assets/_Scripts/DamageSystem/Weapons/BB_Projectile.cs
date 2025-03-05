using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB_Projectile : MonoBehaviour
{
    [SerializeField] private Weapons weapon;
    private Vector3 startDist;
    private float growthRate;
    private Vector3 maxSize;
    private bool hit = false;
    public int bonusDamage;

    private void Start()
    {
        growthRate = 2f;
        maxSize = new Vector3(2f, transform.localScale.y, transform.localScale.z);
        startDist = transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 scale = transform.localScale;
        float dis = Vector3.Distance(startDist, transform.position);
        if (dis >= weapon.maxDistance || transform.localScale.x == maxSize.x)
        {
            Destroy(gameObject);
            Debug.Log("destroyed");
        }
        if(transform.localScale.x < maxSize.x)
        {
            scale.x += growthRate * Time.deltaTime;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Destroy(gameObject);
        }
        if (other.gameObject && other.gameObject.tag != "Weapon")
        {
                Destroy(gameObject);
        }
    }
}
