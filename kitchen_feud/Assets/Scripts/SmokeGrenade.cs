using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{
    public GameObject prefab;
    
    public float delay = 3f;

    float countdown;

    public GameObject smoke1;
    private ParticleSystem particle1;

    GameObject smokeClone;
    bool hasExploded = false;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown("1") && !started) {
            started = true;
            Throw();
         } else if (started && !hasExploded) {
             countdown -= Time.deltaTime;
         }


        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

        if (particle1 != null && hasExploded && started)
        {
            if (!particle1.isEmitting)
                Destroy(smokeClone);
        }
    }

    void Throw() {
        smokeClone = Instantiate(prefab, transform.position, transform.rotation);
        particle1 = smokeClone.transform.GetChild(0).GetComponent<ParticleSystem>();
        smokeClone.GetComponent<Rigidbody>().AddForce(Vector3.forward * 500); //Moving projectile
    }

    void Explode()
    {
        particle1.Play();
        started = false;
    }
}
