using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{

    public TextMeshProUGUI winner;
    public void GameOver(bool winning_player)
    {
        gameObject.SetActive(true);
        if (winning_player)
        {
            winner.text = "Player 1 Wins";
        }
        else
        {
            winner.text = "Player 2 Wins";
        }
    }
}
