using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GlobalTimer
{

    // SET TIMER HERE !!!!!!
    private static int time = 10;
    private static int timer = time;
    PhotonRoom room;

    // changes original starting time, only do before game starts!
    public int AddSubtractTimerValue(int newTime)
    {
        int intermediateTime = time + newTime;
        if (intermediateTime < 60)
        {
            return 1;
        } else if (intermediateTime > 1200)
        {
            return 2;
        } else
        {
            timer = time = intermediateTime;
            return 0;
        }
    }

    public void ChangeTimerValue(int newTime)
    {
        timer = time = newTime >= 0 ? newTime : 0;
    }

    public int GetTotalTime(){
        return timer;
    }

    public int GetCurrentTime()
    {
        return time;
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
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable() { { "Time", timer } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);

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

    public bool OrderInterval()
    {
        int currentTime = TryTime();
        int interval = currentTime % 20;
        if (interval == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    // decrement timer
    public void Decrement()
    {
        timer -= 1;
        ExitGames.Client.Photon.Hashtable ht = PhotonNetwork.CurrentRoom.CustomProperties;
        ht.Remove("Time");
        ht.Add("Time", timer);
        PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
    }
}

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
        if ((score1 + newScore1) >= 0)
            score1 += newScore1;
    }

    public void AddScore2(int newScore2)
    {
        if ((score2 + newScore2) >= 0)
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
    public List<GameObject> trays = new List<GameObject>();
    float elapsed = 0f;

    // updates end scores to compare in game over scene
    private static ParseScore scores = new ParseScore();

    // global timer
    private static GlobalTimer timer = new GlobalTimer();

    // Start is called before the first frame update
    void Start()
    {
        // start scores at 0
        score1Text.text = ConvertScoreToString(scores.GetScore1());
        score2Text.text = ConvertScoreToString(scores.GetScore2());

        // start timer if not started yet
        timer.InitializeTimer();
        timerText.text = ConvertSecondToMinutes(timer.GetTime());
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
        score1Text.text = ConvertScoreToString(scores.GetScore1());
        score2Text.text = ConvertScoreToString(scores.GetScore2());

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

        if (timer.GetTime() > 0)
        {
            // updates timer and text in timer
            timer.Decrement();
            timerText.text = ConvertSecondToMinutes(timer.GetTime());
        }
        else
        {
            // load game over screen and send final scores
            for (int i = 0; i < trays.Count; i++)
            {
                Tray ts = trays[i].GetComponent<Tray>();
                ts.tray.trayID = null;
                ts.tray.ServingTray.Clear();
                ts.tray.objectsOnTray.Clear();
            }
            PhotonNetwork.LoadLevel("gameOver");
        }

    }

    
}