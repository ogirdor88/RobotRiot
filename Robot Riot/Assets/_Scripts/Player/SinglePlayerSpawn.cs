using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class SinglePlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    private void Awake()
    {
        PlayerInput.Instantiate(player1, 0, "Controls", -1, new[] { Gamepad.all[0] });
    }
}
