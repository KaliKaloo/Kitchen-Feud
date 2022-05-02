using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;


public class AM : MonoBehaviour
{
    IRtcEngine engine;
    int randomInstance;
    int myTeam;
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
        PV = GetComponent<PhotonView>();
        band =(string) PhotonNetwork.LocalPlayer.CustomProperties["Band"];
    }


   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PhotonView pFV = other.GetComponent<PhotonView>();
            PlayerVoiceManager myPlayerC = other.GetComponent<PlayerVoiceManager>();
            if (pFV.IsMine)
            {
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
                engine.JoinChannel(randomInstance + "Path");
              
                if (team == 1)
                {
                    myTeam = myPlayerC.myTeam;

                    //enter hallway
                    if (myPlayerC.entered1 == true)
                    {
                        MusicManager.instance.switchLocation(3);
                        pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 1);
                        if (myTeam == 2)
                        {
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                        }
                    }else{
                        MusicManager.instance.switchLocation(1);

                    }
                }
                else
                {
                    myTeam = myPlayerC.myTeam;

                    //enter hallway
                    if (myPlayerC.entered2 == true)
                    {
                        MusicManager.instance.switchLocation(3);
                        pFV.RPC("setEnteredF", RpcTarget.All, pFV.ViewID, 2);
                       
                        if (myTeam == 1)
                        {
                            pFV.RPC("setPlayed", RpcTarget.All, pFV.ViewID, 0);
                        }
                    }
                    else{
                        MusicManager.instance.switchLocation(2);

                    }
                }

                if (myTeam != team)
                {
                    enableSmoke.ChangePlayerState(false);
                }
            }
        }

    }
}
