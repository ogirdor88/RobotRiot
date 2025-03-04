using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private void Start()
    {
        AssignSpawnPositions();
    }

    private void AssignSpawnPositions()
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

        foreach (PlayerInput player in FindObjectsOfType<PlayerInput>())
        {
            SpawnPoint spawn = spawnPoints.FirstOrDefault(s => s.playerIndex == player.playerIndex);
            if (spawn != null)
            {
                player.transform.position = spawn.transform.position;
                PlayerManager.Instance.SetSpawnPosition(player.playerIndex, spawn.transform.position);
            }
        }
    }

}
