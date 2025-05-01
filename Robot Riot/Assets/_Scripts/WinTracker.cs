using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTracker : MonoBehaviour
{
    public int loser;
    public static WinTracker Instance;

    public int winID;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
            Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this.gameObject);
    }

    // Figure out who won and send them to the game over screen
    public void GameOver(int PlayerWon)
    {
        loser = PlayerWon;
        winID = PlayerWon;
        
        Debug.Log("Player Won:" + winID);
        
        SceneManager.LoadScene("Game Over");
        

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        //winID = winID;
        if (scene.name == "Game Over")
        {
            Debug.Log("sending player " + winID);
            //FindObjectOfType<PlayerWinHandler>().WinningPlayer(loser);
           //Destroy(this);
        }
    }

}
