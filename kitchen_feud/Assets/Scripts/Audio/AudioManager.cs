using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AudioManager : MonoBehaviour
{
    IRtcEngine engine;
    int myTeam;
    string randomInstance;
    public AudioSource ding;
    public PhotonView PV;
    public bool played;
    public GameObject otherP;
    public string Speaker;
    public int team;


    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
    }
    
    
    void Start()
    {
        MusicManager.instance.location = myTeam;
        PV = GetComponent<PhotonView>();
        ding = GameObject.FindGameObjectWithTag(Speaker).GetComponent<AudioSource>();


        if (myTeam == 1)
        {
            engine.LeaveChannel();
            engine.JoinChannel(randomInstance + "Team1");
        }else if(myTeam == 2)
        {
            engine.LeaveChannel();
            engine.JoinChannel(randomInstance + "Team2");
        }
        
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerVoiceManager myPlayerC = other.GetComponent<PlayerVoiceManager>();
            myTeam = myPlayerC.myTeam;
            if (pFV.IsMine)
            {
                MusicManager.instance.changeBGM(team, 10, 0, 1);
                MusicManager.instance.location = team;

                if (team == 1)
                {
                    pFV.RPC("setEntered", RpcTarget.AllBuffered, pFV.ViewID, 1);
                    
                }
                else
                {
                    pFV.RPC("setEntered", RpcTarget.AllBuffered, pFV.ViewID, 2);

                }
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team" + team);

                if (myTeam != team)
                {
                    {
                        if (myPlayerC.played == false)
                        {
                            PV.RPC("playDing", RpcTarget.AllBuffered, PV.ViewID);
                            pFV.RPC("setPlayed", RpcTarget.AllBuffered, pFV.ViewID, 1);
                        }
                        pFV.RPC("setKickable", RpcTarget.AllBuffered, pFV.ViewID);
                    }
                }
         
        
                if (myTeam == team && myPlayerC.healthbar1)
                {
                    pFV.RPC("destHB", RpcTarget.AllBuffered, pFV.ViewID);
                    pFV.RPC("setKickableF", RpcTarget.AllBuffered, pFV.ViewID);
                }
            }
        }

        
    }

    [PunRPC]
    void playDing(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<AudioManager>().ding.Play();
    }
}
