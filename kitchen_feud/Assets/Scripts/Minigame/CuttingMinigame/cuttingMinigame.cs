using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class cuttingMinigame : MonoBehaviour
{
    private Appliance appliance;
    public ExitCuttingMinigame backbutton;
    public cuttingkeypress k;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsCutting;
        appliance = GetComponent<Appliance>();
    }
     void Update(){
        if(appliance.isBeingInteractedWith){
            backbutton.appliance = GetComponent<Appliance>();

        }
    }

   //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPointsCutting() {
        if(appliance.isBeingInteractedWith){
             Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
                
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, k.finalPoints);
                dishOfFoundDish.points = k.finalPoints;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
            else{
                Debug.Log("dishoffounddish is null");
            }
        }
       
    }

    
}