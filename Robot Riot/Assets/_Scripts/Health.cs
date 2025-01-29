using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _healthPack;

    private void Awake()
    {
        _healthPack = _startHealth;
        _currentHealth = _startHealth;
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _currentHealth = _startHealth;
        }
    }
}
