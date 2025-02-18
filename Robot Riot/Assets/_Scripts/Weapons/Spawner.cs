using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items;
    [SerializeField]
    private bool random;
    [SerializeField]
    private int itemNumber;
    [SerializeField]
    private float spawndelay;

    private bool spawned;


    private void RandomSpawn()
    {
        //get a random number
        int rand = Random.Range(0, items.Count);

        //spawn the 
    }
}
