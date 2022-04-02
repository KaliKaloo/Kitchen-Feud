using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GlobalTimer
{

    // SET TIMER HERE !!!!!!
    private static int time = 5;

    private static int timer = time;
    private ExitGames.Client.Photon.Hashtable hashTimer = new ExitGames.Client.Photon.Hashtable();


    // changes original starting time, only do before game starts!
    public int AddSubtractTimerValue(int newTime)
    {
        int intermediateTime = time + newTime;
        if (intermediateTime < 60)
        {
            return 1;
        }
        else if (intermediateTime > 1200)
        {
            return 2;
        }
        else
        {
            timer = time = intermediateTime;
            return 0;
        }
    }

    public void ChangeTimerValue(int newTime)
    {
        timer = time = newTime >= 0 ? newTime : 0;
    }

    public int GetTotalTime()
    {
        return time;
    }

    public int GetCurrentTime()
    {
        return timer;
    }

    public string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    public string GetCurrentTimeString()
    {
        return ConvertSecondToMinutes(time);
    }

    // set the timer amount here 
    public void InitializeTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            // how long the timer will last in seconds
            timer = time;
            hashTimer["Time"] = timer;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTimer);

        }
        else
        {
            // temporary fix
            timer = TryTime();
        }
    }

    // avoiding trying to access hashmap without master client loading
    private int TryTime()
    {
        int currentTime;
        try
        {
            currentTime = (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        }
        catch
        {
            currentTime = timer;
        }
        return currentTime;
    }

    // get current time from timer
    public int GetTime()
    {
        return TryTime();
    }

    // decrement timer
    public void Decrement()
    {
        timer -= 1;
        hashTimer["Time"] = timer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashTimer);
    }
}
