using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartCountdown : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> countdown;
    private bool startCount;
    public static bool isCountingDown;

    private void Awake()
    {
        startCount = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startCount)
        {
            isCountingDown = true;
            StartCoroutine(CountDown());
            isCountingDown=false;
            startCount = false;
        }
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
    }
}
