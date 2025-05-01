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
        if(other.GetComponent<Health>() && !didDamage)
        {
            if (weapon.weaponType == WeaponType.Projectile && other.gameObject != owner)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.localScale = new Vector3(3f, 3f, 3f);
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Debug.Log("Did Damage");
                didDamage = true;
                PlayAudioOnDestroy(gameObject);
            }
            else if(other.gameObject != owner)
            {
                other.GetComponent<Health>().TakeDamage(weapon.damage + bonusDamage);
                Debug.Log("Did Damage if else");
                didDamage = true;
                Destroy(gameObject);
            }
        }

        if (other.gameObject != owner && other.gameObject.tag != "Weapon" && other.gameObject.tag != "Spawner")
        {
            if(weapon.weaponType == WeaponType.Projectile)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GameObject VFXObject = Instantiate(VFX, transform.position, transform.rotation);
                VFXObject.transform.localScale = new Vector3(.3f, .3f, .3f);
                //VFXObject.transform.localScale = new Vector3(.3f, .3f, .3f);
                transform.localScale = new Vector3(3f, 3f, 3f);

                //VFX.SetActive(true);
                PlayAudioOnDestroy(gameObject);
                Destroy(VFXObject, VFXObject.GetComponent<ParticleSystem>().main.duration);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void PlayAudioOnDestroy(GameObject obj)
    {
        AudioSource confettiNoise = obj.GetComponent<AudioSource>();

        if(confettiNoise!= null && confettiNoise.clip != null)
        {
            GameObject temp = new GameObject("Audio");
            temp.transform.position = obj.transform.position;
            AudioSource tempAudio = temp.AddComponent<AudioSource>();

            tempAudio.clip = confettiNoise.clip;
            tempAudio.volume = confettiNoise.volume;
            tempAudio.pitch = confettiNoise.pitch;
            tempAudio.spatialBlend = confettiNoise.spatialBlend;
            tempAudio.minDistance = confettiNoise.minDistance;
            tempAudio.maxDistance = confettiNoise.maxDistance;
            tempAudio.rolloffMode = confettiNoise.rolloffMode;
            tempAudio.outputAudioMixerGroup = confettiNoise.outputAudioMixerGroup;

            tempAudio.Play();

            Destroy(temp, tempAudio.clip.length);
        }

        Destroy(obj, .05f);
    }
}

public enum ProjectileType
{
    Prjectile,
    HitScan
}
