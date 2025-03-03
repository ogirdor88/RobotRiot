using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikes : MonoBehaviour
{
    private GameObject player;
    private float playerOriSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerOriSpeed = player.GetComponent<PlayerMovement>().moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerMovement>().moveSpeed = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(SlowDelay());
    }

    private IEnumerator SlowDelay()
    {
        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerMovement>().moveSpeed = playerOriSpeed;
    }
}
