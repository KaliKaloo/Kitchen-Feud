using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    private static GlobalTimer timer = new GlobalTimer();

    private AudioSource track1, track2;


    public AudioClip[] k1_1, k1_2, k2_1, k2_2, musicClips;
    public AudioClip k1_MG, k2_MG ;

    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    public static MusicManager instance;

    // new ones
    private int totalTime, fadingTrack;

    private bool switched = false;
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
        track1.clip = GetRandomAudioClip();
        track1.Play();
        Invoke("playRandom", track1.clip.length);
    }

    //switch to MG music
    public void minigameSwitch(){
        AudioClip newTrack = (location == 1) ? k1_MG : k2_MG;
        CancelInvoke("playRandom");
        track1.Pause();
        track1.clip = newTrack;
        track1.Play();
    }


    public void minigameEnd(){
       playRandom();
    }


    public AudioClip GetRandomAudioClip() {
        if (previousArray == null || previousArray.Length != musicClips.Length / 2) {
            previousArray = new int[musicClips.Length / 2];
        }
        if (previousArray.Length == 0) {
            return null;
        } else {
            do {
                audioClipIndex = Random.Range(0, musicClips.Length);
            } while (PreviousArrayContainsAudioClipIndex());
            previousArray[previousArrayIndex] = audioClipIndex;
            previousArrayIndex++;
            if (previousArrayIndex >= previousArray.Length) {
                previousArrayIndex = 0;
            }
        }

        return musicClips[audioClipIndex];
    }

    private bool PreviousArrayContainsAudioClipIndex() {
        for (int i = 0; i < previousArray.Length; i++) {
            if (previousArray[i] == audioClipIndex) {
                return true;
            }
        }
        return false;
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
