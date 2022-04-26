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
    public bool movementStopped;
    


    void Start()
    {
        movementStopped = false;    
        set = false;
        appliance = GetComponent<Appliance>();
    }

    void Update(){

     

        if (transform.Find("Frying(Clone)") && set == false && GameObject.Find("Pancake(Clone)"))
        {
            GameObject canv = transform.Find("Frying(Clone)").gameObject;
            slider = canv.GetComponentInChildren<Slider>();
            backbutton = canv.GetComponentInChildren<ExitFryingMinigame>();
            plate = canv.GetComponentInChildren<Plate>();
            pan = canv.GetComponentInChildren<PanController>();
            friedFoodController = GameObject.Find("Pancake(Clone)").GetComponent<FriedFoodController>();
            friedFoodController.dishSO = appliance.foundDish;
            friedFoodController.GetComponent<Image>().sprite = imgAtlas.GetSprite(friedFoodController.dishSO.dishID);
            
            int canvasTag = appliance.kitchenNum;
            if (canvasTag == 1){
                GameObject otherTeam = GameObject.FindGameObjectWithTag("FryingBackgroung2");
                otherTeam.SetActive(false);
      
            } else if (canvasTag == 2){
                GameObject otherTeam = GameObject.FindGameObjectWithTag("FryingBackgroung1");
                otherTeam.SetActive(false);
              
            }

            set = true;
            MusicManagerOld.instance.minigameSwitch();
		    MusicManagerOld.instance.inMG = true;
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

//                        spriteName = friedFoodController.dishSO.dishID;
//                        friedFoodController.GetComponent<Image>().sprite = imgAtlas.GetSprite(spriteName);
                        
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

}
