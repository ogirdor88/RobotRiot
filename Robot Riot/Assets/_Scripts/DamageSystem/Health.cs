using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    //What health the player should start with
    [SerializeField] private int _startHealth;

    //Players current health
    [SerializeField] public int _currentHealth;
    private int missingHealth;
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

    // Keep track of what player this is
    public int playerNumber;

    public Texture2D powerRibbonTexure;
    public Texture2D healthPackTexure;
    public Texture2D energyDrinkTexure;

    private void Awake()
    {
        playerNumber = GetComponent<PlayerInput>().playerIndex + 1;

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
        //missingHealth = _startHealth - _currentHealth;
        if (_currentHealth <= 0)
        {
            if (isPlayer)
            {
                Respawn();
            }
            else
            {
                Destroy(this.gameObject);
            }
                
        }

        if (isPlayer)
        {
            if (MatchTimer.suddenDeath)
            {
                SuddenDeathRespawn();
            }

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
                    Life3.SetActive(false);
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
        _playerController._playerCC.enabled = false;
        this.gameObject.transform.position = _spawnPoint;
        if (_outOfLives)
        {
            //SceneManager.LoadScene(3);
            GameObject.FindObjectOfType<GameManager>().GameOver(playerNumber);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            _livesCount--;
            SetMaxHealth(_startHealth);
        }
        _playerController._playerCC.enabled = true;
    }

    private void SuddenDeathRespawn()
    {
        _playerController._playerCC.enabled = false;
        transform.position = _spawnPoint;
        _livesCount = 1;
        //sets player health to 1
        SetMaxHealth(1);
        _playerController._playerCC.enabled = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Healthpack" && isPlayer)
        {
            SetMaxHealth(_startHealth);
            StartCoroutine(ObtainedPowerUp("Health Pack", healthPackTexure));
            StartCoroutine(CountDown(other.gameObject));
            //Destroy(other.gameObject);
            Debug.Log("Collecteed H");
        }
        if(other.gameObject.tag == "PowerRibbon" && isPlayer)
        {
            StartCoroutine(PlayerProtected());
            Destroy(other.gameObject);
            Debug.Log("Collecteed PR");
        }
        if(other.gameObject.tag == "EnergyDrink")
        {
            int newHealth = _currentHealth + 50;
            if(newHealth >= _startHealth)
            {
                newHealth = _startHealth;
            }
            float newStamina = _playerController.stamina + 50f;
            if(newStamina >= _playerController.maxStamina)
            {
                _playerController.stamina = _playerController.maxStamina;
            }
            else
            {
                _playerController.stamina = newStamina;
            }
            SetMaxHealth(newHealth);
            StartCoroutine(ObtainedPowerUp("Energy Drink", energyDrinkTexure));
            Destroy(other.gameObject);
            Debug.Log("Collecteed ED");
        }
    }

    IEnumerator PlayerProtected()
    {
        isProtected = true;
        //yield return new WaitForSecondsRealtime(5f);
        for (int i = 5; i >= 0; i--)
        {
            gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(powerRibbonTexure, "Power Ribbon", i, 5);
            yield return new WaitForSeconds(1);
        }
        gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(null, null, -1, -1);
        isProtected = false;
    }
    IEnumerator ObtainedPowerUp(string name, Texture2D icon)
    {
        isProtected = true;
        //yield return new WaitForSecondsRealtime(5f);
        gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(icon, "Obtained " + name, -1, -1);
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<InventoryManager>().powerUpUI.GetComponent<PowerUpUI>().UpdatePowerup(null, null, -1, -1);
        isProtected = false;
    }
    IEnumerator CountDown(GameObject other)
    {
        Debug.Log("See if " + other + " has audio source");
        if (other.GetComponent<AudioSource>() != null)
        {
            Debug.Log(other.GetComponent<AudioSource>().clip);
            AudioSource newAudio = other.GetComponent<AudioSource>();
            //gameObject.SetActive(false);
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            newAudio.Play();
            Debug.Log("Audio Length: " + newAudio.clip.length);
            yield return new WaitForSeconds(newAudio.clip.length);
            //Destroy(other);
            Debug.Log("Killed Audio");
        }
        else
            Destroy(other);
    }
}
