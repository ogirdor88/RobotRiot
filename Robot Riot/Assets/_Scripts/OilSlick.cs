using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
