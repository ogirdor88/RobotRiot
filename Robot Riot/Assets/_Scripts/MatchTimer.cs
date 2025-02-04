using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour
{
    public float startTimeInSec;
    public float currentTime;
    public Text timeText;

    private void Start()
    {
        currentTime = startTimeInSec;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
        }
        else
        {
            Debug.Log("START SUDDEN DEATH");
        }
    }

    private void DisplayTime( float displayTime)
    {
        displayTime = Mathf.Max(displayTime, 0);
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float sec = Mathf.FloorToInt(displayTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, sec);
    }
}
