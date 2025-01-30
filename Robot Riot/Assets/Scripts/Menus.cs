using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

// Simple menus should be operated here.
public class Menus : MonoBehaviour
{
    public bool debugOnly = false;

    // Polish this stuff later
    public GameObject credits;
    public GameObject menu;

    private void Start()
    {
        if (debugOnly)
            if (Application.isEditor || Debug.isDebugBuild)
            {
                // Should make menu / hud viewable if in editor (easier testing)
                Debug.Log("Is in editor");
            }
            else
                Debug.Log("Not in editor");
    }

    //
    // Main menu
    //

    // Starts the game
    public void StartGame()
    {
        //This should lead to whatever the starting scene is
    }

    // Quit the game
    public void QuitGame()
    {
        QuitGame();
    }

    //
    // Credits
    //

    // Return the players to the menu from the credits
    public void GoToMainMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    //
    // Settings
    //
    public void Settings()
    {

    }

    //
    // Debug
    //

    // Open the credits
    // As it appears at the end of the game, it will probably take you back to the main menu scene.
    public void Credits()
    {
        //Temporarily put functions that might be used for Credits here
        credits.SetActive(true);
        menu.SetActive(false);
    }
}
