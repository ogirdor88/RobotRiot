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
    [SerializeField] private GameObject Life1;
    [SerializeField] private GameObject Life2;
    [SerializeField] private GameObject Life3;

    //[SerializeField] private int _weaponDamage;

    public bool isProtected = false;

    private PlayerController _playerController;

    // Allows this to be on non-player objects
    private bool isPlayer;

    private void Awake()
    {
        isProtected = false;
        _spawnPoint = transform.position;
        _outOfLives = false;

        if (gameObject.GetComponent<PlayerController>())
        {
            isPlayer = true;
            _playerController = GetComponent<PlayerController>();
        }
        else
        {
            isPlayer = false;
            _playerController = null;
        }

        //set Players health to max
        SetMaxHealth(_startHealth);
    }
    private void Update()
    {
        //_weaponDamage = _weaponsObjects.weaponDmage;

        if (_currentHealth <= 0)
        {
            if (isPlayer)
                StartCoroutine(PlayerController());
            else
                Destroy(this.gameObject);
        }

        if (isPlayer)
        {
            switch (_livesCount)
            {
                case 3:
                    Life1.SetActive(true);
                    Life2.SetActive(true);
                    Life3.SetActive(true);
                    break;
                case 2:
                    Life1.SetActive(true);
                    Life2.SetActive(true);
                    Life3.SetActive(false);
                    break;
                case 1:
                    Life1.SetActive(true);
                    Life2.SetActive(false);
                    _outOfLives = true;
                    break;
                case 0:
                    Life1.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isProtected)
        {
            _currentHealth -= damage;
            if (isPlayer)
            {
                _healthSlider.value = _currentHealth;
                _healthText.text = _currentHealth.ToString();
                _healthFill.color = _healthColor.Evaluate(_healthSlider.normalizedValue);
            }
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
        if (isPlayer)
        {
            _healthSlider.value = _currentHealth;
            _healthText.text = _currentHealth.ToString();
            _healthFill.color = _healthColor.Evaluate(1f);
        }
    }
    private void Respawn()
    {
        Debug.Log("Does this work?");
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
        if (other.gameObject.tag == "Healthpack" && isPlayer)
        {
            SetMaxHealth(_startHealth);
        }
        if(other.gameObject.tag == "PowerRibbon" && isPlayer)
        {
            StartCoroutine(PlayerProtected());
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayerController()
    {
        _playerController._playerCC.enabled = false;
        yield return new WaitForSeconds(1f);
        Respawn();
        yield return new WaitForSeconds(1f);
        _playerController._playerCC.enabled = true;
    }

    IEnumerator PlayerProtected()
    {
        isProtected = true;
        yield return new WaitForSecondsRealtime(5f);
        isProtected = false;
    }
}
