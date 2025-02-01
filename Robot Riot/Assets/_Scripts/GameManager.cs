using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startTime;
    public float matchTimer;
    public bool suddenDeath;

    private void Awake()
    {
        suddenDeath = false;
    }

    private void FixedUpdate()
    {
        matchTimer += Time.fixedDeltaTime;
        if (matchTimer >= 5 && !suddenDeath)
        {
            matchTimer = startTime;
            Debug.Log("reset");
            suddenDeath = true;
        }
    }
}
