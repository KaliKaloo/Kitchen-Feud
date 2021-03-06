using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    private static GlobalTimer timer = new GlobalTimer();

    private AudioSource track1, track2, mgSource;

    public AudioSource track;

    public MusicHolder k1_1, k1_2, k2_1, k2_2, hallway, musicClips;
    public AudioClip k1_MG, k2_MG;

    public static MusicManager instance;

    private int totalTime, fadingTrack, fadeTime = 5;

    private bool switched = false, MGStarted = false;
    public int location;
    public bool inMG = false, priorityPitch = false;
    private float musicVol, sliderVol = 0.5f;
    public float pitch = 1;

   
    void Awake(){
        if (instance == null){
            instance = this;
        }
    }
    
    void Start()
    {
        totalTime = timer.GetTotalTime();
        track1 = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        track1.volume = 0;
        track2 = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        track2.volume = 0;
        track = track1;

        // start playing
        StartCoroutine(startTrack());
    }


    void Update()
    {
        // switch to part 2 tracks in latter part of game
        if (!switched && !inMG && timer.GetLocalTime() < (int)(totalTime*0.3)){
            if(location == 1 || location == 2){
                switched = true;
                setMusicClips();
            }
        }
        //adjust volume
        GameObject volumeSlider = GameObject.Find("Music Volume");
        if (volumeSlider){
            settingsVolume(volumeSlider);
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
    }

    public void switchLocation(int loc){
        CancelInvoke("playRandom");
        location = loc;
        //get track according to location
        setMusicClips();
        AudioClip newTrack = musicClips.GetRandomAudioClip();
        StartCoroutine(switchTrack(newTrack));
    }

    public void musicReact(){
        musicReact(1.3f);
    }

    // react to event by pitching
    public void musicReact(float pitchParam){
        CancelInvoke("playRandom");
        pitch = pitchParam;
        track.pitch = pitchParam;
        Invoke("playRandom", (track.clip.length - track.time)/pitch);

    }

    // set pitch to normal
    public void endReaction(){
        CancelInvoke("playRandom");
        pitch = 1f;
        track.pitch = 1f;
        Invoke("playRandom", (track.clip.length - track.time)/pitch);

    }

    // cross-fading between 2 tracks
    private IEnumerator switchTrack(AudioClip newTrack){
        float timeElapsed = 0;
        float track1CurrentVol = 0;
        float track2CurrentVol = 0;

        //if track 2 is the track to fade into
        if ((track1.isPlaying  && !track2.isPlaying)|| (track1.isPlaying && track2.isPlaying && fadingTrack == 2)){
            if (newTrack != track1.clip){
                fadingTrack = 1;
                track2.loop = location == 3;
                track2.clip = newTrack;
                track2.Play();
                track = track2;
                Invoke("playRandom", track2.clip.length/pitch);
                setVolume();
                track1CurrentVol = track1.volume;
                track2CurrentVol = track2.isPlaying ? track2.volume : 0;

                while (timeElapsed < fadeTime){
                    if (fadingTrack == 2) yield break;
                    track1.volume = Mathf.Lerp(track1CurrentVol, 0, timeElapsed/fadeTime);
                    track2.volume = Mathf.Lerp(track2CurrentVol, musicVol, timeElapsed/fadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                track1.Stop();

            }

        } else {
            
            if (newTrack != track2.clip){
                fadingTrack = 2;
                track1.loop = location == 3;
                track1.clip = newTrack;
                track1.Play();
                track = track1;
                Invoke("playRandom", track1.clip.length/pitch);
                setVolume();
                track2CurrentVol = track2.volume;
                track1CurrentVol = track1.isPlaying ? track1.volume : 0;

                while (timeElapsed < fadeTime){
                    if (fadingTrack == 1) yield break;
                    track2.volume = Mathf.Lerp(track2CurrentVol, 0, timeElapsed/fadeTime);
                    track1.volume = Mathf.Lerp(track1CurrentVol, musicVol, timeElapsed/fadeTime);
                    timeElapsed += Time.deltaTime;
                    yield return null;

                }
                
                track2.Stop();
            } 
        }
    }
   

    private IEnumerator startTrack(){
        setMusicClips();
        track1.clip = musicClips.GetRandomAudioClip();
        track1.Play();
        Invoke("playRandom", track1.clip.length/pitch);
        float timeElapsed = 0;
        setVolume();
        // fade into start track
        while (timeElapsed < fadeTime){
            track1.volume = Mathf.Lerp(0, musicVol, timeElapsed/fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    //play pseudo-random clip from array of clips from Music Holder
    public void playRandom(){
        track.clip = musicClips.GetRandomAudioClip();
        track.Play();
        Invoke("playRandom", track.clip.length/pitch);
        setVolume();
        track.volume = musicVol;
    }

    //switch to MG music
    public void minigameSwitch(){
        if (!MGStarted && track){
            track.Pause();
            AudioClip newTrack = (location == 1) ? k1_MG : k2_MG;
            CancelInvoke("playRandom");
            mgSource = track == track1 ? track2 : track1;
            mgSource.clip = newTrack;
            setVolume();
            mgSource.volume = musicVol;
            mgSource.Play();
            MGStarted = true;
        }
    }

    //switch back to kitchen music
    public void minigameEnd(){
        mgSource.Stop();
        track.UnPause();
        Invoke("playRandom", (track.clip.length - track.time)/pitch);
        MGStarted = false;
    }

    private void setVolume(){
        musicVol = sliderVol * volNormaliser();
    }
    
    //adjust volume volume according to settings
    private void settingsVolume(GameObject volumeSlider){
        sliderVol = volumeSlider.GetComponentInChildren<Slider>().value;
        musicVol = sliderVol * volNormaliser();
        if (track1.isPlaying && !track2.isPlaying)
            track1.volume = musicVol;
        else if (track2.isPlaying && !track1.isPlaying)
            track2.volume = musicVol;
    }


   

    //normalise volume of tracks according to location
    float volNormaliser(){
        switch(location){ 
            case 1:
                return 0.6f;
            case 2:
                return 0.6f;
            case 3:
                return 0.2f;
            default:
                return 1f;
        }
    }
}
