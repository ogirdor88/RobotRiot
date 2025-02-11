using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        //get the offset from the camera and the ball
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move the camera to follow the player with the offset
        transform.position = player.transform.position - offset;
    }

    /*public Transform orient;
    public Transform player;
    public Transform playerObj;
    public Rigidbody body;

    public float rotationSpeed;

    private void Start()
    {
        //turn the cursor off
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orient.forward = viewDir.normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = orient.forward * vertical + orient.right * horizontal;

        if(inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed); 
        }
    }*/
}
