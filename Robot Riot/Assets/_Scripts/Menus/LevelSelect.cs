using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject select, buttonSelect;

    private int level, button;

    private bool onlevel;

    private void Awake()
    {
        onlevel = true;
        level = 1;
        button = 2;
        select.SetActive(true);
        buttonSelect.SetActive(false);
        MoveSelector();
    }

    private void MoveSelector()
    {
        if (onlevel) 
        {
            switch (level)
            {
                case 1:
                    select.transform.position = new Vector3(79, 567, 0);
                    break;
                case 2:
                    select.transform.position = new Vector3(568, 567, 0);
                    break;
                case 3:
                    select.transform.position = new Vector3(1057, 567, 0);
                    break;
                case 4:
                    select.transform.position = new Vector3(1544, 567, 0);
                    break;
            }
        }
        
        if (!onlevel)
        {
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
    public void SelectorRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(" LEVEL " + level + " And Button " + button);
            if (onlevel)
            {
                if (level < 4)
                    level++;
                else
                    level = 4;
            }

            if (!onlevel)
            {
                if (button < 2)
                    button++;
                else
                    button = 2;
            }

            MoveSelector();
        }
    }

    public void SelectorLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(" LEVEL " + level + " And Button " + button);
            if (onlevel)
            {
                if (level > 1)
                    level--;
                else
                    level = 1;
            }

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

    public void SelectLevels(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onlevel = true;
            select.SetActive(true);
            buttonSelect.SetActive(false);
        }
    }

    public void SelectButtons(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onlevel = false;
            buttonSelect.SetActive(true);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (!onlevel && button == 2)
            {
                switch (level)
                {
                    case 1:
                        SceneManager.LoadScene(2);
                        break;
                    case 2:
                        SceneManager.LoadScene(3);
                        break;
                    case 3:
                        SceneManager.LoadScene(4);
                        break;
                    case 4:
                        SceneManager.LoadScene(5);
                        break;
                }
            }
            if (!onlevel && button == 1)
            {
                SceneManager.LoadScene(0);
            }
            Debug.Log("Interact");
        }
    }
}
