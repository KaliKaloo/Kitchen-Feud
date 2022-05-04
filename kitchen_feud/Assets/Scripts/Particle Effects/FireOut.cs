using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireOut : MonoBehaviour {
    private ParticleSystem[] PS;
    private ParticleSystem fireParticles;
    private ParticleSystem.EmissionModule fireEmission;
    private bool notFound = false;
    public bool stoppedReaction = false;
    private float currentEmission = 0;
    [SerializeField] private float fadeRate = 1;
    public AudioSource fireSound;


    void Start()
    {
        // Get Particle System and emission
        if (GetComponent<ParticleSystem>())
        {
            fireParticles = GetComponent<ParticleSystem>();
            fireEmission = fireParticles.emission;
            currentEmission = fireEmission.rateOverTime.constant;
            PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
        }
        else
        {
            Debug.Log("ERROR :: FireOut - Particle System not found on object: " + gameObject.name);
            notFound = true;
        }
    }

	void Update () {

        // if no particle system, do nothing
        if (notFound)
            return;
        // check if fire is extinguished
        if (currentEmission <= 0)
        {
            fireParticles.Stop();
            foreach(ParticleSystem p in PS){
                if(p.isPlaying){
                    
                    p.Stop();
                }
            }
<<<<<<< HEAD
            //SOUND -------------------------------------------
            fireSound.gameObject.GetComponent<PhotonView>().RPC("StopFireSound", RpcTarget.All, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
            //-------------------------------------------------
=======
            if (fireSound){
                 //SOUND -------------------------------------------
                fireSound.gameObject.GetComponent<PhotonView>().RPC("StopFireSound", RpcTarget.All, fireSound.gameObject.GetComponent<PhotonView>().ViewID);
                //-------------------------------------------------
            }
>>>>>>> dev
           
            this.enabled = false;

            //stop dynamic music reaction
            if(!stoppedReaction) {
                MusicManager.instance.endReaction();
                stoppedReaction = true;
            }
                 
        
        }
	}

    public void resetEmission(){
        fireEmission.rateOverTime = 10;
        currentEmission = fireEmission.rateOverTime.constant;

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Water")
        {
            currentEmission -= fadeRate;
            fireEmission.rateOverTime = currentEmission;
        }
    }
}
