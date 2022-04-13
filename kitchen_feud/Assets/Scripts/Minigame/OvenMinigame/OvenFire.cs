using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OvenFire : MonoBehaviour
{
    Timer timer;
    bool startFire = false;
    Component[] PS;
    public AudioSource fireSound;

    void Start()
    {
        fireSound = transform.parent.GetComponentInChildren<AudioSource>();
        timer = transform.GetComponent<Timer>();
        PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        //and condition if temperature is too high
        if(!startFire){
            if(timer.timer < -5){
                foreach(ParticleSystem p in PS){
                    p.Play();
                }
                //SOUND -------------------------------------------
                fireSound.gameObject.GetComponent<PhotonView>().RPC("PlayFireSound", RpcTarget.AllBuffered, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
                //-------------------------------------------------
                startFire = true;

            }
        }

    }
}
