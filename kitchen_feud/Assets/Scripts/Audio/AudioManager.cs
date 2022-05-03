using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;

public class AudioManager : MonoBehaviour
{
    IRtcEngine engine;
    int myTeam;
    int randomInstance;
    public AudioSource ding;
    public PhotonView PV;
    public bool played;
    public GameObject otherP;
    public string Speaker;
    public int team;
    private string band;
    EnableSmoke enableSmoke = new EnableSmoke();



    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = (int) PhotonNetwork.CurrentRoom.CustomProperties["Lobby"];
    }
    
    
    void Start()
    {

        MusicManagerOld.instance.location = myTeam;

        // MusicManager.instance.location = myTeam;
        PV = GetComponent<PhotonView>();
        ding = GameObject.FindGameObjectWithTag(Speaker).GetComponent<AudioSource>();
        band =(string) PhotonNetwork.LocalPlayer.CustomProperties["Band"];
        engine.LeaveChannel();


        if (band == "A")
        {
            engine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_HIGH_QUALITY_STEREO,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);
            
        }else if (band == "B")
        {
            engine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_HIGH_QUALITY,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);
            
        }else if(band == "C")

        {
            engine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_MUSIC_STANDARD,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);
            
        }else if (band == "D")
        {
            engine.SetAudioProfile(AUDIO_PROFILE_TYPE.AUDIO_PROFILE_SPEECH_STANDARD,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_MEETING);
            
        }

        if (myTeam == 1)
        {
            
            engine.JoinChannel(randomInstance + "Team1");
           
        }else if(myTeam == 2)
        {
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
                // MusicManager.instance.switchLocation(team);
                MusicManagerOld.instance.changeBGM(team, 10, 0, 1);
                MusicManagerOld.instance.location = team;


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
                    enableSmoke.ChangePlayerState(true);
                    globalClicked.enterEnemyKitchen = true;
                    {
                        if (myPlayerC.played == false)
                        {
                            PV.RPC("playDing", RpcTarget.All, PV.ViewID);
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 1);
                        }
                        pFV.RPC("setKickable", RpcTarget.AllBuffered, pFV.ViewID);
                    }
                }
                else
                {
                    enableSmoke.ChangePlayerState(false);
                    globalClicked.enterEnemyKitchen = false;


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
