using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameOverMenu : MonoBehaviourPunCallbacks
{
    // SCORES GO HERE
    private int team1Score = 0;
    private int team2Score = 0;

    [SerializeField] private Text team1UIScore;
    [SerializeField] private Text team2UIScore;

    [SerializeField] private Text winnerText;

    // receives scores from score screen
    private static ParseScore endScores = new ParseScore();

    // Start is called before the first frame update
    void Start()
    {
        //get scores from score screen
        team1Score = endScores.GetScore1();
        team2Score = endScores.GetScore2();

        // update scores received onto UI
        team1UIScore.text = String.Format("{0:n0}", team1Score);
        team2UIScore.text = String.Format("{0:n0}", team2Score);

        CompareScore();
    }

    // compare scores and update who wins based on scores
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

    // alters global variable and returns straight to lobby menu
    public void PlayAgain()
    {
        PhotonNetwork.Destroy(GameObject.Find("VoiceManager"));
        PhotonNetwork.Destroy(GameObject.Find("RoomController"));

        // leave photon room on the network
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
