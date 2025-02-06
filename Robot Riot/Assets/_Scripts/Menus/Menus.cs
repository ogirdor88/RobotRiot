using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

// Simple menus should be operated here.
public class Menus : MonoBehaviour
{
    // Old version of debug menu
    //public bool debugOnly = false;

    // Polish this stuff later
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject debug;

    private void Start()
    {
        //if (debugOnly)
        if ((Application.isEditor || Debug.isDebugBuild) && debug != null)
        {
            // Should make debug menu / hud viewable if in editor if available (easier testing)
            Debug.Log("Is in editor");
            debug.SetActive(true);
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
        Application.Quit();
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
        menu.SetActive(false);
        settings.SetActive(true);
    }

    //
    // Debug
    //

    // Open the credits
    // As it appears at the end of the game, it will probably take you back to the main menu scene.
    // Will adjust later if needed.
    public void Credits()
    {
        //Temporarily put functions that might be used for Credits here
        credits.SetActive(true);
        menu.SetActive(false);
    }
}
