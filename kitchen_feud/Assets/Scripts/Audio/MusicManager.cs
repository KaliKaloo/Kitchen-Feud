using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    private static GlobalTimer timer = new GlobalTimer();

    private AudioSource track1, track2, track3;

    public MusicHolder k1_1, k1_2, k2_1, k2_2, musicClips;
    public AudioClip k1_MG, k2_MG ;

    public static MusicManager instance;

    // new ones
    private int totalTime, fadingTrack;

    private bool switched = false, MGStarted = false;
    public int location;
    public bool inMG = false;
    public float musicVol = 0.1f;

   
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
        track3 = gameObject.AddComponent<AudioSource>();

        // start playing
        setMusicClips();
        playRandom();
    }


    void Update()
    {
        // switch to part 2 tracks
        if (!switched && !inMG && timer.GetLocalTime() < (int)(totalTime*0.3)){
            if(location == 1 || location == 2){
                CancelInvoke("playRandom");
                setMusicClips();
                playRandom();
                switched = true;
            }
        }
        setVolume();
    }

    void setMusicClips(){
        if (location == 1){
            musicClips = switched ? k1_2 : k1_1;
        }else if (location == 2){
            musicClips = switched ? k2_2 : k2_1;
        }

    }

    public void playRandom(){
        track1.clip = musicClips.GetRandomAudioClip();
        track1.Play();
        Invoke("playRandom", track1.clip.length);
    }

    //switch to MG music
    public void minigameSwitch(){
        if (!MGStarted){
            AudioClip newTrack = (location == 1) ? k1_MG : k2_MG;
            CancelInvoke("playRandom");
            track1.Pause();
            track3.clip = newTrack;
            track3.Play();
            MGStarted = true;
        }
    }


    public void minigameEnd(){
        track3.Stop();
        track1.UnPause();
        Invoke("playRandom", track1.clip.length - track1.time);
        MGStarted = false;
    }


    
    private void setVolume(){
        GameObject volumeSlider = GameObject.Find("Music Volume");
        if (volumeSlider){
            musicVol = volumeSlider.GetComponentInChildren<Slider>().value;
            if (track1.isPlaying && !track2.isPlaying)
                track1.volume = musicVol;
            else if (track2.isPlaying && !track1.isPlaying)
                track2.volume = musicVol;
        }
    }
   
}
