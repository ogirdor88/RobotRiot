using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test to spawn player, made to test split screen.
public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private bool spawnPlayer1;
    [SerializeField] private bool isPlayerSingleScreen;
    [SerializeField] private GameObject playerPrefab;
    private void Awake()
    {
        GameObject spawnedPlayer = Instantiate(playerPrefab, transform);
        //spawnedPlayer.GetComponent<SplitScreenCamera>().isPlayer1 = spawnPlayer1;
        //spawnedPlayer.GetComponent<SplitScreenCamera>().isPlayer1 = isPlayerSplitScreen;
        spawnedPlayer.GetComponent<SplitScreenCamera>().SetPlayer(spawnPlayer1, isPlayerSingleScreen);
    }
}
