using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun; 

public class fryingMinigame : MonoBehaviour
{
    public Slider slider;
    public FriedFoodController friedFoodController;
    private Appliance appliance;
    public ExitFryingMinigame backbutton;
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsFrying;
        appliance = GetComponent<Appliance>();
    }

    void Update(){
        if(appliance.isBeingInteractedWith){
            if (appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
            {
                backbutton.appliance = GetComponent<Appliance>();
            }
        }
    }
   public void UpdateDishPointsFrying() {
       // Debug.Log("Outside");
        Debug.LogError("Inside Function: "+ appliance.isBeingInteractedWith);
        if (appliance.isBeingInteractedWith){
            Debug.Log("Inside");
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
            dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)friedFoodController.points);
            dishOfFoundDish.points = (int)friedFoodController.points;
            Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
        }
        }
    }
}
