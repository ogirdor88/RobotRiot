using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
//using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public CharacterController _playerCC;
    private CapsuleCollider _playerCollider;
    public Animator animator;
    public GameObject animatorCombat;
    [SerializeField] private Transform _camera;

    private Vector3 _playerVelo;
    private Vector3 _jumpFoce;
    private Vector3 _moveInput = Vector3.zero;
    private Vector3 _moveDir = Vector3.zero;

    private Vector2 _cameraMove;

    private float _jumpHieght = 1f;
    private float _gravity = -20;
    private float vertical;
    private float horizontal;
    private float originalMoveSpeed;

    private float xRotaion = 0f;
    private float lookSens = 1.8f;
    private float lookSensOriginal;

    public float _playerSpeed = 4f;

    private bool isSprinting = false;
    private bool isGrounded;
    private bool isJumping;
    private bool jump;
    public bool botMode = false;
    private bool slide = false;

    public bool isShooting = false; 
    public bool istrapping = false;


    public int bonusDamage;

    [SerializeField]
    private List<GameObject> swords;
    [SerializeField]
    private GameObject combatGEO, combatRootControl, botGEO, botRootControl;

    private PlayerInput playerInput;

    //Boost Variable
    [SerializeField]
    private UnityEngine.UI.Image StaminaBar;
    [SerializeField]
    private TMP_Text boostText;
    [SerializeField]
    public float stamina, maxStamina, boostCost;
    private Coroutine recharge;

    private float smoothMoveX;
    private float smoothMoveY;
    private float animationDampTime = 0.1f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = animatorCombat.GetComponent<Animator>();
        InputDevice device = PlayerManager.Instance.GetPlayerDevice(playerInput.playerIndex);
        if (device != null)
        {
            playerInput.SwitchCurrentControlScheme(device);
        }
        Vector3 spawnPos = PlayerManager.Instance.GetSpawnPosition(playerInput.playerIndex);
        if(spawnPos != Vector3.zero)
        {
            transform.position = spawnPos;
        }


        _playerCC = gameObject.AddComponent<CharacterController>();
        _playerCollider = gameObject.AddComponent<CapsuleCollider>();
        originalMoveSpeed = _playerSpeed;
        botMode = false;
        isShooting = false;
        lookSensOriginal = lookSens;
        RandomSword();
        botGEO.SetActive(false);
        botRootControl.SetActive(false);
    }

    private void Start()
    {
        PlayerManager.Instance.RegisterPlayer(playerInput);
        _playerCC.center = new Vector3(0f, 0.65f, 0.05f);
        _playerCC.height = 1.5f;
        _playerCC.radius = 0.5f;
        _playerCollider.center = _playerCC.center;
        _playerCollider.height = _playerCC.height;
        _playerCollider.radius = _playerCC.radius;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.15f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        Debug.Log("sprint " + isSprinting);
        UpdateMove();
        UpdateJump();
        UpdateCamera();
        UpdateAnimation();

        if (!isSprinting)
        {
            StaminaBar.fillAmount = stamina / maxStamina;
        }
    }

    #region Movement
    private void UpdateMove()
    {
        if (isSprinting)
        {
            _playerSpeed = 10f;
            stamina -= boostCost * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isSprinting = false;
            }
            StaminaBar.fillAmount = stamina / maxStamina;
            boostText.text = "Boost: " + (int)stamina + "/" + (int)maxStamina;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            _playerSpeed = originalMoveSpeed;
        }
        _moveInput = transform.right * horizontal + transform.forward * vertical;
        _playerCC.Move(_moveInput * _playerSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;
    }

    public IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (stamina < maxStamina)
        {
            stamina += boostCost / 10f;
            //if the stamina bar gets full set the stamina to max stamina
            if (stamina > maxStamina) stamina = maxStamina;
            //update the stamina bar
            StaminaBar.fillAmount = stamina / maxStamina;
            boostText.text = "Boost: " + (int)stamina + "/" + (int)maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
    #endregion
    #region Jump

    private void UpdateJump()
    {
        if (isGrounded && _jumpFoce.y < 0)
        {
            _jumpFoce.y = -3f;
            jump = true;
        }
        _jumpFoce.y += _gravity * Time.deltaTime;
        _playerCC.Move(_jumpFoce * Time.deltaTime);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (jump)
        {
            _jumpFoce.y = Mathf.Sqrt(_jumpHieght * -3f * _gravity);
        }
        jump = false;
    }
    #endregion
    #region Camera
    public void CamMove(InputAction.CallbackContext context)
    {
        _cameraMove = context.ReadValue<Vector2>();
    }
    public void UpdateCamera()
    {
        if (slide)
        {
            lookSens = 0;
        }
        else
        {
            lookSens = lookSensOriginal;
        }

        float rotateX = _cameraMove.x * lookSens;
        float rotateY = _cameraMove.y * lookSens;

        transform.Rotate(Vector3.up * rotateX);

        xRotaion -= rotateY;
        xRotaion = Mathf.Clamp(xRotaion, -50f, 60f);
        _camera.transform.localRotation = Quaternion.Euler(xRotaion, 0f, 0f);
    }
    #endregion
    #region Shooting/Reload
    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            if (botMode)
            {
                /* GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                 cube.transform.position = this.transform.position;*/
                Debug.Log("Trap");
                istrapping = true;
            }
            else
            {
                Debug.Log("Pew");
                
                isShooting = true;
                istrapping = true;

            }
        }
    }
    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Reloading");
    }
    #endregion
    #region Bot Mode
    public void SwitchModes(InputAction.CallbackContext context)
    {
        botMode = !botMode;
        // this is set up just for inital prototyping purposes
        // will be changed later
        if (botMode)
        {
            botGEO.SetActive(true);
            botRootControl.SetActive(true);

            combatGEO.SetActive(false);
            combatRootControl.SetActive(false);
            _playerCC.center = new Vector3(0f, 0.6f, 0f);
            _playerCC.height = 1f;
            _playerCC.radius = 0.35f;
            _playerCollider.center = _playerCC.center;
            _playerCollider.height = _playerCC.height;
            _playerCollider.radius = _playerCC.radius;

            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
            /*this.GetComponent<Renderer>().material.color = Color.blue;
            _playerSpeed = originalMoveSpeed;*/
            Debug.Log("Combat Mode");
            botGEO.SetActive(false);
            botRootControl.SetActive(false);

            combatGEO.SetActive(true);
            combatRootControl.SetActive(true);
            _playerCC.center = new Vector3(0f, 0.65f, 0.05f);
            _playerCC.height = 1.5f;
            _playerCC.radius = 0.5f;
            _playerCollider.center = _playerCC.center;
            _playerCollider.height = _playerCC.height;
            _playerCollider.radius = _playerCC.radius;
        }
    }
    #endregion
    #region Sprinting
    public void SpeedBoost(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isSprinting = true;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            isSprinting = false;
        }
    }
    public void EndBoost(InputAction.CallbackContext context)
    {
        //isSprinting = false;
        Debug.Log("BoostStopped");
        //_playerSpeed = originalMoveSpeed;
    }
    #endregion
    #region Animation
    private void UpdateAnimation()
    {
        //Move Animations
        smoothMoveX = Mathf.Lerp(smoothMoveX, horizontal, animationDampTime);
        smoothMoveY = Mathf.Lerp(smoothMoveY, vertical, animationDampTime);
        animator.SetFloat("Velocity X", smoothMoveX);
        animator.SetFloat("Velocity Z", smoothMoveY);
        bool isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("IsMoving", isMoving);

        //Jump animation
        animator.SetBool("Jump", !jump);
        
        //Bot Place trap animation

    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        //when the player enters the oil trap, get a reffrence to the character's direction and speed
        //then you set slide bool to be true
        if(other.tag == "Oil")
        {
            slide = true;
            _moveDir = _moveInput;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when you exit the oil trap set the bool to false
        if (other.tag == "Oil")
        {
            slide = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //while you are in the oil make the player slide
        if (other.tag == "Oil")
        {
            OilSlide();
        }
    }
    #region Slide
    private void OilSlide()
    {
        //move the player in the direction that they entered the oil and double the speed to make it seem slick
        _playerCC.Move(_moveDir * _playerSpeed*2 * Time.deltaTime);
    }
    #endregion

    #region Sword
    private void RandomSword()
    {
        // get a random number from 0 to the sword count
        //tunr on that sword
        int rand = Random.Range(0, swords.Count);
        //swords[rand].gameObject.SetActive(true);
        Debug.Log("Creating Sword");
        gameObject.GetComponent<InventoryManager>().ForceAddWeapon(Instantiate(swords[rand]));
    }
    #endregion
}
