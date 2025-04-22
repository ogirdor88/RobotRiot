using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTracker : MonoBehaviour
{
    public int loser;
    public static WinTracker Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
            Instance = this;

        DontDestroyOnLoad(this.gameObject);
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

}
