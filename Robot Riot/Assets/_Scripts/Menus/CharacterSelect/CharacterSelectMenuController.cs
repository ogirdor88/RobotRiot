using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectMenuController : MonoBehaviour
{
    private int playerindex;
    [SerializeField]
    private GameObject readyPanel1;
    [SerializeField]
    private GameObject menuPanel1;
    [SerializeField]
    private Button readyButton, l3Button, fleaButton, tankerButton, remButton;
    [SerializeField]
    private TMP_Text CharacterName, CharacterDescription;

    public static bool l3, flea, tanker, rem;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    private void Awake()
    {
        l3 = false;
        flea = false;
        tanker = false;
        rem = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();

        //ignore player input for a second so that the player can choose a character and not instantly get sent to the ready screen
        if(Time.time > ignoreInputTime) 
        {
            inputEnabled = true;
        }
        updateButtons();
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

        if(prefab.name == "L3_PlayerHolder") 
        {
            l3 =true;
        }
        if (prefab.name == "Flea_Player")
        {
            flea = true;
        }
        if (prefab.name == "Tanker_Player")
        {
            tanker = true;
        }
        if (prefab.name == "Rem_Player")
        {
            rem = true;
        }

    }

    public void ReadyPlayer()
    {
        //when the ready button is pressed set the player's ready bool to true
        // turn off the ready button to display the ready message
        if (!inputEnabled) { return; }
        PlayerCongifManager.instance.ReadyPlayer(playerindex);
        readyButton.gameObject.SetActive(false);
    }

    //checks which button is active and changes the character name and description text accordingly
    private void ChangeText()
    {
        if (EventSystem.current.currentSelectedGameObject == l3Button.gameObject)
        {
            CharacterName.text = "L3";
            CharacterDescription.text = "L3 is the self-proclaimed hero to his own story." +
                " He is always ready for adventure but somehow always gets dragged into Flea's shenanigans." +
                " Is unsuccessful in getting out of them.";
        }
        if (EventSystem.current.currentSelectedGameObject == fleaButton.gameObject)
        {
            CharacterName.text = "Flea";
            CharacterDescription.text = "Flea is the shifty one of the bunch," +
                " and he always tries to be sneaky among the others." +
                " He is the master planner out of our star cast," +
                " but like most of his plans, the execution is not there. Like at all.";
        }
        if (EventSystem.current.currentSelectedGameObject == tankerButton.gameObject)
        {
            CharacterName.text = "Tanker";
            CharacterDescription.text = "Tanker is the gentle giant and resident softie at heart." +
                " He seems like he doesn't care about anyone other than Rem," +
                " but he's grown to love his bot family. Even Flea.";
        }
        if (EventSystem.current.currentSelectedGameObject == remButton.gameObject)
        {
            CharacterName.text = "Rem";
            CharacterDescription.text = "Rem is the sensitive soul among her bot brothers," +
                " always cleaning up after L3 and Flea's messes." +
                " She joins Tanker when he watches old films they found around The Village," +
                " but knows how to have a good time and joins in with her brothers; mischief.";
        }

    }

    public void DisableL3()
    {
        l3 = true;
        
    }
    public void DisableFlea()
    {
        flea = true;
       
    }
    public void DisableTanker()
    {
        tanker = true;
        
    }
    public void DisableRem()
    {
        rem = true;
    }

    private void updateButtons()
    {
        if (l3)
        {
            l3Button.GetComponent<Button>().interactable = false;
        }
        if (flea)
        {
           fleaButton.GetComponent<Button>().interactable = false;
        }
        if (tanker)
        {
            tankerButton.GetComponent<Button>().interactable = false;
        }
        if (rem)
        {
            remButton.GetComponent<Button>().interactable = false;
        }
    }
}
