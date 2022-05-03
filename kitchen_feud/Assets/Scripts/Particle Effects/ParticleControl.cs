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
    public int team;


    private static GlobalTimer timer = new GlobalTimer();
    private int halfTime;
    double sprinklerRandomTime;
    float firstFireRandomTime;
    float secondFireRandomTime;
    bool randomFire = false;
    bool setTimes = false;
    public PhotonView PV;

    void Start()
    {
        halfTime = timer.GetTotalTime()/2;
        //psParent = GetComponent<ParticleSystem>();
        //sprinklerRandomTime = Random.Range(halfTime,0);
        //firstFireRandomTime = Random.Range(halfTime,0);
        //secondFireRandomTime = Random.Range(halfTime,0);
        PV = GetComponent<PhotonView>();
    }

    void Update(){
        int currentTime = timer.GetLocalTime();

        if (PhotonNetwork.IsMasterClient && !setTimes)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                PV.RPC("syncRandomTimes", RpcTarget.All, PV.ViewID,team);
                setTimes = true;
            }
        }

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
    [PunRPC]
    void syncRandomTimes(int ViewID,int team)
    {
        ParticleControl PC = PhotonView.Find(ViewID).GetComponent<ParticleControl>();
        if(team == 1)
        {
            //PC.sprinklerRandomTime = Random.Range(halfTime, 0);
            //PC.firstFireRandomTime = Random.Range(halfTime, 0);
            //PC.secondFireRandomTime = Random.Range(halfTime, 0);
            PC.sprinklerRandomTime = 290;
            PC.firstFireRandomTime = 280;
            PC.secondFireRandomTime = 270;
        }
        else if(team == 2)
        {
            //PC.sprinklerRandomTime = Random.Range(halfTime, 0);
            //PC.firstFireRandomTime = Random.Range(halfTime, 0);
            //PC.secondFireRandomTime = Random.Range(halfTime, 0);
            PC.sprinklerRandomTime = 280;
            PC.firstFireRandomTime = 290;
            PC.secondFireRandomTime = 270;
        }
 




    }
}
