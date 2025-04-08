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
        //ignore player input for a second so that the player can choose a character and not instantly get sent to the ready screen
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
        //instantiate the playercharacter
        //change the panels to the ready screen 
        //select the ready button
        PlayerCongifManager.instance.SetPlayerCharacter(playerindex, prefab);
        readyPanel1.SetActive(true);
        readyButton.Select();
        menuPanel1.SetActive(false);

    }

    public void ReadyPlayer()
    {
        //when the ready button is pressed set the player's ready bool to true
        // turn off the ready button to display the ready message
        if (!inputEnabled) { return; }
        PlayerCongifManager.instance.ReadyPlayer(playerindex);
        readyButton.gameObject.SetActive(false);
    }
}
