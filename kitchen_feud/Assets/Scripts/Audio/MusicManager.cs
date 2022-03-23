using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource BGM;
    private static GlobalTimer timer = new GlobalTimer();
    public AudioClip music2;

    private int totalTime;
    
    void Start()
    {
        totalTime = timer.GetTotalTime();
    }

    void Update()
    {
        if (timer.GetCurrentTime() == (int)(totalTime*0.2)){
            changeBGM(music2);
        }
    }


    public void changeBGM(AudioClip music){
        Debug.Log(music);
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
