using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.XR;
//using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{

    public CharacterController _playerCC;
    private CapsuleCollider _playerCollider;
    public Animator animator;

    [Header("Bots Starting Values")]
    public GameObject combatAnimator;
    public Vector3 initialCombatPosition; // Store the initial local position
    public Quaternion initialCombatRotation;

    public GameObject botAnimator;
    public Vector3 initialBotPosition; // Store the initial local position
    public Quaternion initialBotRotation;

    [SerializeField] private GameObject lookAtRotator;
    [SerializeField] private Transform _camera;
    [SerializeField] private Slider sensSliderX;
    [SerializeField] private Slider sensSliderY;
    [SerializeField] private Text sensXValue;
    [SerializeField] private Text sensYValue;
    public GameObject canvas;

    private Vector3 _playerVelo;
    private Vector3 _jumpFoce;
    private Vector3 _moveInput = Vector3.zero;
    private Vector3 _moveDir = Vector3.zero;

    private Vector2 _cameraMove;

    public LayerMask layerMask;

    private float _jumpHieght = 1f;
    private float _gravity = -20;
    private float vertical;
    private float horizontal;
    private float originalMoveSpeed;

    private float xRotaion = 0f;
    private float lookSensX = 2.4f;
    private float lookSensY = 2.4f;
    private float lookSensXOriginal;
    private float lookSensYOriginal;

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

    public GameObject owner;

    public bool isChanging = false;

    [SerializeField]
    private CinemachineImpulseSource impulseScource;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = combatAnimator.GetComponent<Animator>();
        //InputDevice device = PlayerManager.Instance.GetPlayerDevice(playerInput.playerIndex);
        /*if (device != null)
        {
            playerInput.SwitchCurrentControlScheme(device);
        }*/
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
        lookSensXOriginal = lookSensX;
        lookSensYOriginal = lookSensY;
        RandomSword();
        botGEO.SetActive(false);
        botRootControl.SetActive(false);
        boostText.text = "" + (int)maxStamina;

        if (combatAnimator != null)
        {
            // Store the initial local position and rotation
            initialCombatPosition = combatAnimator.transform.localPosition;
            initialCombatRotation = combatAnimator.transform.localRotation;

            initialBotPosition = botAnimator.transform.localPosition;
            initialBotRotation = botAnimator.transform.localRotation;

            initialBotPosition = new Vector3(initialBotPosition.x, 0.28f, initialBotPosition.z);
        }
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
        sensSliderX.value = (lookSensX / 10f);
        sensSliderY.value = (lookSensY / 10f);
        sensSliderX.gameObject.SetActive(false);
        sensSliderY.gameObject.SetActive(false);

        

        
    }

    private void FixedUpdate()
    {
        UpdateCamera();
        
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.15f))
        {
            isGrounded = true;
            //impulseScource.GenerateImpulse();
        }
        else
        {
            isGrounded = false;
        }
        
        //Debug.Log("sprint " + isSprinting);
        UpdateMove();
        UpdateAnimation();
        UpdateJump();
        //UpdateCamera();
        

        if (!isSprinting)
        {
            StaminaBar.fillAmount = stamina / maxStamina;
        }
        if (Time.timeScale == 1f)
        {
            sensSliderX.gameObject.SetActive(false);
            sensSliderY.gameObject.SetActive(false);
        }
    }

    #region Movement
    private void UpdateMove()
    {
        if (isSprinting && botMode)
        {
            _playerSpeed = 10f;
            stamina -= boostCost * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isSprinting = false;
            }
            StaminaBar.fillAmount = stamina / maxStamina;
            boostText.text = "" + (int)stamina;
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
            boostText.text = "" + (int)stamina;
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
            //impulseScource.GenerateImpulse();
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
                lookSensX = 0;
                lookSensY = 0;
            }
            else
            {
                lookSensX = lookSensXOriginal;
                lookSensY = lookSensYOriginal;
            }

            float rotateX = _cameraMove.x * lookSensX;
            float rotateY = _cameraMove.y * lookSensY;

            transform.Rotate(Vector3.up * rotateX);

            xRotaion -= rotateY;
            xRotaion = Mathf.Clamp(xRotaion, -50f, 60f);
            _camera.transform.localRotation = Quaternion.Euler(xRotaion, 0f, 0f);
        
        
    }
    public void ChangeSensX()
    {
        lookSensXOriginal = sensSliderX.value * 10;
        sensXValue.text = lookSensXOriginal.ToString("F2");
    }
    public void ChangeSensY()
    {
        lookSensYOriginal = sensSliderY.value * 10;
        sensYValue.text = lookSensYOriginal.ToString("F2");
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f)
        {
            sensSliderX.gameObject.SetActive(true);
            sensSliderY.gameObject.SetActive(true);
        }
    }
    #endregion
    #region Shooting/Reload
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (botMode)
            {
                Debug.Log("Trap");
                istrapping = true;
                impulseScource.GenerateImpulse();
            }
            else
            {
                Debug.Log("Pew");

                isShooting = true;
                impulseScource.GenerateImpulse();
                //istrapping = true;

            }
        }
        //if (context.phase == InputActionPhase.Canceled && gameObject.GetComponent<InventoryManager>().inventory[gameObject.GetComponent<InventoryManager>().activeSlot].GetComponent<Weapon>().isContinousWeapon)
        //{
        //    isShooting = false;
        //}

        if (context.phase == InputActionPhase.Canceled)
        {
            if (botMode)
            {
                Debug.Log("Trap");
                istrapping = false;
            }
            else
            {
                Debug.Log("Pew");

                isShooting = false;
                //istrapping = true;

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
        if (isChanging == false)
        {
            StartCoroutine(SwitchMode());
        }
        // this is set up just for inital prototyping purposes
        // will be changed later
    }

    IEnumerator SwitchMode()
    {
        animator.Play("L3Combat_Transform");
        isChanging = true;
        yield return new WaitForSecondsRealtime(.4f);
        botMode = !botMode;
        gameObject.GetComponent<InventoryManager>().SwapSlot(true);
        if (botMode)
        {
            animator = botAnimator.GetComponent<Animator>();
            //animator.applyRootMotion = false;
            //combatAnimator.transform.position = new Vector3(combatAnimator.transform.position.x, 0f, combatAnimator.transform.position.z);
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
            //Animation animationComponent;
            animator.Play("L3Combat_Transform", 0, .3f);
            //gameObject.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.SetActive(false);


            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
            animator = combatAnimator.GetComponent<Animator>();
            //animator.applyRootMotion = false;
            //botAnimator.transform.position = new Vector3(botAnimator.transform.position.x, 0f, botAnimator.transform.position.z);
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
            animator.Play("L3Combat_Transform", 0, .3f);
            //gameObject.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(.1f);
        isChanging = false;
        StartCoroutine(ResetToCenter());

    }

    IEnumerator ResetToCenter()
    {

        float duration = 0.3f; // Duration of the lerp
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Lerp position and rotation
            combatAnimator.transform.localPosition = Vector3.Lerp(combatAnimator.transform.localPosition, initialCombatPosition, elapsedTime / duration);
            combatAnimator.transform.localRotation = Quaternion.Lerp(combatAnimator.transform.localRotation, initialCombatRotation, elapsedTime / duration);


            botAnimator.transform.localPosition = Vector3.Lerp(botAnimator.transform.localPosition, initialBotPosition, elapsedTime / duration);
            botAnimator.transform.localRotation = Quaternion.Lerp(botAnimator.transform.localRotation, initialBotRotation, elapsedTime / duration);
            yield return null;
        }

        // Ensure final position and rotation are set
        combatAnimator.transform.localPosition = initialCombatPosition;
        combatAnimator.transform.localRotation = initialCombatRotation;

        botAnimator.transform.localPosition = initialBotPosition;
        botAnimator.transform.localRotation = initialBotRotation;

        Debug.Log("Player position and rotation reset to center.");
    }


    #endregion
    #region Sprinting
    public void SpeedBoost(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && botMode)
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

    #region Pause
    public void PauseGame()
    {
        Debug.Log("Pausing Game");
        GameObject.FindObjectOfType<GameManager>().PauseGame();
    }
    #endregion
}
