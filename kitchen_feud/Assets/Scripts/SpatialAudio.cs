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
  
  
   

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        agoraAduioEffects = VoiceChatManager.Instance.GetRtcEngine().GetAudioEffectManager();
        engine = VoiceChatManager.Instance.GetRtcEngine();
        myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        Debug.LogError(myTeam);
        myC = 0;
    
        

        spatialAudioFromPlayers[PV.Owner] = this;
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
   

        if (!PV.IsMine)
            return;
        
        if (Vector3.Distance(new Vector3(-3.28f, 1.09f, -14.94f),transform.position) > 10)
        {
            //Debug.LogError("You're too far away");
            //engine.EnableLocalAudio(false);
            if (myTeam == 2 && myC == 1)
            {
                engine.LeaveChannel();
                engine.JoinChannel("Team2");
                myC = 2;
            }

        }
        else
        {
            if(myTeam == 1 && myC == 0)
            {
                engine.LeaveChannel();
                engine.JoinChannel("Team1");
                myC = 1;
            }

            if (myTeam == 2 && myC == 2) {
                engine.LeaveChannel();
                engine.JoinChannel("Team1");
                myC = 1;
            }
        }

        if (Vector3.Distance(new Vector3(-3.22f, 1.09f,9.4f), transform.position) > 10)
        {
            //Debug.LogError("You're too far away");
            //engine.EnableLocalAudio(false);
            if (myTeam == 1 && myC == 2)
            {
                engine.LeaveChannel();
                engine.JoinChannel("Team1");
                myC = 1;
            }

        }
        else
        {
            if(myTeam == 2 && myC == 0)
            {
                engine.LeaveChannel();
                engine.JoinChannel("Team2");
                myC = 2;
               
               

            }


            if (myTeam == 1 && myC == 1)
            {
                engine.LeaveChannel();
                engine.JoinChannel("Team2");
                
                myC = 2;
            }
        }

        

        /*
                foreach(Photon.Realtime.Player player in PhotonNetwork.CurrentRoom.Players.Values)
                {
                    if (player.IsLocal)
                        continue;

                    if (player.CustomProperties.TryGetValue("agoraID", out object agoraID))
                    {
                        if (spatialAudioFromPlayers.ContainsKey(player))
                        {
                            SpatialAudio other = spatialAudioFromPlayers[player];

                            float gain = GetGain(other.transform.position);
                            float pan = GetPan(other.transform.position);

                            agoraAduioEffects.SetRemoteVoicePosition(uint.Parse((string)agoraID), pan, gain);
                        //engine.AdjustUserPlaybackSignalVolume(uint.Parse((string)agoraID),10);
                        }
                        else
                        {
                            agoraAduioEffects.SetRemoteVoicePosition(uint.Parse((string)agoraID), 0, 0);
                        }
                    }

                }
        */
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

}
