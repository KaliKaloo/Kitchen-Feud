using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Net;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;
using System;
using System.Text.RegularExpressions;
using Hashtable  =  ExitGames.Client.Photon.Hashtable;
using Random = System.Random;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    private Hashtable internet = new Hashtable();

    private DateTime dt1;
    private DateTime dt2;
    public double internetSpeed;
    private string appID = "906fd9f2074e4b0491fcde55c280b9e5";
    Random rnd = new Random();
    IRtcEngine rtcEngine;
    public int x;
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
        rtcEngine = IRtcEngine.GetEngine(appID);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        
        rtcEngine.OnLeaveChannel += OnleaveChannel;
        rtcEngine.OnError  += OnError;
        internetSpeed = CheckInternetSpeed();
        if (internetSpeed > 1000)
        {
            internet["Band"] = "A";

        }else if (internetSpeed < 1000 && internetSpeed > 700)
        {
            internet["Band"] = "B";
        }else if (internetSpeed < 700 && internetSpeed < 400)
        {
            internet["Band"] = "C";
        }else if (internetSpeed < 0 && internetSpeed < 400)
        {
            internet["Band"] = "D";
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(internet);
    }

    private void Update()
    {
     
    }

    void OnError(int error, string msg) {
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
            enableSmoke.ChangePlayerState(true);
        // if player joins another area
        } else {
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
  
    public double CheckInternetSpeed()
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
    }
}
