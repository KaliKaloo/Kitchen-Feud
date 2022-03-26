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

    private int fadingTrack;
    private bool firstLoop;

    public static MusicManager instance;
    // public int playerTeam;
    public int location;

    
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
        if (timer.GetCurrentTime() == (int)(totalTime*0.3)){
            if(location == 1 || location == 2)
                changeBGM(location, 10, 1, 0);
            switched = true;

        }
        

        if (!played){
            if (location == 1){
                track1.clip = k1track1;
            }else if (location == 2){
                track1.clip = k2track1;
            }
            track1.Play();
            track1.loop = true;
            played = true;

        }
    }

    public void changeBGM(int team, int FadeTime, float minVol, float maxVol){
        StopAllCoroutines();
        AudioClip newTrack;
        if (team == 1){
            newTrack = switched ? k1track2 : k1track1;
        }else if (team == 2){
            newTrack = switched ? k2track2 : k2track1;
        }else{
            newTrack = hallway;
        }
        firstLoop = true;
        StartCoroutine(fadeTrack(newTrack, FadeTime, minVol, maxVol, firstLoop));
    }


    private IEnumerator fadeTrack(AudioClip newTrack, int FadeTime, float minVol, float maxVol, bool firstLoop){
        float timeElapsed = 0;
        float track1CurrentVol = 0;
        float track2CurrentVol = 0;

        if ((track1.isPlaying  && !track2.isPlaying)|| (track1.isPlaying && track2.isPlaying && fadingTrack == 2)){
            if (newTrack != track1.clip){
                track2.clip = newTrack;
                track2.Play();
                if (firstLoop){
                    fadingTrack = 1;
                    firstLoop = false;
                    track1CurrentVol = track1.volume;
                    track2CurrentVol = track2.volume;

                }   
                while (timeElapsed < FadeTime){
                    track1.volume = Mathf.Lerp(track1CurrentVol, minVol, timeElapsed/FadeTime);
                    track2.volume = Mathf.Lerp(track2CurrentVol, maxVol, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                track1.Stop();
                
            }

        } else {
             if (newTrack != track2.clip){
                track1.clip = newTrack;
                track1.Play();
                if (firstLoop){
                    fadingTrack = 2;
                    firstLoop = false;
                    track1CurrentVol = track1.volume;
                    track2CurrentVol = track2.volume;
                }  
                while (timeElapsed < FadeTime){
                    track2.volume = Mathf.Lerp(track2CurrentVol, minVol, timeElapsed/FadeTime);
                    track1.volume = Mathf.Lerp(track1CurrentVol, maxVol, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                
                track2.Stop();
            } 
        }
    }
}
