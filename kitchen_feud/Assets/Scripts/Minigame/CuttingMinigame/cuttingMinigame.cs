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
    public Exit backbutton;
    public cuttingkeypress k;

    // Start is called before the first frame update
    void Start()
    {
        appliance = GetComponent<Appliance>();
        backbutton.appliance = GetComponent<Appliance>();
        GameEvents.current.assignPoints += UpdateDishPoints;

    }

    
    public void UpdateDishPoints() {
        Dish dishOfFoundDish = appliance.dishOfFoundDish;
        if(dishOfFoundDish != null){
            dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, k.finalPoints );
            dishOfFoundDish.points = 10;
            Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
        }
        else{
            Debug.Log("dishoffounddish is null");
        }
        
    }
}