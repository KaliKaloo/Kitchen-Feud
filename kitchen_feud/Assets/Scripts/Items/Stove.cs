using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;

public class Stove : Interactable
{
    GameObject clickedObj;
    public GameObject inputObj;
    public List<IngredientSO> itemsOnTheStove = new List<IngredientSO>();
    bool foundMatchingDish = false;
    public DishSO foundDish;
    public Dish dishOfFoundDish;

    //the canvases shouldn't be set through inspector, we should get them in the script
    //especially the minigameCanvas
    public GameObject canvas;
    public GameObject minigameCanvas;


    public GameObject cookedDish;
    public Slider slider;
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

            //this shouldn't work but it does lmao
            Vector3 playerPosition = player.transform.position;
            Vector3 offset = new Vector3(1,0,1);

            //create the cooked dish and spawn in the player's hand
            cookedDish = PhotonNetwork.Instantiate(foundDish.Prefab.name, playerPosition + offset, Quaternion.identity);
            dishOfFoundDish = cookedDish.GetComponent<Dish>();
            cookingBar = slider.GetComponent<CookingBar>();
            dishOfFoundDish.points = cookingBar.cookedLevel;

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
