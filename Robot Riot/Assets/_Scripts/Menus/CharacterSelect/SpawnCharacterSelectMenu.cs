using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnCharacterSelectMenu : MonoBehaviour
{
    // this spawns the player select UI
    public GameObject characterSelectMenuPrefab;
    public PlayerInput input;
    private void Awake()
    {
        var rootmenu = GameObject.Find("MainLayout");
        if (rootmenu != null) 
        {
            var menu = Instantiate(characterSelectMenuPrefab, rootmenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<CharacterSelectMenuController>().SetPlayerIndex(input.playerIndex);
        }
    }
}
