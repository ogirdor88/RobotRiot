using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Controls movePlayer;
    private Rigidbody playerRB;
    private Vector2 moveDirection;

    private bool botMode;
    private bool isGrounded;
    private bool isSprinting;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction morph;
    private InputAction boost;
    private InputAction boostStop;
    private InputAction reload;
    //private InputAction scroll;   


    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Image StaminaBar;

    [SerializeField]
    private float stamina, maxStamina , boostCost;

    private Coroutine recharge;

    private void Awake()
    {
        movePlayer = new Controls();
        playerRB = GetComponent<Rigidbody>();
        isGrounded = true;
        isSprinting = false;
    }

    private void Start()
    {
        botMode = false;
    }
    private void OnEnable()
    {
        // Set up movement
        move = movePlayer.Player.Movement;
        move.Enable();

        //set up Shooting
        fire = movePlayer.Player.Fire;
        fire.Enable();
        fire.performed += Shoot;

        //set up jumping
        jump = movePlayer.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        //set up the transformation
        morph = movePlayer.Player.Transform;
        morph.Enable();
        morph.performed += SwitchModes;

        //set up the Boost
        boost = movePlayer.Player.Boost;
        boost.Enable();
        boost.performed += SpeedBoost;

        //set up the Booststop
        boostStop = movePlayer.Player.BoostStop;
        boostStop.Enable();
        boostStop.performed += EndBoost;

        //set up the transformation
        reload = movePlayer.Player.Reload;
        reload.Enable();
        reload.performed += ReloadWeapon;

       /* //set up the transformation
        scroll = movePlayer.Player.Scroll;
        scroll.Enable();
        scroll.performed += SwitchItems;*/

    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
        morph.Disable();
        boost.Disable();
        reload.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        if(isSprinting)
        {
            transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 3f);

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
                transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * (moveSpeed * 1.25f);
            }
            else
            {
                transform.position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime * moveSpeed;
            }
        }
       

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

    private void Shoot(InputAction.CallbackContext context)
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
        }
    }

    private void Jump(InputAction.CallbackContext context) 
    {
        if(isGrounded)
        {
            isGrounded = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jump");
        }
    }

    private void SwitchModes(InputAction.CallbackContext context)
    {
        botMode = !botMode;
        // this is set up just for inital prototyping purposes
        // will be changed later
        if(botMode)
        {
            /*transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);*/
            this.GetComponent<Renderer>().material.color = Color.green;
            
            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
            /*transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);*/
            this.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log("Combat Mode");
        }
    }

    private void SpeedBoost(InputAction.CallbackContext context)
    {
        isSprinting = true;
        Debug.Log("Boost");
    }
    private void EndBoost(InputAction.CallbackContext context)
    {
        isSprinting = false;
        Debug.Log("BoostStopped");
    }

    private void ReloadWeapon(InputAction.CallbackContext context)
    {
        Debug.Log("Reloading");
    }

    //wait 1 second before recharging the stamina bar
    private IEnumerator RechargeStamina()
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
