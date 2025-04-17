using Cinemachine;
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

    private bool didDamage;

    [SerializeField] public CinemachineImpulseSource impulseScource;


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

        

        if (other.GetComponent<Health>() && !didDamage)
        {
            if (weapon.weaponType == WeaponType.Projectile && other.gameObject != owner)
            {
                
                GetComponent<Rigidbody>().isKinematic = true;
                transform.localScale = new Vector3(3f, 3f, 3f);
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                

                Debug.Log("Did Damage");
                didDamage = true;
                Destroy(gameObject, .05f);
            }
            else if(other.gameObject != owner)
            {
                //Hit Marker Camera Feedback
                impulseScource.GenerateImpulse(transform.position);
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Debug.Log("Did Damage if else");
                didDamage = true;
                Destroy(gameObject);
            }
        }

        if (other.gameObject != owner && other.gameObject.tag != "Weapon")
        {
            if(weapon.weaponType == WeaponType.Projectile)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GameObject VFXObject = Instantiate(VFX, transform.position, transform.rotation);
                VFXObject.transform.localScale = new Vector3(.3f, .3f, .3f);
                //VFXObject.transform.localScale = new Vector3(.3f, .3f, .3f);
                transform.localScale = new Vector3(3f, 3f, 3f);

                impulseScource.GenerateImpulse(transform.position);

                //VFX.SetActive(true);
                Destroy(gameObject, .05f);
                Destroy(VFXObject, VFXObject.GetComponent<ParticleSystem>().main.duration);
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
