using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilSlick : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float slideForce;

    [SerializeField] private int coolDown;
    private bool canDeploy = false;
    [SerializeField] public CinemachineImpulseSource impulseScource;

    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canDeploy) 
        {
            //Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            //dir = dir.normalized;
            player.GetComponent<PlayerController>().enabled = false;
            impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        player.GetComponent<Rigidbody>().AddForce(GameObject.Find("rotationPoint").transform.forward * slideForce, ForceMode.Force);
    }

    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }

    public void DisplayName()
    {
        Debug.Log("Pete");
    }

    public void TurnOffButton(GameObject b)
    {
       b.GetComponent<Button>().interactable = false;
    }
}
