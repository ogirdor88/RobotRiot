using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenuController : MonoBehaviour
{
    private int playerindex;
    [SerializeField]
    private GameObject readyPanel1;
    [SerializeField]
    private GameObject menuPanel1;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime) 
        {
            inputEnabled = true;
        }
        
    }

    public void SetPlayerIndex(int pi)
    {
        playerindex = pi;
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetCharacter(GameObject prefab)
    {
        if(!inputEnabled) { return; }

        PlayerCongifManager.instance.SetPlayerCharacter(playerindex, prefab);
        readyPanel1.SetActive(true);
        readyButton.Select();
        menuPanel1.SetActive(false);

    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        PlayerCongifManager.instance.ReadyPlayer(playerindex);
        readyButton.gameObject.SetActive(false);
    }
}
