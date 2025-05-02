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

    [SerializeField]
    private bool spawned, canspawn;

    //private GameObject spawnedItem;
    private void Awake()
    {
        canspawn = true;
    }
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
        FindSlot(items[rand]);
        Debug.Log(items[rand].name + " Slot:" + slotNum);
       Instantiate(items[rand], new Vector3(transform.position.x,transform.position.y + .5f, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
       spawned = true;
    }

    private void ConstantSpawn(int index)
    {
        //spawnedItem = Instantiate(items[index], new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
        FindSlot(items[index]);
        Debug.Log(items[index].name + " Slot:" + slotNum);
        Instantiate(items[index], new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
        spawned = true;
    }

    private void FindSlot(GameObject item)
    {
        //check if the item is a trap
        if (item.GetComponent<DeployTrapWeapon>() != null)
        {
            //set the slot number
            slotNum = item.GetComponent<DeployTrapWeapon>().slot;
        }

        //check if the item is a boombox 
        if (item.GetComponent<BoomBox>() != null)
        {
            slotNum = item.GetComponent<BoomBox>().slot;
        }

        //check if the item is a cannon 
        if (item.GetComponent<Cannon>() != null)
        {
            slotNum = item.GetComponent<Cannon>().slot;
        }

        //check if the item is a Laser gun 
        if (item.GetComponent<LaserGun>() != null)
        {
            slotNum = item.GetComponent<LaserGun>().slot;
        }
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
            //if the spawner has an
            if(canspawn)
            {
                canspawn = false;
                StartCoroutine(NewSpawnDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<InventoryManager>().inventory[slotNum] == null)
        {
            canspawn = true;
        }
    }
}
