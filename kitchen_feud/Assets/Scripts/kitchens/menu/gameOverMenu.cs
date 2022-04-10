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
    [SerializeField] private GameObject LeavingGameCanvas;

    [SerializeField] private TextMeshProUGUI winnerText;

    [SerializeField] private GameObject genericPlayer1;
    [SerializeField] private GameObject genericPlayer2;
    [SerializeField] private GameObject genericPlayer3;

    [SerializeField] private TextMeshProUGUI stat1;
    [SerializeField] private TextMeshProUGUI stat2;
    [SerializeField] private TextMeshProUGUI stat3;

    private Animator genericPlayer1Animator;
    private Animator genericPlayer2Animator;
    private Animator genericPlayer3Animator;

    private int winningTeam;

    private static ParseScore endScores = new ParseScore();
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("lastLobby", null);
        PlayerPrefs.SetInt("disconnected", 0);
        PlayerPrefs.Save();

        LeavingGameCanvas.SetActive(false);

        genericPlayer1Animator = genericPlayer1.GetComponent<Animator>();
        genericPlayer2Animator = genericPlayer2.GetComponent<Animator>();
        genericPlayer3Animator = genericPlayer3.GetComponent<Animator>();

        ChooseAnimation();
        // Reset all player stats after stats are displayed
        CustomProperties.PlayerResetStats.ResetAll();


    }

    // compare scores and update who wins based on scores
    public void CompareScore(int score1, int score2)
    {
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

        DisplayStats();
        SkinPlayers();
    }


    // Sets the custom stats to be displayed properly on the screen
    private void DisplayStats()
    {
        stat1.text = CustomProperties.PlayerPoints.GetHighestPlayerPoints(winningTeam);
        stat2.text = CustomProperties.PlayerCookedDishes.GetHighestCookedDishes();
        stat3.text = CustomProperties.PlayerMischievous.GetHighestMischievousStat();
    }

    // Skin the models in the scene to appropriate winning team
    private void SkinPlayers()
    {
        Material newMat = Resources.Load("cat_red", typeof(Material)) as Material;

        if (winningTeam == 1)
        {
            newMat = Resources.Load("cat_red", typeof(Material)) as Material;
        }
        else if (winningTeam == 2)
        {
            newMat = Resources.Load("cat_blue", typeof(Material)) as Material;
        }

        // if not draw colour models correctly
        if (winningTeam > 0)
        {
            genericPlayer1.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
            genericPlayer2.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
            genericPlayer3.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
        }
        // if draw then split model colours up
        else
        {
            Material newMat2 = Resources.Load("cat_blue", typeof(Material)) as Material;
            genericPlayer1.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
            genericPlayer2.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat2;
            genericPlayer3.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat2;
        }

    }

    // Choose random animation depending on the outcome of the game
    // i.e. winning team chooses 3 dance animations, draw chooses 3 sad animations
    private void ChooseAnimation()
    {
        List<int> animationList = new List<int>();
        int index;
        int current;

        // if there is a winning team pick from dance animations
        if (winningTeam != 0)
            animationList = new List<int>() { 1, 2, 3 };
        // else pick from sad animations
        else
            animationList = new List<int>() { 4, 5, 6 };

        // choose a random animation from list
        System.Random random = new System.Random();
        index = random.Next(animationList.Count);
        current = animationList[index];
        // remove animation so can't be picked again
        animationList.RemoveAt(index);

        // set 1st model animation
        genericPlayer1Animator.SetInteger("Dance", current);

        // get next model animation
        index = random.Next(animationList.Count);
        current = animationList[index];
        animationList.RemoveAt(index);

        genericPlayer2Animator.SetInteger("Dance", current);

        // set last model's animation to remaining in list
        genericPlayer3Animator.SetInteger("Dance", animationList[0]);


    }


    // alters global variable and returns straight to lobby menu
    public void PlayAgain()
    {
        // start loading screen for leave game
        LeavingGameCanvas.SetActive(true);
        Destroy(GameObject.Find("VoiceManager"));
        Destroy(GameObject.Find("RoomController"));

        // sometimes local player is not destroyed on scene load so try destroying if available
        try
        {
            Destroy(GameObject.Find("Local"));
        } catch
        {
            // local player is not found
        }

        customProperties["Team"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        // leave photon room on the network
        PhotonNetwork.LeaveRoom();

        endScores.ResetScores();
    }

    // Load main menu once player has properly left the room
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
