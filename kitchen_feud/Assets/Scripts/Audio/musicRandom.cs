using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Track{
    k1_1,
    k1_2,
    k2_1,
    k2_2
}


public class musicRandom : MonoBehaviour
{
    private static GlobalTimer timer = new GlobalTimer();

    private AudioSource track1, track2;


    public AudioClip[] k1_1, k1_2, k2_1, k2_2, musicClips;
    public AudioClip k1_MG, k2_MG ;

    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    public static musicRandom instance;

    public Track track;


    // new ones
    private int totalTime, fadingTrack;

    private bool switched = false;
    public int location, team;
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
        playRandom(); // start playing
    }


    void Update()
    {
        // switch to part 2 tracks
        if (!switched && !inMG && timer.GetLocalTime() < (int)(totalTime*0.3)){
            if(location == 1 || location == 2){
                switched = true;
                // changeBGM(location, 10, 0, 1);
            }
        }
        playRandom();      
        
        setVolume();
    }

// change to set music clips
// don't need track any more?
    void setTrackFromLocation(){
        if (location == 1){
            track = switched ? (Track)1 : (Track)0;
        }else if (location == 2){
            track = switched ? (Track)3 : (Track)2;
        }

    }


    




    public void playRandom(){
        // audioSource.clip = GetRandomAudioClip();
        // audioSource.Play();
        // Invoke("playRandom", audioSource.clip.length);
    }


    


    public AudioClip GetRandomAudioClip() {
        findTrackArray();
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

    private void findTrackArray(){
        switch (track)
        {
            case (Track)0:
                musicClips = k1_1;
                break;

            case (Track)1:
                musicClips = k1_2;
                break;

            case (Track)2:
                musicClips = k2_1;
                break;
            case (Track)3:
                musicClips = k2_2;
                break;

            default:
                break;
        }
    }

    private bool PreviousArrayContainsAudioClipIndex() {
        for (int i = 0; i < previousArray.Length; i++) {
            if (previousArray[i] == audioClipIndex) {
                return true;
            }
        }
        return false;
    }


    //set music volume
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
