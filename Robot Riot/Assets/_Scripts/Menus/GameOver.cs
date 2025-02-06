using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Restart the previous battle
    // Currently, we can't go in-game yet so I'll have to figure this out later once we're further in development.
    public void Retry()
    {

    }

    // Return to title screen
    // Currently, we don't have a specific screen for the title, so this will be quickly added later.
    public void ReturntoTitle()
    {

    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
