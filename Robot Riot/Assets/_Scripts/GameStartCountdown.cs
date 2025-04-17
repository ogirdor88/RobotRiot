using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartCountdown : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> countdown;
    private bool startCount, countFinished;
    public static bool isCountingDown;

    private void Awake()
    {
        startCount = true;
    }

    void Start()
    {
        isCountingDown = true;
        countFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GamerTEST " +isCountingDown);
        if(startCount)
        {
            isCountingDown = true;
            StartCoroutine(CountDown());
            startCount = false;
        }
        FightTime();
    }

    private IEnumerator CountDown()
    {
        foreach(GameObject go in countdown) 
        {
            go.SetActive(true);
            Debug.Log("on " + go.name);
            yield return new WaitForSeconds(1f);
            go.SetActive(false);
            Debug.Log("off " + go.name);
            yield return new WaitForSeconds(1);
        } 
        countFinished = true;
    }

    private void FightTime()
    {
        if(!startCount && countFinished)
        {
            isCountingDown=false;
        }
    }
}
