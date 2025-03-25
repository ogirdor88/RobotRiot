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


    [SerializeField] private int coolDown;
    private bool canDeploy = false;

    private void Awake()
    {
        StartCoroutine(WaitForCooldown());
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerOriSpeed = player.GetComponent<PlayerController>()._playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canDeploy)
        {
            if (!triggered)
            {

                Debug.Log("hitplayer");
                other.GetComponent<PlayerController>()._playerSpeed = slowAmount;
                VFX.SetActive(true);

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
        StartCoroutine(SlowDelay(other));
    }

    private IEnumerator SlowDelay(Collider other)
    {
        yield return new WaitForSeconds(3);
        other.GetComponent<PlayerController>()._playerSpeed = playerOriSpeed;
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        canDeploy = true;
    }
}
