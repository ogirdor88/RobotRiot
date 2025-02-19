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

    private void Update()
    {
       if (!spawned)
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

        //for testing press t to destroy item and have the delay start
       /* if(spawned)
        {
            if (Input.GetKeyDown("y"))
            {
                Destroy(GameObject.Find("ElectricTrap(Clone)"));
                Destroy(GameObject.Find("LaserGun(Clone)"));
                Destroy(GameObject.Find("BalloonYellow(Clone)"));
                Destroy(GameObject.Find("BalloonRed(Clone)"));
                Destroy(GameObject.Find("BalloonGreen(Clone)"));
                Destroy(GameObject.Find("BalloonBlue(Clone)"));

                StartCoroutine(NewSpawnDelay());
            }
        }*/
    }

    private void RandomSpawn()
    {
        //get a random number
        int rand = Random.Range(0, items.Count);

        //spawn the random item
        Instantiate(items[rand], new Vector3(transform.position.x,transform.position.y + 2, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
       spawned = true;
    }

    private void ConstantSpawn(int index)
    {
        Instantiate(items[index], new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        //set the spawned bool to true
        spawned = true;
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
            Destroy(GameObject.Find("ElectricTrap(Clone)"));
            Destroy(GameObject.Find("LaserGun(Clone)"));
            Destroy(GameObject.Find("BalloonYellow(Clone)"));
            Destroy(GameObject.Find("BalloonRed(Clone)"));
            Destroy(GameObject.Find("BalloonGreen(Clone)"));
            Destroy(GameObject.Find("BalloonBlue(Clone)"));

            StartCoroutine(NewSpawnDelay());
        }
    }
}
