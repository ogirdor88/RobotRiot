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
    [SerializeField] private Image _healthFill;
    [SerializeField] private Gradient _healthColor;
    [SerializeField] private Text _healthText;

    //[SerializeField] private int _weaponDamage;

    public bool isProtected = false;

    private void Awake()
    {
        isProtected = false;
        _spawnPoint = transform.position;
        _outOfLives = false;

        //set Players health to max
        SetMaxHealth(_startHealth);
    }

    private void Update()
    {
        //_weaponDamage = _weaponsObjects.weaponDmage;

        if (_currentHealth <= 0)
        {
            Respawn();
        }
        if (_livesCount <= 1)
        {
            _outOfLives = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isProtected)
        {
            _currentHealth -= damage;
            _healthSlider.value = _currentHealth;
            _healthText.text = _currentHealth.ToString();
            _healthFill.color = _healthColor.Evaluate(_healthSlider.normalizedValue);
            Debug.Log("DAMAGED");
        }
        else
        {
            Debug.Log("ALL GOOD");
        }
    }
    public void SetMaxHealth(int health)
    {
        _currentHealth = health;
        _healthSlider.value = _currentHealth;
        _healthText.text = _currentHealth.ToString();
        _healthFill.color = _healthColor.Evaluate(1f);
    }
    private void Respawn()
    {
        this.gameObject.transform.position = _spawnPoint;
        if (_outOfLives)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            _livesCount--;
            SetMaxHealth(_startHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Healthpack")
        {
            SetMaxHealth(_startHealth);
        }
        if(other.gameObject.tag == "PowerRibbon")
        {
            StartCoroutine(PlayerProtected());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayerProtected()
    {
        isProtected = true;
        yield return new WaitForSecondsRealtime(5f);
        isProtected = false;
    }
}
