using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static GlobalTimer timer = new GlobalTimer();
    private AudioSource track1, track2;

    public AudioClip k1track1, k1track2, k2track1, k2track2, hallway;

    private int totalTime;

    private bool switched = false;
    private bool played = false;

    private int lastPlayed;

    public static MusicManager instance;
    public int playerTeam;
    
    void Awake(){
        if (instance == null){
            instance = this;
        }
    }
    
    void Start()
    {
        totalTime = timer.GetTotalTime();
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();

    }

    void Update()
    {
        // if (timer.GetCurrentTime() == (int)(totalTime*0.3)){

        //     changeBGM(k1track2);
        //     switched = true;

        // }
        

        if (!played){
            if (playerTeam == 1){
                track1.clip = k1track1;
            }else if (playerTeam == 2){
                track1.clip = k2track1;
            }
            track1.Play();
            track1.loop = true;
            played = true;
            lastPlayed = true;


        }
    }

    public void changeBGM(int team, int FadeTime, float startVol, float endVol){
        StopAllCoroutines();
        AudioClip newTrack;
        if (team == 1){
            newTrack = switched ? k1track2 : k1track1;
        }else if (team == 2){
            newTrack = switched ? k2track2 : k2track1;
        }else{
            newTrack = hallway;
        }
        StartCoroutine(fadeTrack(newTrack, nextTrackis1, FadeTime, startVol, endVol));
    }


    private IEnumerator fadeTrack(AudioClip newTrack, bool nextTrackis1, int FadeTime, float startVol, float endVol){
        float timeElapsed = 0;
        // Debug.Log(nextTrackis1);
        if (!nextTrackis1){
            if (newTrack != track1.clip){
                lastPlayed = 2;
                track2.clip = newTrack;
                track2.Play();
                while (timeElapsed < FadeTime){
                    track1.volume = Mathf.Lerp(startVol, endVol, timeElapsed/FadeTime);
                    track2.volume = Mathf.Lerp(endVol, startVol, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                track1.Stop();
                
            }

        } else {
             if (newTrack != track2.clip){
                lastPlayed = 1;

                track1.clip = newTrack;
                track1.Play();
                while (timeElapsed < FadeTime){
                    track2.volume = Mathf.Lerp(startVol, endVol, timeElapsed/FadeTime);
                    track1.volume = Mathf.Lerp(endVol, startVol, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                
                track2.Stop();
            } 
        }
    }
}
