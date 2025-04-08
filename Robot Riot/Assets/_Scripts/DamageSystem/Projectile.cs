using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Weapons weapon;
    private Vector3 startDist;
    private bool hit = false;
    public int bonusDamage;
    public GameObject VFX;
    public ProjectileType projectileType;
    public GameObject owner;
    private Vector3 velocity;
    private float gravity = -5f;


    public Vector3 target { get; set; }
    public bool hitShot { get; set; }

    private void Start()
    {
        startDist = transform.position;
        if (projectileType == ProjectileType.Prjectile)
        {
            Vector3 direction = (target - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = direction * weapon.prjectileSpeed;
        }
    }
    private void Update()
    {
        if(projectileType == ProjectileType.HitScan)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, weapon.prjectileSpeed * Time.deltaTime);
        }

        if(transform.position == target)
        {
            Destroy(gameObject);
        }

        float dis = Vector3.Distance(startDist, transform.position);
        if(dis >= weapon.maxDistance)
        {
            Destroy(gameObject);

        }
        if(hit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>())
        {
            if (weapon.weaponType == WeaponType.Projectile && other.gameObject != owner)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.localScale = new Vector3(3f, 3f, 3f);
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Destroy(gameObject, .05f);
            }
            else if(other.gameObject != owner)
            {
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Destroy(gameObject);
            }
        }

        if (other.gameObject != owner && other.gameObject.tag != "Weapon")
        {
            if(weapon.weaponType == WeaponType.Projectile)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.localScale = new Vector3(3f, 3f, 3f);
                VFX.SetActive(true);
                Destroy(gameObject, .05f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum ProjectileType
{
    Prjectile,
    HitScan
}
