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
    public int team;

    void Update()
    {
        
        if (!hasParent && transform.parent)
        {
            PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
            fireSound = transform.parent.GetComponentInChildren<AudioSource>();
            team = transform.parent.GetComponent<Appliance>().kitchenNum;
            hasParent = true;
        }

        //IF timer below -5, start fire
        if(!startFire && timer.timer < -5){
            foreach(ParticleSystem p in PS){
                if(p.GetComponent<FireOut>()){
                    fireOut = p.GetComponent<FireOut>();
                    fireOut.resetEmission();
                    fireOut.enabled=true;
                }
                p.Play();
            }
            if (fireSound){
                fireSound.gameObject.GetComponent<PhotonView>().RPC("PlayFireSound", RpcTarget.All, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
            }
            
            startFire = true;
           
        }

        //dynamic music reaction
        if (GameObject.Find("Local")){
            PlayerVoiceManager playerVM =  GameObject.Find("Local").GetComponentInChildren<PlayerVoiceManager>();
            if (startFire && (playerVM.entered1 && team == 1) || (playerVM.entered2 && team == 2)){
                MusicManager.instance.priorityPitch = true;
                MusicManager.instance.musicReact();
                foreach(ParticleSystem p in PS){
                    if(p.GetComponent<FireOut>()){
                        fireOut = p.GetComponent<FireOut>();
                        fireOut.stoppedReaction = false;
                    }
                }
            }
        }
    }

}
