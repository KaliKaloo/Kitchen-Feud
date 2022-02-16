using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stove : Interactable
{
    GameObject clickedObj;
    public GameObject inputObj;
    public List<IngredientSO> itemsOnTheStove = new List<IngredientSO>();
    bool foundMatchingDish = false;
    public DishSO foundDish;
    public Dish dishOfFoundDish;
    public GameObject canvas;
    public GameObject minigameCanvas;
    public GameObject cookedDish;
    public Slider slider;
    public CookingBar cookingBar;

    public override void Interact(){
 	    PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        PhotonView pv = PhotonView.Get(player);
        cookingBar = slider.GetComponent<CookingBar>();

        //MAKE A CUSTOM EVENT SYSTEM: LISTEN FROM AN EVENT (assignPoints) IN THE COOKINGBAR, IT CALLS UpdateDishPoints()
        GameEvents.current.assignPoints += UpdateDishPoints;
        //slider.onValueChanged.AddListener(delegate { UpdateDishPoints();});
        if(playerHold.items.Count!=0){
            addItem(playerHold.heldObj, playerHold);
        }else{
            //not sure if that's how and where it's done
            if(pv.IsMine) {
                cookDish();
            }
            
        }
    }
    public void cookDish(){
        checkForDish();
        if(foundMatchingDish){

            Debug.Log("Recipe found: "+foundDish.name + " - "+ foundDish.dishID);

            //open the minigame canvas
            canvas.gameObject.SetActive(false);
            minigameCanvas.gameObject.SetActive(true);

            //the position the dish will be instantiated at
            Vector3 playerPosition = player.transform.position;
            Vector3 offset = new Vector3(0.5f,0,0);

            //instantiate the cooked dish
            cookedDish = PhotonNetwork.Instantiate(foundDish.Prefab.name, playerPosition + offset, Quaternion.identity);
            dishOfFoundDish = cookedDish.GetComponent<Dish>();
            
            //cookingBar.UpdateDishPoints();
            Debug.Log(cookingBar.cookedLevel);
            //dishOfFoundDish.points = cookingBar.cookedLevel;
            Debug.Log(dishOfFoundDish.points);

            //delete the items the dish was cooked from
            itemsOnTheStove.Clear();

        }
        else{
            Debug.Log("Ingredients given do not make a dish");
        }
	}

    public void addItem(GameObject heldObjArg, PlayerHolding playerHold) {
        IngredientItem heldObjArgItem = heldObjArg.GetComponent<IngredientItem>();
        itemsOnTheStove.Add(heldObjArgItem.item);
		playerHold.dropItem(); 
        Destroy(heldObjArg);

    }

    public void checkForDish(){
        foundDish = Database.GetDishFromIngredients(itemsOnTheStove);
        if(foundDish != null)
            foundMatchingDish = true;
    }

    //sets back to 70 too! I think it counts the reset as a value change
    public void UpdateDishPoints() {
        //cookedLevel = SetCookedLevel(slider.value);
        dishOfFoundDish.points = cookingBar.cookedLevel;
        //Debug.Log(dishOfFoundDish.points);
    }
}
