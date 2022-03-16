using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;


public class AMOne : MonoBehaviour
{
    IRtcEngine engine;
    string randomInstance;
    int myTeam;
    public AudioSource ding;
    public PhotonView PV;
    public bool played;
    public GameObject otherP;
    // Start is called before the first frame update
    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        ding = GameObject.FindGameObjectWithTag("Speaker1").GetComponent<AudioSource>();
    }
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
        if (pFV.IsMine)
        {
           
            myTeam = myPlayerC.myTeam;
            if (myPlayerC.entered1 == true)
            {
                pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 1);
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Path");
                if (myTeam == 2)
                {
                    pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                }
            }
            //GetComponent<BoxCollider>().enabled = true;
        }

    }
}
