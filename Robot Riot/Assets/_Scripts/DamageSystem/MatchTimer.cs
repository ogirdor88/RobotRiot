using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : MonoBehaviour
{

    [Tooltip("Time in minutes")] public float startTime;
    [Tooltip("Time in minutes")] public float suddenDeathTime;
    public float currentTime;
    public Text timeText;

    public bool suddenDeath;
    public bool stop;

    private void Awake()
    {
        //converts time to seconds
        startTime *= 60;
        suddenDeathTime *= 60;
    }
    private void Start()
    {
        suddenDeath = false;
        stop = false;
        currentTime = startTime;
    }
    private void Update()
    {
        if (currentTime > 0 && !stop)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
        }
        else if (!stop)
        {
            currentTime = suddenDeathTime;
            suddenDeath = true;
            Debug.Log("START SUDDEN DEATH");
        }
        if (currentTime <= 0 && suddenDeath)
        {
            Debug.Log("Over");
            stop = true;
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
