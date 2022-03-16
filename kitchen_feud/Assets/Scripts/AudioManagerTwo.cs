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

       /* Debug.Log(myC);
        if (!PV.IsMine)
        {
            if (myTeam == 2 && myC == 0)
            {
                Debug.Log("222");
                engine.LeaveChannel();

                engine.JoinChannel(randomInstance + "Team2");

                PV.RPC("setMyC",RpcTarget.All,1);
            }
            else if (myTeam == 1 && myC == 0)
            {
                Debug.Log("1111");
                engine.LeaveChannel();

                engine.JoinChannel(randomInstance + "Team1");

                PV.RPC("setMyC",RpcTarget.All,1);
            }
        }*/
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PhotonView>().IsMine)
        {
            myTeam = other.GetComponent<PlayerController>().myTeam;
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerController myPlayerC = other.GetComponent<PlayerController>();
            if (myPlayerC.entered2 == false)
            {
                pFV.RPC("setEntered", RpcTarget.All, pFV.ViewID, 2);
            }
            else
            {
                pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 2);
            }

            if (myPlayerC.entered2 == true)
            {
                engine.LeaveChannel();

                engine.JoinChannel(randomInstance + "Team2");

                if (myTeam == 1)
                {

                    if (played == false)
                    {
                        PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                        played = true;
                    }

                    pFV.RPC("setKickable", RpcTarget.All,pFV.ViewID);
                }
                if (myTeam == 2 && other.GetComponent<PlayerController>().healthbar1)
                {
                    pFV.RPC("destHB", RpcTarget.All, pFV.ViewID);

                }





            }
            if (myPlayerC.entered2 == false)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Path");
                if (myTeam == 1)
                {
                    played = false;
                }
            }

        }
    }
    
    [PunRPC]
    void playDing(int viewID)
    {
       
            PhotonView.Find(viewID).GetComponent<AudioManagerTwo>().ding.Play();
        
    }
   
   
}
