using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun; 

public class stoveMinigame : MonoBehaviour
{
    public Slider slider;
    public CookingBar cookingBar;
    private Appliance appliance;
    public ExitStoveMinigame backbutton;
    void Start()
    {
        GameEvents.current.assignPoints += UpdateDishPointsStove;
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


   public void UpdateDishPointsStove() {
        if (appliance.isBeingInteractedWith){
            Debug.Log("Inside");
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)cookingBar.cookedLevel);
                dishOfFoundDish.points = (int)cookingBar.cookedLevel;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
        }
    }
}
