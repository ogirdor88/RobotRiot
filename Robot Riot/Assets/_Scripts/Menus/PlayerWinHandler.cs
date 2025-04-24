using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWinHandler : MonoBehaviour
{

    public TMP_Sprite[] WinSprites;

    public GameObject[] winUI;  // UI for winners
    public GameObject[] loseUI; // UI for losers

    public void WinningPlayer(int playerIndex)
    {
        Debug.Log(playerIndex + "Player WINNN");
        winUI[playerIndex].SetActive(true);
    }

}
