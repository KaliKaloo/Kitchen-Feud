using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParseScore
{
    private static int score1 = 0;
    private static int score2 = 0;

    public void UpdateScores(int newScore1, int newScore2)
    {
        score1 = newScore1;
        score2 = newScore2;
    }

    public void AddScore1(int newScore1)
    {
        score1 += newScore1;
    }

    public void AddScore2(int newScore2)
    {
        score2 += newScore2;
    }

    public int GetScore1()
    {
        return score1;
    }

    public int GetScore2()
    {
        return score2;
    }
}

public class scoreController : MonoBehaviour
{
    [SerializeField] private Text score1Text;
    [SerializeField] private Text score2Text;

    [SerializeField] private Text timerText;

    // How long the game lasts in seconds
    private int timer = 100;

    private int score1 = 0;
    private int score2 = 0;
    float elapsed = 0f;
    
    // updates end scores to compare in game over scene
    private static ParseScore endScores = new ParseScore();

    // Start is called before the first frame update
    void Start()
    {
        // start scores at 0
        score1Text.text = ConvertScoreToString(score1);
        score2Text.text = ConvertScoreToString(score2);
        timerText.text = ConvertSecondToMinutes(timer);
    }

    // Converts an integer to a string with proper comma notation
    private string ConvertScoreToString(int score)
    {
        return String.Format("{0:n0}", score);
    }

    private string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    // Update is called once per frame
    void Update()
    {
        // update scores every frame
        score1 = endScores.GetScore1();
        score2 = endScores.GetScore2();

        score1Text.text = ConvertScoreToString(score1);
        score2Text.text = ConvertScoreToString(score2);

        // increment every second
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            OutputTime();
        }
    }

    // OutputTime is called once per second
    void OutputTime()
    {
        if (timer != 0)
        {
            // updates timer and text in timer
            timer = timer - 1;
            timerText.text = ConvertSecondToMinutes(timer);
        }
        else
        {
            // load game over screen and send final scores
            endScores.UpdateScores(score1, score2);
            SceneManager.LoadScene("gameOver");
        }
    }
}
