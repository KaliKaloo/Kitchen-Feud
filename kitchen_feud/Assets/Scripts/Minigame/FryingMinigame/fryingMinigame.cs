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
    public Plate plate;
    public bool set;
    public SpriteAtlas imgAtlas;
    public string spriteName;
    bool pointsadded;
    


    void Start()
    {
        set = false;
        //GameEvents.current.assignPoints += UpdateDishPointsFrying;
        pointsadded = false;    
        appliance = GetComponent<Appliance>();
    }

    void Update(){

        if (transform.Find("Frying(Clone)") && transform.Find("Frying(Clone)").gameObject.activeSelf)
        {
            Debug.LogError("??");
            GameObject.Find("Local").GetComponentInChildren<playerMvmt>().enabled = false;
        }
        else 
        {
            if (GameObject.Find("Local")){
                GameObject.Find("Local").GetComponentInChildren<playerMvmt>().enabled = true;
            }
        }

        if (transform.Find("Frying(Clone)") && set == false && GameObject.Find("Pancake(Clone)"))
        {
            GameObject canv = transform.Find("Frying(Clone)").gameObject;
            slider = canv.GetComponentInChildren<Slider>();
            backbutton = canv.GetComponentInChildren<ExitFryingMinigame>();
            plate = canv.GetComponentInChildren<Plate>();
            pan = canv.GetComponentInChildren<PanController>();
            friedFoodController = GameObject.Find("Pancake(Clone)").GetComponent<FriedFoodController>();
            friedFoodController.dishSO = appliance.foundDish;
            friedFoodController.GetComponent<RawImage>().texture = imgAtlas.GetSprite(GetSpriteName(friedFoodController.dishSO)).texture;

            set = true;
        }else if (!transform.Find("Frying(Clone)")) {
            set = false;    
        }

        if (transform.Find("Frying(Clone)") && appliance.isBeingInteractedWith){
            
            //if (appliance.player && appliance.player.GetComponent<PhotonView>().IsMine)
            {
                if (backbutton) { 
                backbutton.appliance = GetComponent<Appliance>();
                    if (appliance.foundDish != null)
                    {
                        friedFoodController = pan.friedFood;
                        if (friedFoodController)
                        {
                            friedFoodController.dishSO = appliance.foundDish;
                            friedFoodController.appliance = appliance;

                            spriteName = GetSpriteName(friedFoodController.dishSO);
                            friedFoodController.GetComponent<RawImage>().texture = imgAtlas.GetSprite(GetSpriteName(friedFoodController.dishSO)).texture;
                            
                        }
                    }
                }
            }
        }
    }
   public void UpdateDishPointsFrying() {
       // Debug.Log("Outside");
//        Debug.LogError("Inside Function: "+ appliance.isBeingInteractedWith);
        if (appliance.isBeingInteractedWith){
            Debug.Log("Inside");
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
            dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)plate.totalPoints);
            //dishOfFoundDish.points = (int)friedFoodController.points;
            dishOfFoundDish.points = (int) plate.totalPoints;
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
