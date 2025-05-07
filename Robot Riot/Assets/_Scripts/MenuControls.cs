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
        var gamepads = Gamepad.all;

        if (gamepads.Count > 1)
        {
            for (int i = 1; i < gamepads.Count; i++)
            {
                DisableController(gamepads[i]);
            }
        }

        for (int i = 0; i <= gamepads.Count; i++)
        {
            Debug.LogWarning(i);
        }
    }

    private void Update()
    {
        if(playerInputManager != null)
        {
            if (playerInputManager.playerCount >= 1)
            {
                EnableController(Gamepad.all[1]);
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
