using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class CheckFirstPlayer : MonoBehaviour
{
    InputDevice player1;
    // Start is called before the first frame update
    void Start()
    {
        player1 = InputSystem.devices[3];

        Debug.Log("Player 2 is " + player1);
        
        InputSystem.DisableDevice(player1);
    }

}
