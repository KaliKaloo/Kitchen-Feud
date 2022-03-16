using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AMTwo : MonoBehaviour
{
    // public bool entered2;
    IRtcEngine engine;

    int myTeam;
    string randomInstance;
    public AudioSource ding;
    public PhotonView PV;
    public int myC = 1;
    public bool played = false;

    private void Awake()
    {
        
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        ding = GameObject.FindGameObjectWithTag("Speaker2").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();


        if (myTeam == 2)
        {
            engine.LeaveChannel();
            engine.JoinChannel(randomInstance + "Team2");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerController myPlayerC = other.GetComponent<PlayerController>();
            myTeam = myPlayerC.myTeam;
             if(myPlayerC.entered2 == true)
            {
                pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 2);
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Path");
                if (myTeam == 1)
                {
                    pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                }
            }
            
                
                
          
        }

    }
}
