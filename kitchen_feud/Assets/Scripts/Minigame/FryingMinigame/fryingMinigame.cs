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
    public GameObject gameCanvas;
    


    void Start()
    {
        movementStopped = false;    
        set = false;
        appliance = GetComponent<Appliance>();
    }

    void Update(){

     

        if (gameCanvas && set == false)
        {

   
            MusicManager.instance.minigameSwitch();
            MusicManager.instance.inMG = true;
            
            slider = gameCanvas.GetComponentInChildren<Slider>();
            backbutton = gameCanvas.GetComponentInChildren<ExitFryingMinigame>();

            set = true;

            friedFoodController.GetComponent<Image>().sprite = imgAtlas.GetSprite(friedFoodController.dishSO.dishID);
      

    
            
        }else if (!gameCanvas) {
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

                        
                    }
                }
            }
        }
    }
    
   public void UpdateDishPointsFrying() {
        if (appliance.isBeingInteractedWith){
            Dish dishOfFoundDish = appliance.dishOfFoundDish;
            if(dishOfFoundDish != null){
                
                int ingredientMultiplier = appliance.foundDish.recipe.Count - 1;
                
                dishOfFoundDish.points = (int) plate.totalPoints *5 + (30 * ingredientMultiplier);
                dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)dishOfFoundDish.points);
                Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
            }
        }
    }

}
