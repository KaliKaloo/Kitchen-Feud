using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    private static GlobalTimer timer = new GlobalTimer();

    private AudioSource track1, track2;

    public MusicHolder k1_1, k1_2, k2_1, k2_2, hallway, musicClips;
    public AudioClip k1_MG, k2_MG ;

    public static MusicManager instance;

    private int totalTime, fadingTrack, fadeTime = 5;

    private bool switched = false, MGStarted = false;
    public int location;
    public bool inMG = false;
    public float musicVol;

   
    void Awake(){
        if (instance == null){
            instance = this;
        }
    }
    
    void Start()
    {
        totalTime = timer.GetTotalTime();
        track1 = gameObject.AddComponent<AudioSource>();
        track1.volume = 0;
        track2 = gameObject.AddComponent<AudioSource>();

        // start playing
        Debug.Log("hm");

        StartCoroutine(startTrack());
    }


    void Update()
    {
        // switch to part 2 tracks
        if (!switched && !inMG && timer.GetLocalTime() < (int)(totalTime*0.3)){
            if(location == 1 || location == 2){
                switched = true;
                setMusicClips();
            }
        }
        GameObject volumeSlider = GameObject.Find("Music Volume");
        if (volumeSlider){
            setVolume(volumeSlider);
        }
    }

    void setMusicClips(){
        if (location == 1){
            musicClips = switched ? k1_2 : k1_1;
        }else if (location == 2){
            musicClips = switched ? k2_2 : k2_1;
        }else{
            musicClips = hallway;
        }
        Debug.Log(location);

    }

    private AudioSource getAudioSource(){
        if (location == 1){
            return track1;
        }else{
            return track2;
        }
    }

    public void switchLocation(int loc){
        AudioSource oldAudio = getAudioSource();
        location = loc;
        AudioSource newAudio = getAudioSource();
        setMusicClips();
        AudioClip newTrack = musicClips.GetRandomAudioClip();
        StartCoroutine(switchTrack(oldAudio, newAudio, newTrack));
    }

    private IEnumerator switchTrack(AudioSource oldAudio, AudioSource newAudio, AudioClip newTrack){
        float timeElapsed = 0;
        float track1CurrentVol = 0;
        float track2CurrentVol = 0;
    
        newAudio.clip = newTrack;
        fadingTrack = 1;
        track1CurrentVol = oldAudio.volume;
        track2CurrentVol = newAudio.isPlaying ? newAudio.volume : 0;

        newAudio.Play();

        while (timeElapsed < fadeTime){
            oldAudio.volume = Mathf.Lerp(track1CurrentVol, 0, timeElapsed/fadeTime);
            newAudio.volume = Mathf.Lerp(track2CurrentVol, 1, timeElapsed/fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;

        }
        oldAudio.Stop();
    }

    private IEnumerator startTrack(){
        setMusicClips();
        track1.clip = musicClips.GetRandomAudioClip();
        Debug.Log(musicClips.GetRandomAudioClip());
        track1.Play();
        float timeElapsed = 0;
        while (timeElapsed < fadeTime){
            track1.volume = Mathf.Lerp(0, musicVol, timeElapsed/fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Invoke("playRandom", track1.clip.length);
    }

    public void playRandom(){
        AudioSource track = getAudioSource();
        track.clip = musicClips.GetRandomAudioClip();
        track.Play();
        Invoke("playRandom", track.clip.length);
    }


    public void minigameSwitch(){
        // if (!MGStarted){
        //     AudioClip newTrack = (location == 1) ? k1_MG : k2_MG;
        //     CancelInvoke("playRandom");
        //     AudioSource track = location == 1 ? track1 : track2;
        //     track.Pause();
        //     mgSource.clip = newTrack;
        //     mgSource.Play();
        //     MGStarted = true;
        // }
    }

    public void minigameEnd(){
        // mgSource.Stop();
        // AudioSource track = location == 1 ? track1 : track2;
        // track.UnPause();
        // Invoke("playRandom", track.clip.length - track.time);
        // MGStarted = false;
    }

  
    
    private void setVolume(GameObject volumeSlider){
        musicVol = volumeSlider.GetComponentInChildren<Slider>().value;
        if (track1.isPlaying && !track2.isPlaying)
            track1.volume = musicVol;
        else if (track2.isPlaying && !track1.isPlaying)
            track2.volume = musicVol;
    }
   
}
