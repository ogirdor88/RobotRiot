using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Unity.VisualScripting;
using Unity.Burst.Intrinsics;

public class MatchTimer : MonoBehaviour
{

    [Tooltip("Time in minutes")] public float startTime;
    [Tooltip("Time in minutes")] public float suddenDeathTime;
    public float currentTime;
    public Text timeText;

    public static bool suddenDeath;
    public bool stop;

    public static bool camOn;

    float clipLength;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private VideoPlayer sdClip;
    [SerializeField] private Camera mainCam;

    private void Awake()
    {
        //converts time to seconds
        startTime *= 60;
        suddenDeathTime *= 60;
    }
    private void Start()
    {
        camOn = false;
        suddenDeath = false;
        stop = false;
        currentTime = startTime;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(sdClip != null)
        {
            clipLength = (float)sdClip.length;
            Debug.LogWarning("GOT LENGTH");
        }
    }
    private void Update()
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
                timeText.text = "Sudden Death";
                suddenDeath = true;
                if (sdClip != null)
                {
                    StartCoroutine(PlayClip());
                    Debug.LogWarning("CLIP PLAY IN IF");
                }
                else
                {
                    suddenDeath = true;
                    camOn = true;
                    Debug.LogWarning("CLIP NO PLAY IN IF");
                }
                //Debug.Log("START SUDDEN DEATH");
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
        //Debug.Log("PEEM");
    }

    IEnumerator PlayClip()
    {
        mainCam.enabled = true;
        sdClip.Play();
        Debug.LogWarning("CLIP PLAY FIRST");
        yield return new WaitForSeconds(clipLength);
        camOn = true;
        stop = true;
        Debug.LogWarning("CLIP PLAY SECOND");
        Destroy(sdClip.gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
