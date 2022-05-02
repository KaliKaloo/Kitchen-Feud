using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticleControl : MonoBehaviour
{
    public List<GameObject> fireSlots = new List<GameObject>();
    public GameObject firePrefab;
    public ParticleSystem sprinklers;
    ParticleSystem firePS;
    Transform disableAppliance;
    GameObject fire;


    private static GlobalTimer timer = new GlobalTimer();
    private int halfTime;
    double sprinklerRandomTime;
    float firstFireRandomTime;
    float secondFireRandomTime;
    bool randomFire = false;

    void Start()
    {
        halfTime = timer.GetTotalTime()/2;
        //psParent = GetComponent<ParticleSystem>();
        sprinklerRandomTime = Random.Range(halfTime,0);
        firstFireRandomTime = Random.Range(halfTime,0);
        secondFireRandomTime = Random.Range(halfTime,0);
    }

    void Update(){
        int currentTime = timer.GetTime();

        if (firePS){
            if(firePS.isPlaying == false){
                randomFire = false;
                disableAppliance.GetComponent<Appliance>().isBeingInteractedWith = false;
                Debug.Log("appliance abled");
                Destroy(fire);
            }
        }

        if ((currentTime == firstFireRandomTime) || (currentTime == secondFireRandomTime)){
            Debug.Log("random event");
            if (!randomFire){
                starRandomFire();    
            }
        }

        if (currentTime == sprinklerRandomTime){
            sprinklers.Play();
        }
    }

    public void starRandomFire(){
        GameObject slot = fireSlots[Random.Range(0, fireSlots.Capacity)];
        Debug.Log(slot.name);

        disableAppliance = slot.transform.parent;
        
        fire = Instantiate(firePrefab);
        fire.transform.SetParent(slot.transform);
        fire.transform.position = slot.transform.position;
        firePS = fire.GetComponent<ParticleSystem>();
        firePS.Play();

        if (firePS.isPlaying){
            randomFire = true;
            disableAppliance.GetComponent<Appliance>().isBeingInteractedWith = true;
        }
    }   
}
