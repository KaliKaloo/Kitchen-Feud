using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static GlobalTimer timer = new GlobalTimer();
    private AudioSource track1, track2;

    public AudioClip k1track1, k1track2, k2track1, k2track2;

    private int FadeTime = 5;

    private int totalTime;

    private bool switched = false;
    
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
            switched = true;
            changeBGM(k1track2);
        }
    }

    public void changeBGM(int team){
       StopAllCoroutines();
       AudioClip newTrack;
       if (team == 1){
           newTrack = switched ? k1track1 : k1track2;
       }else{
            newTrack = switched ? k2track1 : k2track2;

       }
       StartCoroutine(fadeTrack(newTrack));
    }
    public void changeBGM(AudioClip newTrack){
       StopAllCoroutines();
       StartCoroutine(fadeTrack(newTrack));
    }



    private IEnumerator fadeTrack(AudioClip newTrack){
        float timeElapsed = 0;
        track2.clip = newTrack;
        track2.Play();
        while (timeElapsed < FadeTime){
            track1.volume = Mathf.Lerp(1, 0, timeElapsed/FadeTime);
            track2.volume = Mathf.Lerp(0, 0.6f, timeElapsed/FadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
        
        track1.Stop();
    }
}
