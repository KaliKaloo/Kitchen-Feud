using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFire : MonoBehaviour
{
    Timer timer;
    bool startFire = false;
    Component[] PS;

    void Start()
    {
        timer = transform.GetComponent<Timer>();
        PS = transform.parent.GetComponentsInChildren<ParticleSystem>();
    }

    void Update()
    {
        if(!startFire){
            if(timer.timer < 0){
                foreach(ParticleSystem p in PS){
                    p.Play();
                }
                startFire = true;
            }
        }

    }
}
