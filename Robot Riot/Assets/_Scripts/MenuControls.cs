using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControls : MonoBehaviour
{

    [SerializeField] private PlayerInputManager playerInputManager; 

    private void Start()
    {
        // Get all connected gamepads

        if (Gamepad.all.Count > 1)
        {
            for (int i = 1; i < Gamepad.all.Count; i++)
            {
                DisableController(Gamepad.all[i]);
            }
        }
    }

    private void Update()
    {
        if(playerInputManager != null)
        {
            if (playerInputManager.playerCount >= 1)
            {
                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    EnableController(Gamepad.all[i]);
                }
            }
        }
    }
    private void DisableController(Gamepad gamepad)
    {
        // Disable the actions for the given gamepad
        InputSystem.DisableDevice(gamepad);
    }
    public void EnableController(Gamepad gamepad)
    {
        // Enables the actions for the given gamepad
        InputSystem.EnableDevice(gamepad);
    }
}
