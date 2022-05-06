using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using agora_gaming_rtc;
using UnityEngine.EventSystems;


public class kickPlayers : MonoBehaviour
{
    public GameObject[] players;
    public PhotonView PV;
    public List<int> oPl1;
    public List<int> oPl2;
    IRtcEngine engine;
    public static kickPlayers Instance;
    int randomInstance;
    


    void Awake()
    {
        Instance = this;
        engine = VoiceChatManager.Instance.GetRtcEngine();
//        randomInstance = menuController.Instance.x.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        randomInstance = (int) PhotonNetwork.CurrentRoom.CustomProperties["Lobby"];
        
        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Local"))
        {
            GameObject localP = GameObject.Find("Local");

            if (!localP.GetComponentInChildren<AudioListener>().enabled)
            {
                localP.GetComponentInChildren<AudioListener>().enabled = true;
            }
        }


        if (PhotonNetwork.CurrentRoom != null && players.Length < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }


        if (PhotonNetwork.CurrentRoom != null && players.Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (GameObject.Find("Local"))
            {
                GameObject localP = GameObject.Find("Local");



                if (Vector3.Distance(new Vector3(11.21f, 0.201f, -3.37f), localP.transform.position) < 10 &&
                   localP.GetComponent<PlayerVoiceManager>().myC == 0 &&
                   localP.GetComponent<PlayerController>().myTeam == 1)
                {
                    engine.LeaveChannel();
                    engine.JoinChannel(randomInstance + "Team1");
                    PV.RPC("setMyC", RpcTarget.All, localP.GetComponent<PhotonView>().ViewID, 1);
                }

                if (Vector3.Distance(new Vector3(-7.35f, 0.201f, -4.27f), localP.transform.position) < 10 &&
                  localP.GetComponent<PlayerVoiceManager>().myC == 0 &&
                   localP.GetComponent<PlayerController>().myTeam == 2)
                {
                    engine.LeaveChannel();
                    engine.JoinChannel(randomInstance + "Team2");
                    PV.RPC("setMyC", RpcTarget.All, localP.GetComponent<PhotonView>().ViewID, 1);
                }
            }

        }
    }

    public void kickPlayer(GameObject obj)
    {
        PhotonView OV = obj.GetComponent<PhotonView>();
        
        {
            if (obj.GetComponent<PlayerController>().myTeam == 1)
            {
                
                OV.RPC("synctele", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, new Vector3(11.21f,0.201f, -3.37f));
                OV.RPC("setEnteredF", RpcTarget.All, OV.ViewID, 2);
                OV.RPC("setEntered", RpcTarget.All,OV.ViewID, 1);
                PV.RPC("setMyC", RpcTarget.All, OV.ViewID,0);
                OV.RPC("setKickableF", RpcTarget.All, OV.ViewID);
                Debug.Log(OV.transform.position);

            }
            else
            {
                obj.GetComponent<PhotonView>().RPC("synctele", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, new Vector3(-7.35f, 0.201f, -4.27f));
                OV.RPC("setEnteredF", RpcTarget.All, OV.ViewID, 1);
                OV.RPC("setEntered", RpcTarget.All, OV.ViewID, 2);
                PV.RPC("setMyC", RpcTarget.All, OV.ViewID, 0);
                OV.RPC("setKickableF", RpcTarget.All, OV.ViewID);

            }
        }
       
    }


    [PunRPC]
    void setMyC(int viewiD,int x)
    {
        PlayerVoiceManager OV = PhotonView.Find(viewiD).GetComponent<PlayerVoiceManager>();
        OV.myC = x;
    }
}
