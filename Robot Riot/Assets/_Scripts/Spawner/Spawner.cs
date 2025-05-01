using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items;
    [SerializeField]
    private bool random, pickup;
    [SerializeField]
    private int itemNumber;
    [SerializeField]
    private float spawndelay;

    private int slotNum;
    private bool spawned;
    //private GameObject spawnedItem;

    private void Update()
    {
       if (!spawned && !MatchTimer.suddenDeath)
        {
            if (random)
            {
                RandomSpawn();
            }
            else
            {
                ConstantSpawn(itemNumber);
            }
        }
    }

    private void RandomSpawn()
    {
        //get a random number
        int rand = Random.Range(0, items.Count);

        //spawn the random item
        //spawnedItem = Instantiate(items[rand], new Vector3(transform.position.x,transform.position.y + .5f, transform.position.z), Quaternion.identity);
        slotNum = items[rand].GetComponent<DeployTrapWeapon>().slot;
        Debug.Log(items[rand].name + " Slot:" + slotNum);
       Instantiate(items[rand], new Vector3(transform.position.x,transform.position.y + .5f, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
       spawned = true;
    }

    private void ConstantSpawn(int index)
    {
        //spawnedItem = Instantiate(items[index], new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
        slotNum = items[index].GetComponent<DeployTrapWeapon>().slot;
        Debug.Log(items[index].name + " Slot:" + slotNum);
        Instantiate(items[index], new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
        spawned = true;
    }

    private void FindSlot(//pass a game object here)
        )
    {
        //check for slot number here 
    }
    private IEnumerator NewSpawnDelay()
    {
        yield return new WaitForSeconds(spawndelay);
        spawned = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            //Destroy(spawnedItem);
            StartCoroutine(NewSpawnDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
             
        }
    }
}
