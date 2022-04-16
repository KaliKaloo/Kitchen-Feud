using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Track{
    k1_1,
    k1_2,
    k2_1,
    k2_2
}



// [System.Serializable]
public class musicRandom : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip[] k1_1, k1_2, k2_1, k2_2, musicClips;
    public AudioClip last;
    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    public static musicRandom instance;

    private Track track;

    
    void Awake(){
        if (instance == null){
            instance = this;
        }
    }
    
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        playRandom();      
    }


    public void playRandom(){
        audioSource.clip = GetRandomAudioClip();
        audioSource.Play();
        Invoke("playRandom", audioSource.clip.length);

        
    }


    private void findTrackArray(){
        switch (track)
        {
            case (Track)1:
                musicClips = k1_1;
                break;

            case (Track)2:
                musicClips = k1_2;
                break;

            case (Track)3:
                musicClips = k2_1;
                break;
            case (Track)4:
                musicClips = k2_2;
                break;

            default:
                break;
        }
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

    private bool PreviousArrayContainsAudioClipIndex() {
        for (int i = 0; i < previousArray.Length; i++) {
            if (previousArray[i] == audioClipIndex) {
                return true;
            }
        }
        return false;
    }
   
}
