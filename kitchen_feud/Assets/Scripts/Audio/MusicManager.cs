using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static GlobalTimer timer = new GlobalTimer();
    private AudioSource track1, track2;

    public AudioClip k1track1, k1track2, k2track1, k2track2;

    private int totalTime;

    private bool switched = false;
    private bool played = false;

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
            }else if (playerTeam     == 2){
                track1.clip = k2track1;
            }
            track1.Play();
            track1.loop = true;
            played = true;
        }
    }

    public void changeBGM(int team){
        StopAllCoroutines();
        AudioClip newTrack;
        if (team == 1 || team == 3){
            newTrack = switched ? k1track1 : k1track2;
        }else{
            newTrack = switched ? k2track1 : k2track2;
        }
       StartCoroutine(fadeTrack(newTrack, 1, 10));
    }
    // public void changeBGM(AudioClip newTrack){
    //    StopAllCoroutines();
    //    StartCoroutine(fadeTrack(newTrack, 0.5f, 5));
    // }



    private IEnumerator fadeTrack(AudioClip newTrack, float volume, int FadeTime){
        float timeElapsed = 0;

        if (track1.isPlaying){
            if (newTrack != track1.clip){
                track2.clip = newTrack;
                track2.Play();
                while (timeElapsed < FadeTime){
                    track1.volume = Mathf.Lerp(1, 0, timeElapsed/FadeTime);
                    track2.volume = Mathf.Lerp(0, 1, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                
                track1.Stop();
            } else{
                float currentVol = track1.volume;
                while (timeElapsed < FadeTime){
                    track1.volume = Mathf.Lerp(currentVol, 1, timeElapsed/FadeTime);
                    yield return null;
                }

            }
        } else {
             if (newTrack != track2.clip){
                track1.clip = newTrack;
                track1.Play();
                while (timeElapsed < FadeTime){
                    track2.volume = Mathf.Lerp(1, 0, timeElapsed/FadeTime);
                    track1.volume = Mathf.Lerp(0, 1, timeElapsed/FadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                
                track1.Stop();
            } else{
                float currentVol = track1.volume;
                while (timeElapsed < FadeTime){
                    track2.volume = Mathf.Lerp(currentVol, 1, timeElapsed/FadeTime);
                    yield return null;
                }

            }
        }
    }
}
