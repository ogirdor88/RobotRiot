using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnStart : MonoBehaviour
{
    public GameObject timer;
    private void Start()
    {
        timer.SetActive(true);
    }
}
