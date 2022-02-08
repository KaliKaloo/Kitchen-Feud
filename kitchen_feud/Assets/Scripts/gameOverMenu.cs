using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverMenu : MonoBehaviour
{
    // SCORES GO HERE
    private int team1Score = 0;
    private int team2Score = 0;

    [SerializeField] private Text team1UIScore;
    [SerializeField] private Text team2UIScore;

    [SerializeField] private Text winnerText;

    private static MyTestScriptNoMonoBehaviour gameOver = new MyTestScriptNoMonoBehaviour();

    // Start is called before the first frame update
    void Start()
    {
        //randomize scores for now
        System.Random r = new System.Random();
        int score1 = r.Next(0, 20000);
        int score2 = r.Next(0, 20000);
        team1Score = score1;
        team2Score = score2;

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
        gameOver.End();
        SceneManager.LoadScene("mainMenu");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
