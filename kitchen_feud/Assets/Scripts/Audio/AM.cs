using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;


public class AM : MonoBehaviour
{
    IRtcEngine engine;
    string randomInstance;
    int myTeam;
    public PhotonView PV;
    public bool played;
    public GameObject otherP;
    public string Speaker;
    public int team;

    public PlayerController player; 

    public int location;
    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
    }
    void Start()
    {   
        PV = GetComponent<PhotonView>();
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
            if (pFV.IsMine)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Path");
                player.location = 3;
                AudioManager.instance.changeBGM(3);


                if (team == 1)
                {
                    myTeam = myPlayerC.myTeam;
                    if (myPlayerC.entered1 == true)
                    {
                        pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 1);
                        if (myTeam == 2)
                        {
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                        }
                    }
                }
                else
                {
                    myTeam = myPlayerC.myTeam;
                    if (myPlayerC.entered2 == true)
                    {
                        pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 2);
                       
                        if (myTeam == 1)
                        {
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                        }
                    }
                }
            }
        }

    }
}
