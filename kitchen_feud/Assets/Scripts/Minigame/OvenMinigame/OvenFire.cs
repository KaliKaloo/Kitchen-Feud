using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OvenFire : MonoBehaviour
{
    public Timer timer;
    public bool startFire = false;
    ParticleSystem[] PS;
    public AudioSource fireSound;
    public FireOut fireOut;
    private bool hasParent;

    void Start()
    {
        
    }

    void Update()
    {
        if (!hasParent && transform.parent)
        {
            PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
            fireSound = transform.parent.GetComponentInChildren<AudioSource>();
            hasParent = true;
        }
        //and condition if temperature is too high
        if(!startFire){
            if(timer.timer < -5){
                foreach(ParticleSystem p in PS){
                    if(p.GetComponent<FireOut>()){
                        fireOut = p.GetComponent<FireOut>();
                        fireOut.resetEmission();
                        fireOut.enabled=true;
                    }
                    p.Play();
                }
                //SOUND -------------------------------------------
                fireSound.gameObject.GetComponent<PhotonView>().RPC("PlayFireSound", RpcTarget.All, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
                //-------------------------------------------------
                MusicManager.instance.suddenTrackChange();
                startFire = true;

            }
        }
    }

}
