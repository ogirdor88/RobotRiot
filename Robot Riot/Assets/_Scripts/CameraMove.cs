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
}
