using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MusicHolder
{
    public AudioClip[] tracks;
    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;


    //get pseudo-random clip from array
    public AudioClip GetRandomAudioClip() {
        if (previousArray == null || previousArray.Length != tracks.Length / 2) {
            previousArray = new int[tracks.Length / 2];
        }
        if (previousArray.Length == 0) {
            return tracks[0];
        } else {
            do {
                audioClipIndex = Random.Range(0, tracks.Length);
            } while (PreviousArrayContainsAudioClipIndex()); //check track hasn't been played recently
            previousArray[previousArrayIndex] = audioClipIndex;
            previousArrayIndex++;
            if (previousArrayIndex >= previousArray.Length) {
                previousArrayIndex = 0;
            }
        }

        return tracks[audioClipIndex];
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
