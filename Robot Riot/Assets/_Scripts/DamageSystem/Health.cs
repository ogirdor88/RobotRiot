using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //What health the player should start with
    [SerializeField] private int _startHealth;

    //Players current health
    [SerializeField] private int _currentHealth;

    [SerializeField] private int _livesCount;

    [SerializeField] private Vector3 _spawnPoint;

    [SerializeField] private bool _outOfLives;

    // Players health slider
    [SerializeField] private Slider _healthSlider;

    //[SerializeField] private int _weaponDamage;

    private void Awake()
    {
        //set Players health to max
        _currentHealth = _startHealth;
        _spawnPoint = transform.position;
        _healthSlider.value = _currentHealth;
        _outOfLives = false;
    }

    private void Update()
    {
        //_weaponDamage = _weaponsObjects.weaponDmage;

        if (_currentHealth <= 0)
        {
            Respawn();
        }
        if (_livesCount == 1)
        {
            _outOfLives = true;
        }

        if(_livesCount == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    private void FixedUpdate()
    {
        //sets slider to the players health
        //_healthSlider.value = _currentHealth;

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthSlider.value = _currentHealth;
    }

    private void Respawn()
    {
        this.gameObject.transform.position = _spawnPoint;
        if (_outOfLives)
        {
            //GameOver will go here
            SceneManager.LoadScene("Game Over");
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
        /*
        if (other.gameObject.tag == "SpawnPoint")
        {
            _spawnPoint = other.gameObject.transform.position;
        }
        */
    }
}
