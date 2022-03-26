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
    public PlayerController player; 



    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        ding = GameObject.FindGameObjectWithTag(Speaker).GetComponent<AudioSource>();
        MusicManager.instance.playerTeam = myTeam;

    }
    
    void Start()
    {
        PV = GetComponent<PhotonView>();

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
         if (!player){
            player = GameObject.Find("Local").GetComponent<PlayerController>();
		}
        if (other.tag == "Player")
        {
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerVoiceManager myPlayerC = other.GetComponent<PlayerVoiceManager>();
            myTeam = myPlayerC.myTeam;
            if (pFV.IsMine)
            {
                MusicManager.instance.changeBGM(team, 10, 0.8f, 0.2f);
                Debug.Log(team);
                
                if (team == 1)
                {
                    pFV.RPC("setEntered", RpcTarget.All, pFV.ViewID, 1);
                    
                }
                else
                {
                    pFV.RPC("setEntered", RpcTarget.All, pFV.ViewID, 2);

                }
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team" + team);
                player.location = team;


                if (myTeam != team)
                {
                    {
                        if (myPlayerC.played == false)
                        {
                            PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 1);
                        }
                        pFV.RPC("setKickable", RpcTarget.All, pFV.ViewID);
                    }
                }
                if (myTeam == team && myPlayerC.healthbar1)
                {
                    pFV.RPC("destHB", RpcTarget.All, pFV.ViewID);
                    pFV.RPC("setKickableF", RpcTarget.All, pFV.ViewID);
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
