using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;
using System;

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    private string appID = "906fd9f2074e4b0491fcde55c280b9e5";
    IRtcEngine rtcEngine;


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
        Debug.Log("Left channel with duration" + stats.duration);
    }

    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined channel" + channelName);
    }

    
        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name);
            rtcEngine.JoinChannel("Lobby");



        }
    
    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;

    }

    public override void OnLeftRoom()
    {
        Debug.Log("BYEEEE");
        rtcEngine.LeaveChannel();
    }
    private void OnDestroy()
    {
        IRtcEngine.Destroy();
    }
}
