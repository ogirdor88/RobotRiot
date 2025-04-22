using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfiguration : MonoBehaviour
{
    public PlayerConfiguration(PlayerInput input)
    {
        playerIndext = input.playerIndex;
        Input = input;
    }

    public PlayerInput Input { get; set; }
    public int playerIndext { get; set; }
    public bool isReady { get; set; }

    public GameObject PlayerPrefab { get; set; }
}
