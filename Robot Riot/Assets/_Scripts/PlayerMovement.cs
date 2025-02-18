using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public Controls movePlayer;
    private Rigidbody playerRB;
    private Vector2 moveDirection;

   

    private bool botMode;
    private bool isGrounded;
    private bool isSprinting;

    public bool shot = false;

    private InputActionAsset inputAsset;
    private InputActionMap player;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction morph;
    private InputAction boost;
    private InputAction boostStop;
    private InputAction reload;
    private InputAction lookaround;
    //private InputAction scroll;

    private float horizontal, vertical;

    [SerializeField]
    private bool moving, looking;


    //cam stuff
    float rotateX = 0;
    float rotateY = 0;

    public float lookSense;


    [SerializeField]
    private float moveSpeed;
    private float originalMoveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private UnityEngine.UI.Image StaminaBar;

    [SerializeField]
    private float stamina, maxStamina , boostCost;

    private Coroutine recharge;

    [SerializeField]
    private GameObject cam;

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

        movePlayer = new Controls();
        playerRB = GetComponent<Rigidbody>();
        isGrounded = true;
        isSprinting = false;
        originalMoveSpeed = moveSpeed;
    }

    private void Start()
    {
        botMode = false;
    }
    private void OnEnable()
    {
       /* // Set up movement
        move = movePlayer.Player.Movement;
        move.Enable();
        move.performed += MovePlayer;
        move.canceled += StopPlayer;*/

        player.FindAction("move").started += MovePlayer;
        player.FindAction("move").canceled += StopPlayer;
        move = player.FindAction("Movement");
        player.Enable();

        /*lookaround = movePlayer.Player.Rotation;
        lookaround.Enable();*/
        
        
        player.FindAction("lookaround").started += CamMove;
        player.FindAction("lookaround").canceled += CamStop;
        lookaround = player.FindAction("Rotation");
        //lookaround.Enable();

        /* //set up Shooting
         fire = movePlayer.Player.Fire;
         fire.Enable();
         fire.performed += Shoot;*/

        player.FindAction("fire").started += Shoot;
        fire = player.FindAction("Fire");
        

        /*//set up jumping
        jump = movePlayer.Player.Jump;
        jump.Enable();
        jump.performed += Jump;*/

        player.FindAction("jump").started += Jump;
        jump = movePlayer.Player.Jump;

        /*//set up the transformation
        morph = movePlayer.Player.Transform;
        morph.Enable();
        morph.performed += SwitchModes;*/

        player.FindAction("morph").started += SwitchModes;
        morph = movePlayer.Player.Transform;

        /*//set up the Boost
        boost = movePlayer.Player.Boost;
        boost.Enable();
        boost.performed += SpeedBoost;*/

        player.FindAction("boost").started += SpeedBoost;
        boost = movePlayer.Player.Boost;

        /*//set up the Booststop
        boostStop = movePlayer.Player.BoostStop;
        boostStop.Enable();
        boostStop.performed += EndBoost;*/

        player.FindAction("boostStop").started += EndBoost;
        boostStop = movePlayer.Player.BoostStop;

        /*//set up the transformation
        reload = movePlayer.Player.Reload;
        reload.Enable();
        reload.performed += ReloadWeapon;*/

        player.FindAction("reload").started += ReloadWeapon;
        reload = movePlayer.Player.Reload;

        /* //set up the transformation
         scroll = movePlayer.Player.Scroll;
         scroll.Enable();
         scroll.performed += SwitchItems;*/

    }

    private void OnDisable()
    {
        /*move.Disable();
        fire.Disable();
        jump.Disable();
        morph.Disable();
        boost.Disable();
        reload.Disable();
        lookaround.Disable();*/
    }

    private void FixedUpdate()
    {
        /*//get the inputs for movement
        moveDirection = move.ReadValue<Vector2>();


        if (isSprinting)
        {
            //transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 3f);
            //transform.Translate(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 3f));


            stamina -= boostCost * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isSprinting = false;
            }
            StaminaBar.fillAmount = stamina / maxStamina;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            if(botMode)
            {
                //transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 1.25f);
                //transform.Translate(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 1.25f));
            }
            else
            {
                //transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * moveSpeed;
                //transform.Translate(new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * moveSpeed);
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
            }
        }*/

        updateMovement();
        UpdateLooking();

         /*rotateY += Input.GetAxis("Mouse X") * lookSense;
         rotateX += Input.GetAxis("Mouse Y") * lookSense * -1;*/

        /*rotateY += lookaround.ReadValue<Vector2>().x * lookSense;
        rotateX += lookaround.ReadValue<Vector2>().y * lookSense * -1;
        transform.eulerAngles = new Vector3(0, rotateY, 0);
        cam.transform.eulerAngles = new Vector3(Mathf.Clamp(rotateX, -35f, 70f), rotateY, 0);*/


        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.15f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<Vector2>().y;
        horizontal = context.ReadValue<Vector2>().x;
        moving = true;

    }
    public void StopPlayer(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<Vector2>().y;
        horizontal = context.ReadValue<Vector2>().x;
        if(vertical == 0 && horizontal == 0)
        {
            moving = false;
        }
    }
    private Vector2 input;
    public void CamMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        //rotateY += input.x * lookSense;
        //rotateX += input.y * lookSense * -1;
        looking = true;

        Debug.Log(context.ReadValue<Vector2>());
    }

    public void CamStop(InputAction.CallbackContext context)
    {
        //rotateY += context.ReadValue<Vector2>().x;
        //rotateX += context.ReadValue<Vector2>().y * -1;
        //if (rotateX == 0 && rotateY == 0)
        //{
        //    looking = false;
        //}
    }

    public void UpdateLooking()
    {
        rotateY += input.x * lookSense;
        rotateX += input.y * lookSense * -1;

        transform.localEulerAngles = new Vector3(0, rotateY, 0);

        rotateX = Mathf.Clamp(rotateX, -35f, 70f);
        // Rotate camera along X axis
        cam.transform.localEulerAngles = new Vector3(rotateX, 0, 0);
        
    }
    public void updateMovement()
    {
        if (moving)
        {
            transform.Translate((Vector3.forward * vertical) * Time.deltaTime * moveSpeed);
            transform.Translate((Vector3.right * horizontal) * Time.deltaTime * moveSpeed);
        }

/*        if (isSprinting)
        {
            if (moving)
            {
                transform.Translate((Vector3.forward * vertical) * Time.deltaTime * (moveSpeed * 3f));
                transform.Translate((Vector3.right * horizontal) * Time.deltaTime * (moveSpeed * 3f));
            }
            stamina -= boostCost * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isSprinting = false;
            }
            StaminaBar.fillAmount = stamina / maxStamina;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            if (botMode)
            {
                if (moving)
                {
                    transform.Translate((Vector3.forward * vertical) * Time.deltaTime * (moveSpeed * 1.25f));
                    transform.Translate((Vector3.right * horizontal) * Time.deltaTime * (moveSpeed * 1.25f));
                }
                else
                {
                    if (moving)
                    {
                        transform.Translate((Vector3.forward * vertical) * Time.deltaTime * moveSpeed);
                        transform.Translate((Vector3.right * horizontal) * Time.deltaTime * moveSpeed);
                    }
                }

            }
        }*/
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if(botMode)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = this.transform.position;
            Debug.Log("Deploying Trap");
        }
        else
        {
            Debug.Log("Pew");
            shot = true;
        }
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if(isGrounded)
        {
            isGrounded = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
        }
    }

    public void SwitchModes(InputAction.CallbackContext context)
    {
        botMode = !botMode;
        // this is set up just for inital prototyping purposes
        // will be changed later
        if(botMode)
        {
            /*transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);*/
            this.GetComponent<Renderer>().material.color = Color.green;
            moveSpeed = moveSpeed * 1.25f;
            
            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
            /*transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);*/
            this.GetComponent<Renderer>().material.color = Color.blue;
            moveSpeed = originalMoveSpeed;
            Debug.Log("Combat Mode");
        }
    }

    public void SpeedBoost(InputAction.CallbackContext context)
    {
        isSprinting = true;
        Debug.Log("Boost");
        moveSpeed = moveSpeed * 3f;
    }
    public void EndBoost(InputAction.CallbackContext context)
    {
        isSprinting = false;
        Debug.Log("BoostStopped");
        moveSpeed = originalMoveSpeed;
    }

    public void ReloadWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Reloading");
    }

    //wait 1 second before recharging the stamina bar
    public IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(stamina < maxStamina)
        {
            stamina += boostCost / 10f;
            //if the stamina bar gets full set the stamina to max stamina
            if(stamina > maxStamina) stamina = maxStamina;
            //update the stamina bar
            StaminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
