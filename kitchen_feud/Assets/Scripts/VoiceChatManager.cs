using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;
using System;
using System.Text.RegularExpressions;
using Random = System.Random;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    private string appID = "906fd9f2074e4b0491fcde55c280b9e5";
    Random rnd = new Random();
    IRtcEngine rtcEngine;
    public int x;


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
       
        Debug.Log("Joined " + channelName.Substring(2));
    }
    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();

       
    }



  /*  public override void OnJoinedRoom()
        {
            Debug.LogError("THIS IS THE LOBBY: " + x);
            rtcEngine.JoinChannel(x.ToString() + "Lobby");
        }
  */
    
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
  
}