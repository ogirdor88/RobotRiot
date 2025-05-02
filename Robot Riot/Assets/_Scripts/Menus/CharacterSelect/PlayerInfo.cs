using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    public GameObject[] characters;
    

    private void Awake()
    {
        characters = new GameObject[];
        DontDestroyOnLoad(this);
    }

    public void GetPlayerCharacter(GameObject prefab , bool check)
    {
        if(check) 
        {
            characters[0] = prefab;
        }
        else 
        {
            characters[1] = prefab; 
        }
        
    }

    public void WhoGoesWhere()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            Debug.Log("Player" + i + " Chose " + characters[i]);
        }
    }
}
