using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneSwitch : MonoBehaviour
{
    [SerializeField] private TMP_Text playerWonText;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<GameManager>())
        {
            playerWonText.text = "Player " + GameObject.FindObjectOfType<GameManager>().loser.ToString() + " loses!";
        }
        else
        {
            playerWonText.text = "Nobody wins";
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void HowToScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
		SceneManager.LoadScene(2);
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
