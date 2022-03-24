using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    // public AudioSource BGM;
    private static GlobalTimer timer = new GlobalTimer();
    private AudioSource track1, track2;

    public AudioClip newTrack;

    private int FadeTime = 5;

    private int totalTime;
    
    void Start()
    {
        totalTime = timer.GetTotalTime();
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();

    }

    void Update()
    {
        if (timer.GetCurrentTime() == (int)(totalTime*0.3)){
            changeBGM(newTrack);
        }
    }


    public void changeBGM(AudioClip newTrack){
       StopAllCoroutines();
       StartCoroutine(fadeTrack(newTrack));
    }



    private IEnumerator fadeTrack(AudioClip newClip){
        float timeElapsed = 0;
        track2.clip = newClip;
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
