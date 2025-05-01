using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEngine.InputManagerEntry;

public class PlayerWinHandler : MonoBehaviour
{

    public TMP_Sprite[] WinSprites;

    public GameObject[] winUI;  // UI for winners
    public GameObject[] loseUI; // UI for losers

    public int winnerID;
    //private void Awake()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Scene Loaded: " + scene.name);

    //    if (scene.name == "Game Over")
    //    {
    //        //Debug.Log("sending player " + winID);
    //        WinningPlayer(GetComponent<WinTracker>().winID);

    //        //FindObjectOfType<PlayerWinHandler>().WinningPlayer(winID);
    //    }
    //}
    private void Start()
    {
        Debug.Log("Started win screen");
       WinningPlayer(FindAnyObjectByType<WinTracker>().winID);
    }

    private void Update()
    {
        //WinningPlayer(GetComponent<WinTracker>().winID);
    }

    //public void callWinn()
    //{
    //    WinningPlayer(GetComponent<WinTracker>().winID);
    //    Debug.Log("Calling win");
    //}
    public void WinningPlayer(int playerIndex)
    {
        winnerID = playerIndex;
        Debug.Log(playerIndex + " Player WINNN");
        winUI[playerIndex].SetActive(true);
    }

}
