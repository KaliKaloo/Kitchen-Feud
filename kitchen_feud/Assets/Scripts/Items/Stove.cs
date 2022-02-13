using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

//or Interactable
public class Stove : Interactable
{
    public Stove stove;
    GameObject clickedObj;
    //PlayerHolding playerHold;
    //Item item;
    public GameObject inputObj;
    public List<pickableItem> itemsOnTheStove = new List<pickableItem>();

    public string workingID;

//questionable
    void Start()
	{
        if(PhotonNetwork.IsConnected) {
            isStove = true;
			// cam = Camera.main;
			//playerHold = GetComponent<PlayerHolding>();
            //stove = GameObject.Find("Stove 1").GetComponent<Stove>();
		}
	} 

    public void Cook() {
        //popup of the 2d game (new scene?)
        //get the ingredients: items on the stove list updated in the overloaded cooking(arg)
        //get the order
        //propagate that to the scene
        //lock the stove! One player interacting with it at a time
        //player has to have a function for interacting with a stove in which it gives it all the stove needs
        //+possibly also on player side switching to the scene
        //cooking in the game, return a new object + score (delete the objects put in)
        Debug.Log("Cooking!");
        EnterScene("stoveMinigame");
	}

    public void Cook(GameObject heldObjArg, PlayerHolding playerHold) {

        Debug.Log(heldObjArg + "is on the stove"); 
        pickableItem heldObjArgItem = heldObjArg.GetComponent<pickableItem>();
        itemsOnTheStove.Add(heldObjArgItem);
        //get ingredient id
        //IngredientSO ingredient = heldObjArg.GetComponent<IngredientSO>();
        IngredientSO ingredient = heldObjArg;
        if (ingredient == null) {
            Debug.Log("it's not an ingredient!");
        }
        else {
            if (workingID != null) {
                Debug.Log(Database.GetDishByID("DI" + workingID));
            }
            else {
                workingID = workingID + ingredient.ingredientID;
                Debug.Log(Database.GetDishByID("DI" + workingID));
            }  
        }

        Debug.Log(itemsOnTheStove);
        playerHold.dropItem();
        Destroy(heldObjArg);

    }

    public bool isStoveFunction(GameObject obj) {
        Stove stove = obj.GetComponent<Stove>();
        if (stove == null) {
            return false;
        }
        //null reference
        if(stove.isStove){
            clickedObj = obj;
            return true;
        } 
        else{
            return false;
        }
    }
}
