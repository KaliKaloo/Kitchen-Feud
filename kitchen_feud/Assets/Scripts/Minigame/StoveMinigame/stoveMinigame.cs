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
    public Exit backbutton;
    // Start is called before the first frame update
    void Start()
    {
        appliance = GetComponent<Appliance>();
        cookingBar = slider.GetComponent<CookingBar>();
        slider.value = -30;
        cookingBar.keyHeld = false;
        cookingBar.done = false;
        backbutton.appliance = GetComponent<Appliance>();
        GameEvents.current.assignPoints += UpdateDishPoints;

    }

     //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPoints() {
        Dish dishOfFoundDish = appliance.dishOfFoundDish;
        if(dishOfFoundDish != null){
            dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)cookingBar.cookedLevel);
            dishOfFoundDish.points = (int)cookingBar.cookedLevel;
            Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
        }
        else{
            Debug.Log("dishoffounddish is null");
        }
        
    }

}
