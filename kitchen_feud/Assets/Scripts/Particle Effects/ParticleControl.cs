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
   public double sprinklerRandomTime;
    public float firstFireRandomTime;
    public float secondFireRandomTime;
    public bool randomFire = false;
    public bool setTimes = false;
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
        Debug.LogError("COUNTTT" + timer.GetLocalTime());
        int currentTime = timer.GetLocalTime();
        if(PhotonNetwork.CurrentRoom.PlayerCount == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            Debug.LogError("HUH?!?!");
        }
        if (PhotonNetwork.IsMasterClient && !setTimes  && GameObject.FindGameObjectsWithTag("Player").Length == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                if(team == 1)
                {
                    PV.RPC("syncRandomTimes", RpcTarget.All, PV.ViewID, team, Random.Range(halfTime, 0), Random.Range(halfTime, 0), Random.Range(halfTime, 0));

                }
                else if(team == 2)
                {
                    PV.RPC("syncRandomTimes", RpcTarget.All, PV.ViewID, team, Random.Range(halfTime, 0), Random.Range(halfTime, 0), Random.Range(halfTime, 0));

                }
               
                setTimes = true;
            }
        }

        if (firePS){
            if(firePS.isPlaying == false){
                randomFire = false;
                disableAppliance.GetComponent<Appliance>().isBeingInteractedWith = false;
                Destroy(fire);
            }
        }

        if ((currentTime == firstFireRandomTime) || (currentTime == secondFireRandomTime)){
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
    void syncRandomTimes(int ViewID,int team, int num1, int num2, int num3)
    {
        ParticleControl PC = PhotonView.Find(ViewID).GetComponent<ParticleControl>();

        PC.sprinklerRandomTime = num1;
        PC.firstFireRandomTime = num2;
        PC.secondFireRandomTime = num3;
    
 




    }
}
