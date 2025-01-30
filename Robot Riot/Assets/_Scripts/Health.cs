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

    // Players health slider
    [SerializeField] private Slider _healthSlider;

    [SerializeField] private Weapons _weaponsObjects;

    [SerializeField] private int damage;

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
        damage = _weaponsObjects.weaponDmage;
        Debug.Log(_currentHealth);
        Debug.Log(damage);
    }

    private void FixedUpdate()
    {
        //sets slider to the players health
        _healthSlider.value = _currentHealth;

        TakeDamage();
    }

    private void TakeDamage()
    {
        _currentHealth -= damage;
    }

}
