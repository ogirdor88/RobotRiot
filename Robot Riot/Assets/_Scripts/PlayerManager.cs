using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    public List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject moveImage;

    private void Awake()
    {
        moveImage.SetActive(false);
        startCamera.enabled = true;
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    private void Update()
    {
        if (players.Count >= 2)
        {
            moveImage.SetActive(false);
            startCamera.enabled = false;
        }
    }


    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        moveImage.SetActive(true);
        startCamera.enabled = false;
        Transform playerParent = player.transform;
        
        playerParent.position = startingPoints[players.Count -1].position;
        playerParent.rotation = startingPoints[players.Count - 1].rotation;
    }
}
