using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Who won??
    public int loser;

    public static GameManager Instance;

    public GameObject pauseScreen;
    [SerializeField]
    private Button resumeButton;

    /*public PlayerInputManager playerOne;
    public PlayerInputManager playerTwo;*/

    private void Awake()
    {
        FindAnyObjectByType(typeof(GameManager));
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
            return;
        }
        else
            Instance = this;
    }

    // Figure out who won and send them to the game over screen
    public void GameOver(int playerLost)
    {
        loser = playerLost;
        //SceneManager.LoadScene(3);
        Debug.Log("Player Lost:" + playerLost);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game Over");
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        resumeButton.Select();
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
