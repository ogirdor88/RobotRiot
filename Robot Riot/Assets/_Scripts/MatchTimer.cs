using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MatchTimer : MonoBehaviour
{

    [Tooltip("Time in minutes")] public float startTime;
    [Tooltip("Time in minutes")] public float suddenDeathTime;
    public float currentTime;
    public Text timeText;

    public static bool suddenDeath;
    public bool stop;
    bool clipDone = false;

    float clipLength;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private VideoPlayer sdClip;

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

        if(sdClip != null)
        {
            clipLength = (float)sdClip.length; 
        }
    }
    private void Update()
    {
        if(sdClip == null)
        {
            if (!GameStartCountdown.isCountingDown)
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
        else
        {
            if (!GameStartCountdown.isCountingDown)
            {
                if (currentTime > 0 && !stop)
                {
                    currentTime -= Time.deltaTime;
                    DisplayTime(currentTime);
                }
                else if (!stop && clipDone)
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
        Debug.Log("PEEM");
    }

    IEnumerator PlayClip()
    {
        sdClip.Play();
        yield return new WaitForSeconds(clipLength);
        Destroy(sdClip.gameObject);
        clipDone = true;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
