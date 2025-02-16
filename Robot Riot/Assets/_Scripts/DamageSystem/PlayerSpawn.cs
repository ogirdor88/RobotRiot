using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private bool spawnPlayer1;
    [SerializeField] private bool isPlayerSingleScreen;
    [SerializeField] private bool screenBorder = true;
    public GameObject player;

    private void Start()
    {
        Instantiate(player, transform.position, transform.rotation);
        player.GetComponent<SplitScreenCamera>().SetPlayer(spawnPlayer1, isPlayerSingleScreen, screenBorder);
        Destroy(this.gameObject);
    }
}
