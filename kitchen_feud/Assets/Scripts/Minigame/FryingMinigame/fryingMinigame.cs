using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun; 
using UnityEngine.U2D;

public class fryingMinigame : MonoBehaviour
{
    public Slider slider;
    public FriedFoodController friedFoodController;
    private Appliance appliance;
    public ExitFryingMinigame backbutton;
    public PanController pan;

    public SpriteAtlas imgAtlas;
    public string spriteName;


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
                    spriteName = GetSpriteName(friedFoodController.dishSO);           
                    //friedFoodController.GetComponent<SpriteRenderer>().sprite = friedFoodController.dishSO.img;
                    //to check
                    friedFoodController.GetComponent<SpriteRenderer>().sprite = imgAtlas.GetSprite(spriteName);
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
    public string GetSpriteName(DishSO dishSO) {
        if(dishSO.dishID == "DI1415") return "patty";
        if(dishSO.dishID == "DI1621") return "rice";
        if(dishSO.dishID == "DI1316") return "13";
        if(dishSO.dishID == "DI1617") return "eggFried";
        else return "pngegg";
    }

}
