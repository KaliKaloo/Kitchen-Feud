using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{

    public float delay = 3f;

    float countdown;

    public GameObject smoke1;
    public AudioSource smokeSource;
    public AudioClip smokeSound;


    private ParticleSystem particle1;

    bool hasExploded = false;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        particle1 = smoke1.GetComponent<ParticleSystem>();

        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

        if (!particle1.isEmitting && hasExploded && started)
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        particle1.Play();
        smokeSource.Play();
        started = true;
    }
}
