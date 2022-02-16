using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

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


    public CookingBar cookingBar;
    public override void Interact(){
 	    PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        if(playerHold.items.Count!=0){
            addItem(playerHold.heldObj, playerHold);
        }else{
            cookDish();
        }
    }
    public void cookDish(){
        //popup of the 2d game or popup UI
        //lock the stove! One player interacting with it at a time
        //lock player movement when in minigame
        //after finishing game, instantiate new gameobject dish
        checkForDish();
        if(foundMatchingDish){
            // display the type of dish found
            // display a "cook <dish name>" ui button 
            Debug.Log("Recipe found: "+foundDish.name + " - "+ foundDish.dishID);

            //EnterScene("stoveMinigame");
            canvas.gameObject.SetActive(false);
            minigameCanvas.gameObject.SetActive(true);

            //the problem
            //foundDish.finalScore = (int) cookingBar.cookedLevel;
            //dishOfFoundDish = foundDish.Prefab.GetComponent<Dish>();
            //dishOfFoundDish.points = 

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
}
