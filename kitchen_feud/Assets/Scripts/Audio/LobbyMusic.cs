using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusic : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip track1, track2;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.05f;
        audioSource.clip = track1;
        audioSource.Play();
        Invoke("loopTrack2", track1.length);
    }


    public void loopTrack2(){
        audioSource.clip = track2;
        audioSource.Play();
        audioSource.loop = true;
    }


}
