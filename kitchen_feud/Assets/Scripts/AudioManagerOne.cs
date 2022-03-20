using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AudioManagerOne : MonoBehaviour
{
    //public bool entered1;
    IRtcEngine engine;
    int myTeam;
    string randomInstance;
    public AudioSource ding;
    public PhotonView PV;
    public bool played;
    public GameObject otherP;



    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        ding = GameObject.FindGameObjectWithTag("Speaker1").GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
      

        if (myTeam == 1)
        {
            engine.LeaveChannel();
            engine.JoinChannel(randomInstance + "Team1");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        PhotonView pFV = other.GetComponent<PhotonView>();
        PlayerVoiceManager myPlayerC = other.GetComponent<PlayerVoiceManager>();
        myTeam = myPlayerC.myTeam;
        if (pFV.IsMine)
        {
          


            if (myPlayerC.entered1 == false)
            {

                pFV.RPC("setEntered", RpcTarget.All, pFV.ViewID, 1);
            }
     
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team1");
                if (myTeam == 2)
                {
                    if (myPlayerC.played == false)
                    {
                        PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                        pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 1);
                }
                    pFV.RPC("setKickable", RpcTarget.All,pFV.ViewID);
                }
                if (myTeam == 1 && myPlayerC.healthbar1)
                {
                    pFV.RPC("destHB", RpcTarget.All, pFV.ViewID);


                }

  
            

        }
    }
    [PunRPC]
    void playDing(int viewID)
    {

        PhotonView.Find(viewID).GetComponent<AudioManagerOne>().ding.Play();

    }
}
