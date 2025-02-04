using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //What health the player should start with
    [SerializeField] private int _startHealth;

    //Players current health
    [SerializeField] private int _currentHealth;

    [SerializeField] private int _livesCount;

    [SerializeField] private Vector3 _spawnPoint;

    // Players health slider
    [SerializeField] private Slider _healthSlider;

    // Weapons
    [SerializeField] private Weapons _weaponsObjects;

    [SerializeField] private int _weaponDamage;

    private void Awake()
    {
        //set Players health to max
        _currentHealth = _startHealth;
    }

    private void Start()
    {

    }

    private void Update()
    {
        _weaponDamage = _weaponsObjects.weaponDmage;
        //Debug.Log(_currentHealth);
        //Debug.Log(_weaponDamage);

        if (_currentHealth <= 0)
        {
            Respawn();
        }
    }

    private void FixedUpdate()
    {
        //sets slider to the players health
        //_healthSlider.value = _currentHealth;

    }

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthSlider.value = _currentHealth;
    }

    public void DealDamage(GameObject target)
    {
        var playerTarget = target.GetComponent<Health>();
        if (playerTarget != null)
        {
            playerTarget.TakeDamage(_weaponDamage);
        }
    }

    private void Respawn()
    {
        this.gameObject.transform.position = _spawnPoint;
        if (_livesCount == 0)
        {
            //GameOver will go here
            Debug.Log("GAME OVER");
        }
        else
        {
            _livesCount--;
            _currentHealth = _startHealth;
            _healthSlider.value = _currentHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Healthpack")
        {
            _currentHealth = _startHealth;
            //Destroy(other.gameObject);
            _healthSlider.value = _currentHealth;
        }
        if (other.gameObject.tag == "SpawnPoint")
        {
            _spawnPoint = other.gameObject.transform.position;
        }
    }
}
