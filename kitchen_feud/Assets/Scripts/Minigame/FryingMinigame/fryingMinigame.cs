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
    public PanController pan;
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
                if (appliance.foundDish != null) {
                    friedFoodController = pan.friedFood;
                    friedFoodController.dishSO = appliance.foundDish;
                    friedFoodController.appliance = appliance;
                    Debug.Log("dish asigned: " + friedFoodController.dishSO);            
                    friedFoodController.GetComponent<SpriteRenderer>().sprite = friedFoodController.dishSO.img;
                    Debug.Log("sprite assigned: " + friedFoodController.GetComponent<SpriteRenderer>().sprite);
                }
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
