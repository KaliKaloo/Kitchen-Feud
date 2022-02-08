using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class gameOverMenu : MonoBehaviour
{
    // SCORES GO HERE
    private readonly int team1Score = 10000;
    private readonly int team2Score = 20000;

    [SerializeField] private Text team1UIScore;
    [SerializeField] private Text team2UIScore;

    [SerializeField] private Text winnerText;

    // Start is called before the first frame update
    void Start()
    {
        // update scores received onto UI
        team1UIScore.text = String.Format("{0:n0}", team1Score);
        team2UIScore.text = String.Format("{0:n0}", team2Score);

        CompareScore();
    }

    public void CompareScore()
    {
        if (team2Score < team1Score)
        {
            winnerText.text = "Team 1 wins!";
        }
        else if (team1Score < team2Score)
        {
            winnerText.text = "Team 2 wins!";
        }
        else
        {
            winnerText.text = "It's a draw!";
        }
    }

    public void PlayAgain()
    {
        print("work");
        PhotonNetwork.LoadLevel("mainMenu");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
