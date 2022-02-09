using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreController : MonoBehaviour
{
    [SerializeField] private Text score1Text;
    [SerializeField] private Text score2Text;

    [SerializeField] private Text timerText;

    private int timer = 300;

    // Start is called before the first frame update
    void Start()
    {
        // start scores at 0
        score1Text.text = ConvertScoreToString(0);
        score2Text.text = ConvertScoreToString(0);
    }

    // Converts an integer to a string with proper comma notation
    private string ConvertScoreToString(int score)
    {
        return String.Format("{0:n0}", score);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer = timer - 1;
            timerText.text = String.Format("{0}", timer);
        }
        else
        {
            SceneManager.LoadScene("gameOver");
        }
    }
}
