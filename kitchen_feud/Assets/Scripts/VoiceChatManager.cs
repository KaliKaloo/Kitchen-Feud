using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System;


using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using TMPro;
using Hashtable  =  ExitGames.Client.Photon.Hashtable;
using Random = System.Random;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{

    float time;
    private DateTime dt1;
    private DateTime dt2;
    public double internetSpeed;
    private string appID = "906fd9f2074e4b0491fcde55c280b9e5";
    Random rnd = new Random();
    IRtcEngine rtcEngine;
    public int x;
    private bool setInternetSpeed;
    EnableSmoke enableSmoke = new EnableSmoke();


    public static VoiceChatManager Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }



    }

    private void Start()
    {
        setInternetSpeed = false;
        rtcEngine = IRtcEngine.GetEngine(appID);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;

        rtcEngine.OnLeaveChannel += OnleaveChannel;
        rtcEngine.OnError += OnError;

        StartCoroutine(CheckInternetSpeed());



    }




    void OnError(int error, string msg)
    {
        Debug.Log("Error with Agora: " + error + "This is the message: " + msg);
    }

    private void OnleaveChannel(RtcStats stats)
    {
        //Debug.Log("Left channel with duration" + stats.duration);
    }

    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        // if player enters enemy kitchen 
        if (((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1 && channelName.Substring(channelName.Length - 5) == "Team2") 
        || ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2 && channelName.Substring(channelName.Length - 5) == "Team1")) {
            CustomProperties.PlayerMischievous.AddMischievousStat();
            //enableSmoke.ChangePlayerState(true);
           // globalClicked.enterEnemyKitchen =true;

        // if player joins another area    
        }
        else
        {
            //enableSmoke.ChangePlayerState(false);
        }

        Debug.Log("Joined " + channelName.Substring(2));
    }






    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;

    }

    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }

    private void OnDestroy()
    {
        IRtcEngine.Destroy();
    }


    IEnumerator CheckInternetSpeed()
    {


        Stopwatch s = new Stopwatch();

        UnityWebRequest www = new UnityWebRequest("http://localhost:3000/");
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SendWebRequest();
        s.Start();
        dt1 = DateTime.Now;



        while (true)
        {

            if (www.isDone)
            {
                byte[] results = www.downloadHandler.data;
                internetSpeed = Math.Round(results.Length * 0.008f / s.Elapsed.TotalSeconds, 2);
                /*Debug.Log("SIZE " + results.Length);
                Debug.Log("STOPTIME" + s.Elapsed.TotalSeconds);
                Debug.Log("SPEED " + internetSpeed);*/
                //Debug.Log(internetSpeed);

                yield break;
            }

            if (www.result != UnityWebRequest.Result.Success && www.result != UnityWebRequest.Result.InProgress)
            {
                Debug.Log(www.error);
            }



            yield return null;


        }




    }
}
