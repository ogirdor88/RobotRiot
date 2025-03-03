using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _playerCC;
    private Rigidbody playerRB;
    private Vector2 moveDirection;
    private Vector2 input;


    private bool botMode;
    private bool isGrounded;
    private bool isSprinting;

    public bool shot = false;

    private float horizontal, vertical;

    [SerializeField]
    private bool moving;

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
    private float stamina, maxStamina, boostCost;

    private Coroutine recharge;

    [SerializeField]
    private GameObject cam;

    private void Awake()
    {
        _playerCC = gameObject.GetComponent<CharacterController>();
        playerRB = GetComponent<Rigidbody>();
        isGrounded = true;
        isSprinting = false;
        originalMoveSpeed = moveSpeed;
    }

    private void Start()
    {
        botMode = false;
    }
    private void Update()
    {
        updateMovement();
        UpdateLooking();

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
        if (vertical == 0 && horizontal == 0)
        {
            moving = false;
        }
    }
    public void CamMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void UpdateLooking()
    {
        rotateY += input.x * lookSense;
        rotateX += input.y * lookSense * -1;

        transform.localEulerAngles = new Vector3(0, rotateY, 0);

        rotateX = Mathf.Clamp(rotateX, -50f, 70f);
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
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (botMode)
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
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
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
        if (botMode)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            moveSpeed = moveSpeed * 1.25f;

            Debug.Log("Bot Mode");
        }

        if (!botMode)
        {
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

        while (stamina < maxStamina)
        {
            stamina += boostCost / 10f;
            //if the stamina bar gets full set the stamina to max stamina
            if (stamina > maxStamina) stamina = maxStamina;
            //update the stamina bar
            StaminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
