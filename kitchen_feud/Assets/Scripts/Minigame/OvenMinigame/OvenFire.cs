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
    private bool hasParent, reacted = false;
    public int team;

    void Start()
    {
        
    }

    void Update()
    {
        if (!hasParent && transform.parent)
        {
            PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
            fireSound = transform.parent.GetComponentInChildren<AudioSource>();
            team = transform.parent.GetComponent<Appliance>().kitchenNum;
            hasParent = true;
        }
        //and condition if temperature is too high
        if(!startFire){

         
            if(timer.timer < 35){ //cahnge back later
                foreach(ParticleSystem p in PS){
                    if(p.GetComponent<FireOut>()){
                        fireOut = p.GetComponent<FireOut>();
                        fireOut.resetEmission();
                        fireOut.enabled=true;
                        reacted=false;
                    }
                    p.Play();
                }
                //SOUND -------------------------------------------
                fireSound.gameObject.GetComponent<PhotonView>().RPC("PlayFireSound", RpcTarget.All, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
                //-------------------------------------------------
                startFire = true;

                   

            }
            //dynamic music reaction
            PlayerVoiceManager playerVM =  GameObject.Find("Local").GetComponentInChildren<PlayerVoiceManager>();
            if (startFire && !reacted && playerVM.entered1 ){
                MusicManager.instance.musicReact();
                reacted = true;
                fireOut.stoppedReaction = false;
            }
        }
    }

}
