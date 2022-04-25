// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;


// public class MusicManager2 : MonoBehaviour
// {
//     private static GlobalTimer timer = new GlobalTimer();
//     private AudioSource track1, track2;

//     private AudioClip k1track1, k1track2, k2track1, k2track2, hallway;
//     public AudioClip k1_MG, k2_MG ;

//     private int totalTime;

//     private bool switched = false;
//     private bool played = false;
//     private int fadingTrack;

//     public static MusicManager2 instance;
//     public int location;

//     public bool inMG = false;
//     public float musicVol = 0.1f;

//     //DONE
//     void Awake(){
//         if (instance == null){
//             instance = this;
//         }
//     }
//     //DONE
//     void Start()
//     {
//         totalTime = timer.GetTotalTime();
//         track1 = gameObject.AddComponent<AudioSource>();
//         track2 = gameObject.AddComponent<AudioSource>();

//     }

//     void Update()
//     {
//         // switch to part 2 tracks
//         if (!switched && !inMG && timer.GetLocalTime() < (int)(totalTime*0.3)){
//             if(location == 1 || location == 2){
//                 switched = true;
//                 changeBGM(location, 10, 0, 1);
//             }
//         }
//         // musicRandom.instance.playRandom();      
        
//         //start playing
//         if (!played){
//             if (location == 1){
//                 track1.clip = k1track1;
//             }else if (location == 2){
//                 track1.clip = k2track1;
//             }
//             track1.volume = 0;
//             track1.Play();
//             track1.loop = true;
//             played = true;
//             StartCoroutine(fadeTrack(track1, 10, 0));
//         }

//         setVolume();
//     }


    

//     private IEnumerator fadeTrack(AudioSource source, int FadeTime, float startVol){
//         float timeElapsed = 0;
//         while (timeElapsed < FadeTime){
//             track1.volume = Mathf.Lerp(startVol, musicVol, timeElapsed/FadeTime);
//             timeElapsed += Time.deltaTime;
//             yield return null;
//         }
//     }


// //DONE
//     public void minigameSwitch(){
//         //switch to MG music
//         AudioClip newTrack;
//         newTrack = (location == 1) ? k1_MG : k2_MG;
//         StartCoroutine(switchTrack(newTrack, 1, 0, 1));
        
//     }

// //DONE
//     public void minigameEnd(){
//         //change back to kitchen music
//         changeBGM(location, 1, 0, 1);
//     }
    
//     public void changeBGM(int team, int FadeTime, float minVol, float maxVol){
//         StopAllCoroutines();
//         // if (team == 1){
//         //     musicRandom.instance.track = switched ? Track.k1_2 : Track.k1_1;
//         // }else if (team == 2){
//         //     musicRandom.instance.track = switched ? Track.k2_2 : Track.k2_1;
//         // }else{
//         //     // newTrack = hallway;
//         // }
//         // musicRandom.instance.playRandom();      

//         // bool track1Switch = (track1.isPlaying  && !track2.isPlaying)|| (track1.isPlaying && track2.isPlaying && fadingTrack == 2);
//         // StartCoroutine(switchTrack(newTrack, FadeTime, minVol, maxVol));
//     }


//     private IEnumerator switchTrack(AudioClip newTrack, int FadeTime, float minVol, float maxVol){
//         //fade music between track switch
//         float timeElapsed = 0;
//         float track1CurrentVol = 0;
//         float track2CurrentVol = 0;

//         if ((track1.isPlaying  && !track2.isPlaying)|| (track1.isPlaying && track2.isPlaying && fadingTrack == 2)){
            
//             if (newTrack != track1.clip){
//                 track2.clip = newTrack;
//                 fadingTrack = 1;
//                 track1CurrentVol = track1.volume;
//                 track2CurrentVol = track2.isPlaying ? track2.volume : 0;

//                 track2.Play();

//                 while (timeElapsed < FadeTime){
//                     track1.volume = Mathf.Lerp(track1CurrentVol, minVol, timeElapsed/FadeTime);
//                     track2.volume = Mathf.Lerp(track2CurrentVol, musicVol, timeElapsed/FadeTime);
//                     timeElapsed += Time.deltaTime;
//                     yield return null;

//                 }
//                 track1.Stop();
                
//             }

//         } else {
//              if (newTrack != track2.clip){
//                 track1.clip = newTrack;
//                 fadingTrack = 2;
//                 track1CurrentVol = track1.isPlaying ? track1.volume : 0;
//                 track2CurrentVol = track2.volume;
//                 track1.Play();

//                 while (timeElapsed < FadeTime){
//                     track2.volume = Mathf.Lerp(track2CurrentVol, minVol, timeElapsed/FadeTime);
//                     track1.volume = Mathf.Lerp(track1CurrentVol, musicVol, timeElapsed/FadeTime);
//                     timeElapsed += Time.deltaTime;
//                     yield return null;

//                 }
                
//                 track2.Stop();
//             } 
//         }
//     }

//     // DONE
//     private void setVolume(){
//         //set music volume
//         GameObject volumeSlider = GameObject.Find("Music Volume");
//         if (volumeSlider){
//             musicVol = volumeSlider.GetComponentInChildren<Slider>().value;
//             if (track1.isPlaying && !track2.isPlaying)
//                 track1.volume = musicVol;
//             else if (track2.isPlaying && !track1.isPlaying)
//                 track2.volume = musicVol;
//         }
//     }
  
// }
