using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{

    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private List<Transform> startingPointsLvl2;
    
    private PlayerInputManager playerInputManager;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject moveImage;
    public int playerCount = 0;
    public int sceneNum = 0;
    public static PlayerManager Instance;
    private Dictionary<int, InputDevice> playerDevice = new();
    private Dictionary<int, Vector3> playerSpawnPosition = new();

    [SerializeField] private GameObject player1;
    [Header("Player Layer Cameras")]
    [SerializeField] public List<LayerMask> playerLayers;
    //[SerializeField] public List<mask>
    // public List<LayerMask> playerLayers;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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

        GetCharacter();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;

        Debug.Log("Functional");
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
        Debug.Log("Player Number:" + playerCount);

        playerParent.position = startingPoints[players.Count - 1].position;
        playerParent.rotation = startingPoints[players.Count - 1].rotation;

        //Convert Layer mask from bit to int
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        //playerParent.GetComponentInChildren<CinemachineBrain>().gameObject.layer = layerToAdd;

        
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        //playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;


        //set the channelmasking for cinemachines recoil channels
        if(playerCount < 2)
        {
            playerCount = 1;
            
        }
        else if(playerCount == 2)
        {
            playerCount = 3;
        }     
        playerParent.GetComponentInChildren<CinemachineIndependentImpulseListener>().m_ChannelMask = playerCount + 1;

        Debug.Log(playerCount + "PlayerCount");


        //player.gameObject.GetComponent<Health>().playerNumber = playerCount + 1;
    }

    public void RegisterPlayer(PlayerInput player)
    {
        if (!playerDevice.ContainsKey(player.playerIndex))
        {
            playerDevice[player.playerIndex] = player.devices[0];
            playerSpawnPosition[player.playerIndex] = player.transform.position;
        }
    }
    /*public InputDevice GetPlayerDevice(int playerIndex)
    {
        return playerDevice.ContainsKey(playerIndex) ? playerDevice[playerIndex] : null;
    }*/

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

    /*private void GetCharacters()
    {
        GameObject info = GameObject.Find("PlayerInfo");

        GameObject player1, player2;

        player1 = info.GetComponent<PlayerInfo>().characters[0];
        player2 = info.GetComponent<PlayerInfo>().characters[1];

        PlayerInput.Instantiate(player1, 0, "Controls", -1,new[] { Gamepad.all[0] });
        //player1.GetComponentInChildren<CinemachineBrain>().gameObject.layer = LayerMask.NameToLayer("Player 1");

        PlayerInput.Instantiate(player2, 1, "Controls", -1,new[] { Gamepad.all[1] });
        //player2.GetComponentInChildren <CinemachineBrain>().gameObject.layer = LayerMask.NameToLayer("Player 2");
    }*/
    
    private void GetCharacter()
    {
        PlayerInput.Instantiate(player1, 0, "Controls", -1, new[] { Gamepad.all[0] });
    }
}
