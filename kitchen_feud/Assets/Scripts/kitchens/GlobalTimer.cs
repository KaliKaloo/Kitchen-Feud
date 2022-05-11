using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// Timer for server is established here, to be used globally for all scripts where necessary
public class GlobalTimer
{

    private Coroutine timerCoroutine;

    // default time set here
    public static int time = 300;
    public static int totalTime;
    private readonly int startTime = time;

    private static int timer = time;
    private ExitGames.Client.Photon.Hashtable hashTimer = new ExitGames.Client.Photon.Hashtable();
    Hashtable total = new Hashtable();
    public PhotonView PV;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // set the servers time to whatever the master client has stored
            total["TotalTime"] = time;
            PhotonNetwork.CurrentRoom.SetCustomProperties(total);
        }
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
            total["TotalTime"] = intermediateTime;
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(total);
            }
            return 0;
        }
    }

    public void SetServerTime()
    {
        hashTimer["Time"] = timer;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashTimer);
    }

    // timer should never go below 0
    public void ChangeTimerValue(int newTime)
    {
        timer = time = newTime >= 0 ? newTime : 0;
    }

    public void setTotalTime(int num)
    {
        totalTime = num;
    }

    public int GetTotalTime()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // use time stored locally
                return time;
            }
            else
            {
                // get time from server
                if (PhotonNetwork.CurrentRoom.CustomProperties["TotalTime"] != null)
                {
                    return (int)PhotonNetwork.CurrentRoom.CustomProperties["TotalTime"];
                }
                else return 0;

            }
        }
        else
        {
            // use local time
            return time;
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
            return timer;
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

    // see above function
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
}
