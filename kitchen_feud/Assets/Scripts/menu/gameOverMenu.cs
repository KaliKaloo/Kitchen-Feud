using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class gameOverMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text team1UIScore;
    [SerializeField] private Text team2UIScore;

    [SerializeField] private TextMeshProUGUI winnerText;

    [SerializeField] private GameObject genericPlayer1;
    [SerializeField] private GameObject genericPlayer2;
    [SerializeField] private GameObject genericPlayer3;

    private Animator genericPlayer1Animator;
    private Animator genericPlayer2Animator;
    private Animator genericPlayer3Animator;

    private static ParseScore endScores = new ParseScore();
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        genericPlayer1Animator = genericPlayer1.GetComponent<Animator>();
        genericPlayer2Animator = genericPlayer2.GetComponent<Animator>();
        genericPlayer3Animator = genericPlayer3.GetComponent<Animator>();

        // update scores received onto UI
        //team1UIScore.text = String.Format("{0:n0}", team1Score);
        //team2UIScore.text = String.Format("{0:n0}", team2Score);

        ChooseAnimation();
        // Reset all player stats after stats are displayed
        CustomProperties.PlayerResetStats.ResetAll();

    }

    private void DisplayStats()
    {
        // TODO
    }

    // compare scores and update who wins based on scores
    public void CompareScore(int score1, int score2)
    {
        int winningTeam = 0;
        if (score2 < score1)
        {
            winnerText.text = "Team 1 wins!";
            winningTeam = 1;
        }
        else if (score1 < score2)
        {
            winnerText.text = "Team 2 wins!";
            winningTeam = 2;
        }
        else
        {
            winnerText.text = "It's a draw!";
        }

        SkinPlayers(winningTeam);
    }

    private void SkinPlayers(int team)
    {
        Material newMat;
        if (team == 1)
        {
            newMat = Resources.Load("cat_red", typeof(Material)) as Material;
        }
        else
        {
            newMat = Resources.Load("cat_blue", typeof(Material)) as Material;
        }

        genericPlayer1.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
        genericPlayer2.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
        genericPlayer3.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;

    }

    private void ChooseAnimation()
    {

        int index;
        int current;

        List<int> AnimationList = new List<int>() { 1, 2, 3 };

        System.Random random = new System.Random();
        index = random.Next(AnimationList.Count);
        current = AnimationList[index];
        AnimationList.RemoveAt(index);

        genericPlayer1Animator.SetInteger("Dance", current);

        index = random.Next(AnimationList.Count);
        current = AnimationList[index];
        AnimationList.RemoveAt(index);

        genericPlayer2Animator.SetInteger("Dance", current);

        genericPlayer3Animator.SetInteger("Dance", AnimationList[0]);


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
        this.gameObject.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
