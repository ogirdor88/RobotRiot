using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SplitScreenCamera : MonoBehaviour
{
    // These are for if the player is already spawned in.
    //public bool isPlayer1;
    // This should allow for easier testing so you don't have to look at half of a screen
    //public bool singleScreen;

    [SerializeField] private Camera myCamera;

    private void Start()
    {
        //SetPlayer();
    }

    public void SetPlayer(bool isPlayer1, bool singleScreen = false)
    {
        if (!singleScreen)
        {
            if (isPlayer1)
                myCamera.rect = new Rect(-0.5f, 0f, 1f, 1f);
            else
                myCamera.rect = new Rect(0.5f, 0f, 1f, 1f);
        }
        else
            myCamera.rect = new Rect(0, 0f, 1f, 1f);
    }
}
