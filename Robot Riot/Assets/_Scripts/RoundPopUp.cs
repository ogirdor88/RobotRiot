using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundPopUp : MonoBehaviour
{
	public GameObject player1, player2;
	[SerializeField]
	private Image popUpImage1, popUpImage2;
	[SerializeField]
	private TextMeshProUGUI popUpText1, popUpText2;
	[SerializeField]
	private int health1, health2;

	//[SerializeField]
	//public GameObject spawner1, spawner2;

	GameObject[] players;

	private void Start()
	{
		// initializes health for this script
		health1 = -1;
		health2 = -1;	
	}

	void Update()
	{
		// searches for two players, adds both to array
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");

		// once 2 players are detected, adds to final array
		if (temp.Length == 2)
		{
			players = temp;

			// populates player1 and player2 with players in scene
			foreach (GameObject player in players)
			{
				player1 = players[0];
				health1 = player1.gameObject.GetComponent<Health>()._currentHealth;
				player2 = players[1];
				health2 = player2.gameObject.GetComponent<Health>()._currentHealth;
			}
		}

		// if both players are active w/ health, run updates to retrieve current health
		if (player1 != null && player2 != null)
		{
			// update health
			health1 = player1.gameObject.GetComponent<Health>()._currentHealth;
			health2 = player2.gameObject.GetComponent<Health>()._currentHealth;

			//if player 1 loses a life, calls player 2 pop up and return player to other side of map
			if(health1 <= 0)
			{
				//Debug.Log("Player 2 wins");
				//player2.transform.position = spawner2.transform.position;
				StartCoroutine(PopUpTimeout2());
			}

			// if player 2 loses a life, calls player 1 pop up and return player to other side of map
			else if(health2 <= 0)
			{
				//Debug.Log("Player 1 wins");
				//player1.transform.position = spawner1.transform.position;
				StartCoroutine(PopUpTimeout1());
			}
		}
	}

	// activates and deactivates for player 1 round win
	IEnumerator PopUpTimeout1()
	{
		popUpImage1.gameObject.SetActive(true);
		popUpText1.gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		popUpImage1.gameObject.SetActive(false);
		popUpText1.gameObject.SetActive(false);
		health1 = 0;
		
	}
	
	// activates and deactivates for player 2 round win
	IEnumerator PopUpTimeout2()
	{
		popUpImage2.gameObject.SetActive(true);
		popUpText2.gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		popUpImage2.gameObject.SetActive(false);
		popUpText2.gameObject.SetActive(false);
		health2 = 0;
	}
}
