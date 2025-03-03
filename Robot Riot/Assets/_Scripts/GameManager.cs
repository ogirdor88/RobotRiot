using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Who won??
    public int loser;

    public static GameManager Instance;

    /*public PlayerInputManager playerOne;
    public PlayerInputManager playerTwo;*/

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Figure out who won and send them to the game over screen
    public void GameOver(int playerLost)
    {
        loser = playerLost;
        SceneManager.LoadScene(3);
    }

    /*public void RegisterPlayer(PlayerInputManager player)
    {
        if(playerOne == null)
        {
            playerOne = player;
        }
        else if(playerTwo == null)
        {
            playerTwo = player;
        }
    }*/
}
