using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
        Debug.Log("Items");
    }

    // Not implemented yet
    public void LevelSelect()
    {
        Debug.Log("Levels");
    }

    // Not implemented yet
    public void BotSelect()
    {
        Debug.Log("bots");
    }

    // No menu scene yet.
    public void ReturntoMenu()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Destroy(GameObject.Find("PlayerInfo"));
        Destroy(GameObject.Find("WinTracker"));
        Destroy(GameObject.Find("Factory"));
        Destroy(GameObject.Find("City"));
        Destroy(GameObject.Find("Oasis"));
        Destroy(GameObject.Find("West"));
        SceneManager.LoadScene("Title");
    }
}
