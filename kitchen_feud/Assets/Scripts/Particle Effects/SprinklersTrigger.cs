using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklersTrigger : MonoBehaviour
{
    ParticleSystem PS;
    [SerializeField] private float collisionRate;
    public float currentCollisions;
    public float triggerLevel;
    int numberTriggers = 1;
    // Start is called before the first frame update

    void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    void Update(){
        if(currentCollisions == (triggerLevel*numberTriggers)){
               PS.Play();

            numberTriggers++;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Smoke")
        {
            currentCollisions += collisionRate;
        }
    }
}
