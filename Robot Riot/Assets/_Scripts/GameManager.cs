using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Who won??
    public int loser;

    public static GameManager Instance;

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
}
