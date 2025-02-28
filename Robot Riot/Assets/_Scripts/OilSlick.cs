using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float slideForce;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            //Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            //dir = dir.normalized;
            player.GetComponent<PlayerMovement>().enabled = false;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        player.GetComponent<Rigidbody>().AddForce(GameObject.Find("rotationPoint").transform.forward * slideForce, ForceMode.Force);
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
