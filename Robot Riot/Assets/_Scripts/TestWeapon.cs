using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    //Test script
    // Will be changed later when have all weapons and players in the game
    public Health player;
    public Health enemy;

    //this is saying that when you push A player attacks enemy and L enemy attacks player
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.DealDamage(enemy.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            enemy.DealDamage(player.gameObject);
        }
    }
}
