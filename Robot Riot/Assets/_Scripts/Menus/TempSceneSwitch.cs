using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempSceneSwitch : MonoBehaviour
{
    [SerializeField] private TMP_Text player1Text;
    [SerializeField] private TMP_Text player2Text;

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject items;
    [SerializeField] private Button creditsX, itemsX, creditsButton, itemsButton;

    private void Awake()
    {
        if (GameObject.FindObjectOfType<GameManager>())
        {
            Debug.Log("Found GameManager");
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            if (gameManager.loser == 1)
            {
                player1Text.text = "You Lost!";
                player2Text.text = "You Win!";
            }
            else if (gameManager.loser == 2)
            {
                player1Text.text = "You Win!";
                player2Text.text = "You Lose!";
            }
        }
        else
        {
            Debug.Log("Could not find GameManager or starting in gameover scene");
            player1Text.text = "Nobody wins";
            player2Text.text = "Nobody wins";
        }
        //credits.SetActive(false);
        //items.SetActive(false);
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

    public void ShowCredits()
    {
        credits.SetActive(true);
        creditsX.Select();
        Debug.Log("Credits");
    }

    public void ShowItems()
    {
        items.SetActive(true);
        itemsX.Select();
        Debug.Log("Items");
    }

    public void BackOutItems()
    {
        items.SetActive(false);
        itemsButton.Select();
    }
    public void BackOutCredits()
    {
        credits.SetActive(false);
        creditsButton.Select();
    }
}
