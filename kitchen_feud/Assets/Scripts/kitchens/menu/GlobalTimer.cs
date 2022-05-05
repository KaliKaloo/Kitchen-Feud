using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class GlobalTimer:MonoBehaviour
{

    private Coroutine timerCoroutine;

    // SET TIMER HERE !!!!!!
    private static int time = 5;
    public int totalTime;
    private readonly int startTime = time;

    private static int timer = time;
    private ExitGames.Client.Photon.Hashtable hashTimer = new ExitGames.Client.Photon.Hashtable();
    private void Start()
    {
       
    }

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

    // sets the server's timer
    public void SetServerTime()
    {
        hashTimer["Time"] = timer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashTimer);
    }

    public void ChangeTimerValue(int newTime)
    {
        timer = time = newTime >= 0 ? newTime : 0;
    }

    public int GetTotalTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return time;
        }
        else
        {
            return totalTime;
        }
    }

    // gets the time from the server
    public int GetTime()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties["Time"] != null)
            return (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];

        return 0;
    }

    public int GetLocalTime()
    {
        //return (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        if (PhotonNetwork.IsMasterClient)
        {
            return timer;
        }
        else
        {
            return (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"];
        }
    }

    public void SetLocalTime()
    {
        timer = (int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] - 1;        
    }

    // resets the timer to original starting value
    // call every time the game ends
    public void ResetTimer()
    {
        timer = time = startTime;
    }

    // Converts an int into a suitable string for timer
    public string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    // returns the current time in string format to be displayed on UI
    public string GetCurrentTimeString()
    {
        return ConvertSecondToMinutes(time);
    }

    // set the timer amount here by getting it from the server
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
            timer = GetLocalTime();
        }
    }

    // decrement the timer locally
    // ENSURE all local timer values are the same when game starts
    public void StartTimer(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(DecrementTimer(monoBehaviour));
    }

    private IEnumerator DecrementTimer(MonoBehaviour monoBehaviour)
    {
        yield return new WaitForSeconds(1);

        timer -= 1;

        if (timer <= 0)
        {
            timerCoroutine = null;
        } 
        else
        {
            timerCoroutine = monoBehaviour.StartCoroutine(DecrementTimer(monoBehaviour));
            if (PhotonNetwork.IsMasterClient)
            {
                SetServerTime();
            }
        }
    }
    


    // DEPRECATED: decrement timer
    public void Decrement()
    {
        timer -= 1;
        hashTimer["Time"] = timer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashTimer);
    }
}
