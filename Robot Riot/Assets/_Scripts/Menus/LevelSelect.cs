using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject select, buttonSelect, lsUnfocused;

    private int level, button;

    private bool onlevel;

    private void Awake()
    {
        onlevel = true;
        level = 1;
        button = 2;
        select.SetActive(true);
        buttonSelect.SetActive(false);
        lsUnfocused.SetActive(false);
    }

    private void MoveSelector()
    {
        //if the selection is on the levels check the levels 
        if (onlevel) 
        {
            //switch the location of the selector based on the level number
            switch (level)
            {
                case 1:
                    select.transform.position = new Vector3(79, 567, 0);
                    lsUnfocused.transform.position = new Vector3(79, 567, 0);
                    break;
                case 2:
                    select.transform.position = new Vector3(568, 567, 0);
                    lsUnfocused.transform.position = new Vector3(568, 567, 0);
                    break;
                case 3:
                    select.transform.position = new Vector3(1057, 567, 0);
                    lsUnfocused.transform.position = new Vector3(1057, 567, 0);
                    break;
                case 4:
                    select.transform.position = new Vector3(1544, 567, 0);
                    lsUnfocused.transform.position = new Vector3(1544, 567, 0);
                    break;
            }
        }
        
        //if the selection is on the buttons check the buttons 
        if (!onlevel)
        {
            //move the location of the selector based on the button number
            switch(button)
            {
                case 1:
                    buttonSelect.transform.position = new Vector3(296, 88, 0);
                    break;
                case 2:
                    buttonSelect.transform.position = new Vector3(1622, 88, 0);
                    break;
            }
        }
        
    }
    
    //move the selector to the right
    public void SelectorRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log(" LEVEL " + level + " And Button " + button);
            //if the selector is on the levels
            //check to see if it is not at the last level
            //if not at the last level move the selecter over one 
            //if it gets to the last level keep it at the 4th position
            if (onlevel)
            {
                if (level < 4)
                    level++;
                else
                    level = 4;
            }

            //if the selector is on the buttons
            //check to see if it is not at the right
            //if not at the right button move the selecter over
            //if its already at the right button keep it there
            if (!onlevel)
            {
                if (button < 2)
                    button++;
                else
                    button = 2;
            }

            //update the positions after the numbers have changed
            MoveSelector();
        }
    }

    public void SelectorLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log(" LEVEL " + level + " And Button " + button);
            //if the selector is on the levels
            //check to see if it is not at the first level
            //if not at the first level move the selecter back one  
            //if it gets to the first level keep it at the 1st position
            if (onlevel)
            {
                if (level > 1)
                    level--;
                else
                    level = 1;
            }

            //if the selector is on the buttons
            //check to see if it is not at the left
            //if not at the left button move the selecter back
            //if its already at the left button keep it there
            if (!onlevel)
            {
                if (button > 1)
                    button--;
                else
                    button = 1;
            }

            MoveSelector();
        }
    }

    //switching to the level select
    public void SelectLevels(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //make the level selecting bool true
            //turn on the active level select visuals
            //turn off the unfocused selection visuals
            //turn off the button selecting visual
            onlevel = true;
            select.SetActive(true);
            lsUnfocused.SetActive(false);
            buttonSelect.SetActive(false);
        }
    }

    //switchingto button selection
    public void SelectButtons(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //make the level selecting bool false
            //turn off the active level select visuals
            //turn on the unfocused selection visuals
            //turn on the button selecting visual
            onlevel = false;
            select.SetActive(false);
            lsUnfocused.SetActive(true);
            buttonSelect.SetActive(true);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //if the selector is on the next button 
            //when you press a check the level that is selected then change the scene
            if (!onlevel && button == 2)
            {
                switch (level)
                {
                    case 1:
                        SceneManager.LoadScene(3);
                        break;
                    case 2:
                        SceneManager.LoadScene(4);
                        break;
                    case 3:
                        SceneManager.LoadScene(5);
                        break;
                    case 4:
                        SceneManager.LoadScene(6);
                        break;
                }
            }

            //if the selector is on the back button change the scene to the main menu
            if (!onlevel && button == 1)
            {
                SceneManager.LoadScene(0);
            }
            Debug.Log("Interact");
        }
    }
}
