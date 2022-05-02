// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SprinklersTrigger : MonoBehaviour
// {
//     public ParticleSystem[] PS;
//     ParticleSystem p;
//     [SerializeField] private float collisionRate;
//     public float currentCollisions;
//     public float triggerLevel;
//     int numberTriggers = 1;
//     // Start is called before the first frame update

//     void Start()
//     {
//         PS = GetComponentsInChildren<ParticleSystem>();
//     }

//     void Update(){
//         if(currentCollisions == (triggerLevel*numberTriggers)){
//                 Debug.Log("play");
//                 foreach (ParticleSystem p in PS){
//                     p.Play();
//                 }

//             numberTriggers++;
//         }
//     }

//     private void OnParticleCollision(GameObject other)
//     {
//         Debug.Log(triggerLevel*numberTriggers);
//         if (other.tag == "Smoke")
//         {
//             currentCollisions += collisionRate;
//         }
//     }
// }

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

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Smoke")
        {
            currentCollisions += collisionRate;
            if(currentCollisions == triggerLevel*numberTriggers){
                PS.Play();
               numberTriggers++;
            }
        }
    }
}
