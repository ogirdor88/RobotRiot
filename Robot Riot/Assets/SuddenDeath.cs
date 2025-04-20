using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeath : MonoBehaviour
{
    [SerializeField] private GameObject burgerPrefab;
    [SerializeField] private float minDelay = 0.2f;
    [SerializeField] private float maxDelay = 1.5f;
    private bool spawn = false;

    private void Update()
    {
        if (MatchTimer.suddenDeath && !spawn)
        {
            spawn = true;
            StartCoroutine(SuddenDeathSpawn());
        }
    }

    IEnumerator SuddenDeathSpawn()
    {
        while (MatchTimer.suddenDeath)
        {
            Instantiate(burgerPrefab, transform.position, Quaternion.identity);
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
        }
        spawn = true;
    }
}
