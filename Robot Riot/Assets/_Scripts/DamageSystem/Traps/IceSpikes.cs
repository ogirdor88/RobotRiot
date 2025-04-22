using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikes : MonoBehaviour
{
    private GameObject player;
    private float playerOriSpeed;
    // Start is called before the first frame update
    public GameObject VFX;
    public float slowAmount = 1;
    bool triggered;
    public int count = 0;
    [SerializeField]
    private AudioSource iceSound;
    private AudioSource newAudio;


    [SerializeField] public CinemachineImpulseSource impulseScource;


    [SerializeField] private int coolDown;
    private bool canDeploy = false;

    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }

    void Start()
    {
        /*player = GameObject.FindWithTag("Player");
        playerOriSpeed = player.GetComponent<PlayerController>()._playerSpeed;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            if (!triggered)
            {

                Debug.Log("Icetriggered");
                //other.GetComponent<PlayerController>()._playerSpeed = slowAmount;
                impulseScource.GenerateImpulse(impulseScource.m_DefaultVelocity * 3);
                VFX.SetActive(true);
                newAudio = Instantiate(iceSound, other.gameObject.transform);
                newAudio.gameObject.transform.parent = other.gameObject.transform;
                newAudio.Play();
                StartCoroutine(IceBreaker());

            }
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (canDeploy)
        {
            Debug.Log("hitplayer");
            other.GetComponent<PlayerController>()._playerSpeed = slowAmount;
            VFX.SetActive(true);
        }
            

    }
    */

    private void OnTriggerExit(Collider other)
    {
        count++;
    }

    private IEnumerator IceBreaker()
    {
        yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
        Destroy(newAudio.gameObject);
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }
/*
    private void Update()
    {
        if(count >= 2)
        {
            StartCoroutine(IceBreaker());
        }
    }*/
}
