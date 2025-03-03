using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<Transform> startingPointsLvl2;
    [SerializeField] private List<LayerMask> playerLayers;
    private PlayerInputManager playerInputManager;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject moveImage;
    public int playerCount = 0;
    public int sceneNum = 0;
    public static PlayerManager Instance;
    private Dictionary<int, InputDevice> playerDevice = new();
    private Dictionary<int, Vector3> playerSpawnPosition = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        sceneNum = 0;
        moveImage.SetActive(false);
        startCamera.enabled = true;
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        StartCoroutine(TurnCamOff());
        Transform playerParent = player.transform;
        playerCount++;
        //Debug.Log("Player Number:" + playerCount);
        playerParent.position = startingPoints[players.Count -1].position;
        playerParent.rotation = startingPoints[players.Count - 1].rotation;
        player.gameObject.GetComponent<Health>().playerNumber = playerCount;
    }

    public void RegisterPlayer(PlayerInput player)
    {
        if (!playerDevice.ContainsKey(player.playerIndex))
        {
            playerDevice[player.playerIndex] = player.devices[0];
            playerSpawnPosition[player.playerIndex] = player.transform.position;
        }
    }
    public InputDevice GetPlayerDevice(int playerIndex)
    {
        return playerDevice.ContainsKey(playerIndex) ? playerDevice[playerIndex] : null;
    }

    public Vector3 GetSpawnPosition(int playerIndex)
    {
        return playerSpawnPosition.ContainsKey(playerIndex) ? playerSpawnPosition[playerIndex] : Vector3.zero;
    }

    public void SetSpawnPosition(int playerIndex, Vector3 position)
    {
        playerSpawnPosition[playerIndex] = position;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneNum++;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator TurnCamOff()
    {
        if(players.Count < 2)
        {
            moveImage.SetActive(true);
            startCamera.enabled = false;
            yield return new WaitForSeconds(0.1f);
            moveImage.SetActive(false);
        }
    }
}
