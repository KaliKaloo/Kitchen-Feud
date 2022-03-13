using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Timer : MonoBehaviour
{

    // SET TIMER HERE !!!!!!
    private static int time = 40;

    public Text timerText;
    public float timer = time;
    private static bool started;
    public int score = 0;
    float elapsed = 0f;
    public exitOven backbutton;
    PhotonRoom room;

    // changes original starting time, only do before game starts!

    void Start()
    {
        // start scores at 0
       
        // start timer if not started yet
        InitializeTimer();
        timerText.text = ConvertSecondToMinutes(GetTime());
    }

    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }
        // increment every second
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            OutputTime();
        }
    }
    public void ChangeTimerValue(int newTime)
    {
        timer = time = newTime;
    }

    public int GetCurrentTime()
    {
        return time;
    }

    public string ConvertSecondToMinutes(float seconds)
    {
        
        string str;
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        if (seconds > -1)
        {
             str = time.ToString(@"mm\:ss");
        }
        else
        {
             str = "-" + time.ToString(@"mm\:ss");
        }
        return str;
    }

    public string GetCurrentTimeString()
    {
        return ConvertSecondToMinutes(time);
    }

    // set the timer amount here 
    public void InitializeTimer()
    {
        timer = time;
        
    }

    public void RestartTimer()
    {
        started = false;
    }



    // get current time from timer
    public float GetTime()
    {
       return timer;
    }


    // decrement timer
    public void Decrement()
    {
        if (GetTime() > 0)
        {
            score += 2;
        }
        else
        {
            score -= 2;
        }
        timer -= 1;
       
     
    }
  

    void OutputTime()
    {

       // if (GetTime() > 0)
        {
            Decrement();
            // updates timer and text in timer
            if (GetTime() < 5)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.black;
            }
            
            timerText.text = ConvertSecondToMinutes(GetTime());
        }
       
    }
    private void OnDisable()
    {
        timerText.text = "00:40";
        timerText.color = Color.black;
        timer = 40;
        score = 0;
    }
}
