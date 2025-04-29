using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    // Not implemented yet
    public void ItemList()
    {

    }

    // Not implemented yet
    public void LevelSelect()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene("LevelSelect");
    }

    // Not implemented yet
    public void BotSelect()
    {

    }

    // No menu scene yet.
    public void ReturntoMenu()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene("Title");
    }
}
