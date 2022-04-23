using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicInfo {
   public float time;
   public AudioClip track;

   public int trackIndex;
};  


[System.Serializable]
public class MusicHolder
{
    public AudioClip[] tracks;
    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    private MusicInfo info;


    public AudioClip GetRandomAudioClip() {
        if (previousArray == null || previousArray.Length != tracks.Length / 2) {
            previousArray = new int[tracks.Length / 2];
        }
        if (previousArray.Length == 0) {
            return tracks[0];
        } else {
            do {
                audioClipIndex = Random.Range(0, tracks.Length);
            } while (PreviousArrayContainsAudioClipIndex());
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

    public void setInfo(float time){
        info.time = time;
        info.trackIndex = audioClipIndex;
        info.track = tracks[audioClipIndex];
    }

    public MusicInfo getInfo(){
        return info;
    }


   
}
