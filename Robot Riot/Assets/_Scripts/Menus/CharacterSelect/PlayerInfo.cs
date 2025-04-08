using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    public List<GameObject> characters;
    

    private void Awake()
    {
        characters = new List<GameObject>();
        DontDestroyOnLoad(this);
    }

    public void GetPlayerCharacter(GameObject prefab)
    {
        characters.Add(prefab);
    }

    public void WhoGoesWhere()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Debug.Log("Player" + i + " Chose" + characters[i]);
        }
    }
}
