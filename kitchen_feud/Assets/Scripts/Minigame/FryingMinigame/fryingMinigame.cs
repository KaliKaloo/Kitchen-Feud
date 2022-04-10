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
    public Image background;
    public Slider slider;
    public FriedFoodController friedFoodController;
    private Appliance appliance;
    public ExitFryingMinigame backbutton;
    public PanController pan;
    public Plate plate;
    public bool set;
    public SpriteAtlas imgAtlas;
    public string spriteName;
    public Camera UICamera;
    private int team;
    


    void Start()
    {
        set = false;
        appliance = GetComponent<Appliance>();
    }

    void Update(){

        if (transform.Find("Frying(Clone)") && transform.Find("Frying(Clone)").gameObject.activeSelf)
        {
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
            
            int canvasTag = appliance.kitchenNum;
            if (canvasTag == 1){
                GameObject[] otherTeam = GameObject.FindGameObjectsWithTag("Team2");
                foreach (GameObject obj in otherTeam){
                    obj.SetActive(false);
                }
            } else if (canvasTag == 2){
                GameObject[] otherTeam = GameObject.FindGameObjectsWithTag("Team1");
                foreach (GameObject obj in otherTeam){
                    obj.SetActive(false);
                }
            }

            set = true;
        }else if (!transform.Find("Frying(Clone)")) {
            set = false;    
        }

        if (transform.Find("Frying(Clone)") && appliance.isBeingInteractedWith){
            
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
    
   public void UpdateDishPointsFrying() {
        if (appliance.isBeingInteractedWith){
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)plate.totalPoints);
                dishOfFoundDish.points = (int) plate.totalPoints;
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
        }
    }
    public string GetSpriteName(DishSO dishSO) {
        if(dishSO.dishID == "DI1415") return "DI1415";
        if(dishSO.dishID == "DI1621") return "DI1621";
        if(dishSO.dishID == "DI1316") return "DI1316";
        if(dishSO.dishID == "DI1617") return "DI1617";
        else return "DI163134";
    }

}
