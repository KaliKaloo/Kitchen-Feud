using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class cuttingMinigame : MonoBehaviour
{
    public cutController CutController;
    private Appliance appliance;
    public ExitCuttingMinigame backbutton;
    //public int finalPoints;
    
    //public cuttingkeypress k;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsCutting;
        appliance = GetComponent<Appliance>();
        
    }
     void Update(){
        if(appliance.isBeingInteractedWith){
            
            if (appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
            {
                backbutton.appliance = GetComponent<Appliance>();

                if (appliance.foundDish != null)
                {
                    CutController.dish = appliance.foundDish;
                }
            }

        }
    }

 
    //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPointsCutting() {
        if(appliance.isBeingInteractedWith){
             Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
                
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, CutController.finalScore);
                dishOfFoundDish.points = CutController.finalScore;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
            else{
                Debug.Log("dishoffounddish is null");
            }
        }
       
    }

    
}