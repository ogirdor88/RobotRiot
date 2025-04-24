using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    //public GameObject spawn3;
    //public GameObject spawn4;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        AssignSpawnPositions();
        spawn1.SetActive(true);
        spawn2.SetActive(true);
        //spawn3.SetActive(false);
        //spawn4.SetActive(false);
    }

    private void AssignSpawnPositions()
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (PlayerInput player in FindObjectsOfType<PlayerInput>())
        {;
            SpawnPoint spawn = spawnPoints.FirstOrDefault(s => s.playerIndex == player.playerIndex);
            if (spawn != null)
            {
                player.transform.position = spawn.transform.position;
                PlayerManager.Instance.SetSpawnPosition(player.playerIndex, spawn.transform.position);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawn1.SetActive(false);
        spawn2.SetActive(false);
        //spawn3.SetActive(true);
        //spawn4.SetActive(true);
    }

}
