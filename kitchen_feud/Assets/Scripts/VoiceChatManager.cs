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


        // internetSpeed = CheckInternetSpeed();

    }


    private void Update()
    {

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
        if (((int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1 &&
             channelName.Substring(channelName.Length - 5) == "Team2")
            || ((int) PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 2 &&
                channelName.Substring(channelName.Length - 5) == "Team1"))
        {
            enableSmoke.ChangePlayerState(true);
            // if player joins another area
        }
        else
        {
            enableSmoke.ChangePlayerState(false);
        }

        Debug.Log("Joined " + channelName.Substring(2));
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();


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

/*    public double CheckInternetSpeed()
    {
        // Create Object Of WebClient
        WebClient wc = new WebClient();

        //DateTime Variable To Store Download Start Time.
        dt1 = DateTime.Now;

        //Number Of Bytes Downloaded Are Stored In ‘data’
        byte[] data = wc.DownloadData("https://www.google.co.uk");

        //DateTime Variable To Store Download End Time.
        dt2 = DateTime.Now;
        //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
        return Math.Round((data.Length * 0.008f) / (dt2 - dt1).TotalSeconds, 2);            
    }*/

    /*public IEnumerator CheckInternetSpeed()
    {
        const string _echoServer = "https://www.google.com/pagesample/";
        
        UnityWebRequest wwww = new UnityWebRequest("www.photonengine.com");
        wwww.downloadHandler = new DownloadHandlerBuffer();

        wwww.SendWebRequest();
        dt1 = DateTime.Now;
        
        while (!wwww.isDone)
        {
            Debug.Log("NO");
        }
        if(wwww.isDone)
        {
            dt2 = DateTime.Now;
            //Debug.LogError(wwww.downloadedBytes);


            Debug.Log("YES");
        }
        Debug.LogError(Math.Round(wwww.downloadedBytes * 0.008f /(dt2 - dt1).TotalSeconds,2));

        yield return null;
        


        //ConnectionManager.Connected = !_request.isNetworkError && !_request.isHttpError && _request.responseCode == 200;
    }*/
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
                Debug.Log("SIZE " + results.Length);
                Debug.Log("STOPTIME" + s.Elapsed.TotalSeconds);
                Debug.Log("SPEED " + internetSpeed);
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
