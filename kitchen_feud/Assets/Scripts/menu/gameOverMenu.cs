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

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

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


        DisplayStats();
        // Reset all player stats after stats are displayed
        CustomProperties.PlayerResetStats.ResetAll();

    }

    private void DisplayStats()
    {
        // TODO
    }

    // compare scores and update who wins based on scores
    private void CompareScore()
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
        Destroy(GameObject.Find("VoiceManager"));
        Destroy(GameObject.Find("RoomController"));
        PhotonNetwork.LeaveRoom();


        customProperties["Team"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        // leave photon room on the network
        endScores.ResetScores();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
