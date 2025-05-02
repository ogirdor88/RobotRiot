using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCongifManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField] 
    private int maxPlayer = 2;
     
    public static PlayerCongifManager instance { get; private set; }
    public GameObject playerInfo;

    private void Awake()
    {
        // if there is a playerConfigManager already, throw the debug message
        if (instance != null)
        {
            Debug.Log("Trying to creat another instance of a singleton");
        }
        else
        {
            //initialize the playerConfigManager
            //initialize the playerConfig list
            instance = this;
            //DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    //sets the player's character to their player index
    public void SetPlayerCharacter(int index, GameObject prefab)
    {
        playerConfigs[index].PlayerPrefab = prefab;
        playerInfo.GetComponent<PlayerInfo>().GetPlayerCharacter(prefab);
        playerInfo.GetComponent<PlayerInfo>().WhoGoesWhere();
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        //if all the players are joined and all the players are ready
        if(playerConfigs.Count == maxPlayer && playerConfigs.All(p => p.isReady == true)) 
        {
            //check which sceen they will go to 
            if(GameObject.Find("Factory"))
            {
                SceneManager.LoadScene("Lvl1_Facility");
            }
            else if (GameObject.Find("City"))
            {
                SceneManager.LoadScene("Lvl2_Village");
            }
            else if (GameObject.Find("Oasis"))
            {
                SceneManager.LoadScene("Lvl3_Oasis");
            }
            else if (GameObject.Find("West"))
            {
                SceneManager.LoadScene("Lvl4_West");
            }
            else
                SceneManager.LoadScene("Lvl1_Facility");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        //Debug.Log(pi.user);
        Debug.Log(pi.devices[0].name);
        
        if(!playerConfigs.Any(p => p.playerIndext == pi.playerIndex)) 
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}