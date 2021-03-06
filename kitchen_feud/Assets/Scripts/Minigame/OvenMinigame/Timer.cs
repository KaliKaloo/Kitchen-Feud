using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Timer : MonoBehaviour
{

    private static int time = 40;
    public TextMeshProUGUI timerText;
    public float timer = time;
    private float timerFake = time;
    public int score = 0;
    float elapsed = 0f;
    public exitOven backbutton;
    public GameObject sabotageButton;
    string applianceName;
    private bool parentAssigned;
    public GameObject Team1Image;
    public GameObject Team2Image;
    PhotonRoom room;

    
    void Update()
    {

        if (!parentAssigned && transform.parent) {

            InitialiseCanvas();
            InitializeTimer();
            timerText.text = ConvertSecondToMinutes(GetTime());
        }

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

    public int GetTotalTime()
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


    // set the timer amount here 
    public void InitializeTimer()
    {
        timer = time;
        timerFake = time;
        
    }

    // get current time from timer
    public float GetTime()
    {
       return timerFake;
    }


    public void SetTime(float time)
    {
       timerFake = time;
    }


    public void Decrement()
    {
        if (timer > 0) 
        {
            score += 2;
        }
        else
        {
            score -= 2;
        }
        timer -= 1;
        timerFake -=1;
    }
  

    void OutputTime()
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
    private void OnDisable()
    {
        timerText.text = "00:40";
        timerText.color = Color.black;
        timer = 40;
        score = 0;
    }


    public void addSeconds(){
        GetComponent<PhotonView>().RPC("addSecondsRPC", RpcTarget.All, GetComponent<PhotonView>().ViewID);
   }
    public void InitialiseCanvas()
    {
        applianceName = transform.parent.name;

        if (applianceName == "Oven1" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2)
        {
            sabotageButton.SetActive(true);

        }
        else if (applianceName == "Oven2" && (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
        {
            sabotageButton.SetActive(true);
            GameObject.FindGameObjectWithTag("Team1Oven");
        }

        if (applianceName == "Oven1")
        {
            Team1Image.SetActive(true);
        }
        else if (applianceName == "Oven2")
        {
            Team2Image.SetActive(true);
        }
        parentAssigned = true;
    }

    [PunRPC]
    void addSecondsRPC(int viewID){
        PhotonView.Find(viewID).GetComponent<Timer>().timerFake += 10f;
    }
}
