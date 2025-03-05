using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchTimer : MonoBehaviour
{

    [Tooltip("Time in minutes")] public float startTime;
    [Tooltip("Time in minutes")] public float suddenDeathTime;
    public float currentTime;
    public Text timeText;

    public bool suddenDeath;
    public bool stop;


    [SerializeField] private PlayerManager playerManager;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
        if(playerManager.playerCount >= 2)
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
        

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentTime = startTime;
    }

    private void DisplayTime(float displayTime)
    {
        displayTime = Mathf.Max(displayTime, 0);
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float sec = Mathf.FloorToInt(displayTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, sec);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
