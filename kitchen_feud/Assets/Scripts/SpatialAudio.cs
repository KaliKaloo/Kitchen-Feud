using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using agora_gaming_rtc;
using System.Linq;

public class SpatialAudio : MonoBehaviour
{
    [SerializeField] float radius;
    static Dictionary<Photon.Realtime.Player, SpatialAudio> spatialAudioFromPlayers = new Dictionary<Photon.Realtime.Player, SpatialAudio>();
    IAudioEffectManager agoraAduioEffects;
    public PhotonPlayer cube;
    PhotonView PV;
    IRtcEngine engine;
    int myTeam;
    int myC;
    public AudioSource ding1;
    public AudioSource ding2;
    public bool isKickable;
    public bool ding1Played = false;
    public bool ding2Played = false;

 
    public GameObject kick;
    string randomInstance;
    
  
  
   

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        agoraAduioEffects = VoiceChatManager.Instance.GetRtcEngine().GetAudioEffectManager();
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
        myC = 0;
        ding1 = GameObject.FindGameObjectWithTag("Speaker1").GetComponent<AudioSource>();
        ding2 = GameObject.FindGameObjectWithTag("Speaker2").GetComponent<AudioSource>();


    }
    


    private void OnDestroy()
    {
        spatialAudioFromPlayers.Remove(PV.Owner);
        foreach (var item in spatialAudioFromPlayers.Where(x => x.Value == this).ToList())
        {
            spatialAudioFromPlayers.Remove(item.Key);
        }
    }

    private void Update()
    {
        if (!kick)
        {
            kick = GameObject.FindGameObjectWithTag("Kick");
            
        }
        

        if (!PV.IsMine)
            return;
        
        if (Vector3.Distance(new Vector3(-3.28f, 1.09f, -14.94f),transform.position) > 10)
        {
            if (myTeam == 2 && myC == 1)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team2");
                myC = 2;
            }
            if (myTeam == 2)
            {
                ding1Played = false;
                kick.GetComponent<PhotonView>().RPC("setEnteredF", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, 1);
                if(kickPlayers.Instance.oPl1.Contains(PV.ViewID)){
                    kick.GetComponent<PhotonView>().RPC("removeOp", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, PV.ViewID, 1);
                }
            }

        }
        else
        {
            
            if(myTeam == 1 && myC == 0)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team1");
                myC = 1;
            }

            if (myTeam == 2 && myC == 2) {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team1");
                myC = 1;
            }
            if(myTeam == 2)
            {
                Debug.LogError(ding1.isPlaying);
                    if (ding1Played == false)
                    {
                        PV.RPC("playDing", RpcTarget.All, PV.ViewID, 1);
                        ding1Played = true;
                    }
                    //PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                  
                //ding.Play();
                PV.RPC("setKickable", RpcTarget.All);
                kick.GetComponent<PhotonView>().RPC("setEntered", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, 1);
                if (!kickPlayers.Instance.oPl1.Contains(PV.ViewID))
                {
                    kick.GetComponent<PhotonView>().RPC("addOp", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, PV.ViewID, 1);
                }
            }

            if(myTeam == 1 && GetComponent<PlayerController>().healthbar1)
            {
                PV.RPC("destHB", RpcTarget.All, PV.ViewID);
            }
          
        }

        if (Vector3.Distance(new Vector3(-3.22f, 1.09f,9.4f), transform.position) > 10)
        {
            if (myTeam == 1 && myC == 2)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team1");
                myC = 1;
            }
            if (myTeam == 1)
            {
                ding2Played = false;
                kick.GetComponent<PhotonView>().RPC("setEnteredF", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, 2);
                if (kickPlayers.Instance.oPl2.Contains(PV.ViewID))
                {
                    kick.GetComponent<PhotonView>().RPC("removeOp", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, PV.ViewID, 2);
                }
            }

        }
        else
        {
            if(myTeam == 2 && myC == 0)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team2");
                myC = 2;
               
               

            }


            if (myTeam == 1 && myC == 1)
            {
                engine.LeaveChannel();
                engine.JoinChannel(randomInstance + "Team2");
                
                myC = 2;
            }
            if (myTeam == 1)
            {
                if (ding2Played == false)
                {
                    PV.RPC("playDing", RpcTarget.All, PV.ViewID, 2);
                    ding2Played = true;
                }
                PV.RPC("setKickable", RpcTarget.All);
                kick.GetComponent<PhotonView>().RPC("setEntered", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, 2);
                if (!kickPlayers.Instance.oPl2.Contains(PV.ViewID))
                {
                    kick.GetComponent<PhotonView>().RPC("addOp", RpcTarget.All, kick.GetComponent<PhotonView>().ViewID, PV.ViewID, 2);
                }
            }
            if (myTeam == 2 && GetComponent<PlayerController>().healthbar1)
            {
                PV.RPC("destHB", RpcTarget.All, PV.ViewID);
            }

        }

        if(Vector3.Distance(new Vector3(-3.28f, 1.09f, -14.94f), transform.position) > 10 && Vector3.Distance(new Vector3(-3.22f, 1.09f, 9.4f), transform.position) > 10 && !GetComponent<PlayerController>().healthbar1)
        {
            PV.RPC("setKickableF", RpcTarget.All);
        }
    }

    float GetGain(Vector3 otherPosition)
    {
        float distance = Vector3.Distance(transform.position, otherPosition);
        float gain = Mathf.Max(1 - (distance / radius), 0) * 100f;
        return gain;

    }

    float GetPan(Vector3 otherPosition)
    {
        Vector3 direction = otherPosition - transform.position;
        direction.Normalize();
        float dotProduct = Vector3.Dot(transform.right, direction);
        return dotProduct;

    }
    [PunRPC]
    void setKickable()
    {
        isKickable = true;
    }
    [PunRPC]
    void setKickableF()
    {
        isKickable = false;
    }
    [PunRPC]
    void playDing(int viewID,int room) {
        if (room == 1)
        {
            PhotonView.Find(viewID).GetComponent<SpatialAudio>().ding1.Play();
        }
        else
        {
            PhotonView.Find(viewID).GetComponent<SpatialAudio>().ding2.Play();
        }
        //ding.Play();
    }

}
