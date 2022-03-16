using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AudioManagerTwo : MonoBehaviour
{
   // public bool entered2;
    IRtcEngine engine;
    
    int myTeam;
    string randomInstance;
    public AudioSource ding;
    public PhotonView PV;
    public static AudioManagerTwo Instance;
    public int myC = 1;
    public bool played = false;

    private void Awake()
    {
        Instance = this;
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        ding = GameObject.FindGameObjectWithTag("Speaker2").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
       

        if(myTeam == 2)
        {
            engine.LeaveChannel();
            engine.JoinChannel(randomInstance + "Team2");
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    
    }
   
    private void OnTriggerEnter(Collider other)
    {

        myTeam = other.GetComponent<PlayerController>().myTeam;
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerVoiceManager myPlayerC = other.GetComponent<PlayerVoiceManager>();
        if(pFV.IsMine)
        {
        
            if (myPlayerC.entered2 == false)
            {
                pFV.RPC("setEntered", RpcTarget.All, pFV.ViewID, 2);
            }
           

           
                engine.LeaveChannel();

                engine.JoinChannel(randomInstance + "Team2");

                if (myTeam == 1)
                {

                    if (myPlayerC.played == false)
                    {
                        PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                        pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 1);
                    }

                    pFV.RPC("setKickable", RpcTarget.All,pFV.ViewID);
                }
                if (myTeam == 2 && other.GetComponent<PlayerVoiceManager>().healthbar1)
                {
                    pFV.RPC("destHB", RpcTarget.All, pFV.ViewID);

                }





     

        }
    }
    
    [PunRPC]
    void playDing(int viewID)
    {
       
            PhotonView.Find(viewID).GetComponent<AudioManagerTwo>().ding.Play();
        
    }
  
   
   
}
