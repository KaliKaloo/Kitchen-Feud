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
    private string band;
    private void Awake()
    {
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        randomInstance = menuController.Instance.x.ToString();
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
              

                // MusicManager.instance.changeBGM(3, 10, 0, 1);
                MusicManager.instance.location = 3;



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
