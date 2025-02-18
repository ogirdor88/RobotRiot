using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    float rotateX = 0;
    float rotateY = 0;

    public float lookSense;
    // Update is called once per frame
    void Update()
    {
        rotateX += Input.GetAxis("Mouse Y") * lookSense * -1;
        rotateY += Input.GetAxis("Mouse X") * lookSense;
        transform.localEulerAngles = new Vector3(0, rotateY, 0);
     }
}
