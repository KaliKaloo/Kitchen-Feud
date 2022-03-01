using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Appliance : Interactable
{
    GameObject clickedObj;
    public GameObject inputObj;
    public List<IngredientSO> itemsOnTheAppliance = new List<IngredientSO>();
    bool foundMatchingDish = false;
    public DishSO foundDish;
    public Dish dishOfFoundDish;
    public GameObject canvas;
    public GameObject minigameCanvas;
    public GameObject cookedDish;
  
    public bool isBeingInteractedWith = false;
    private Renderer r;
    public PlayerController playerController;
    public Rigidbody playerRigidbody; 
    private SlotsController SlotsController;
    private int dishPoints;

    public int minigameNum;

    public override void Interact(){
 	    PlayerHolding playerHold = player.GetComponent<PlayerHolding>();

        if(gameObject.GetComponent<stoveMinigame>()){
            minigameNum = 0;
        }else if(gameObject.GetComponent<cuttingMinigame>()){
            minigameNum = 1;
        }


        playerRigidbody = player.GetComponent<Rigidbody>(); 
        //stoveSlotsController = this.GetComponent<StoveSlotsController>();
        SlotsController = gameObject.GetComponent<SlotsController>();
        //view control
        PhotonView pv = player.GetComponent<PhotonView>();

        //EVENT SYSTEM: LISTEN FROM AN EVENT (assignPoints) IN THE COOKINGBAR, IT CALLS UpdateDishPoints()
        if (!isBeingInteractedWith) {
            if(playerHold.items.Count!=0){
                this.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.All, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                    player.GetComponent<PhotonView>().ViewID);
                //addItem(playerHold.heldObj, playerHold);
            }else{
                //view control
                if(pv.IsMine) {
                    cookDish();
                }
            }
        }
    }
    public void cookDish(){
        if(itemsOnTheAppliance.Count != 0){
            checkForDish();
            if(foundMatchingDish){

                Debug.Log("Recipe found: "+foundDish.name + " - "+ foundDish.dishID);

                
                this.GetComponent<PhotonView>().RPC("SetToTrue", RpcTarget.Others);
                isBeingInteractedWith = true;
                
                //open the minigame canvas
                canvas.gameObject.SetActive(false);
                minigameCanvas.gameObject.SetActive(true);

                playerController = player.GetComponent<PlayerController>();
                playerController.enabled = false;

                player.GetComponent<PhotonView>().RPC("DisablePushing", RpcTarget.Others,player.GetComponent<PhotonView>().ViewID);
                playerRigidbody.isKinematic = true;

                //instantiate the cooked dish
                cookedDish = PhotonNetwork.Instantiate(foundDish.Prefab.name, transform.TransformPoint(0,1,0),transform.rotation);
                Rigidbody dishRigidbody = cookedDish.GetComponent<Rigidbody>();
                //setting gravity of cookedDish
                cookedDish.GetComponent<PhotonView>().RPC("SetGrav", RpcTarget.Others);
                dishRigidbody.useGravity = true; 
                r = cookedDish.GetComponent<Renderer>();
                cookedDish.GetComponent<PhotonView>().RPC("DisableView", RpcTarget.Others);
                r.enabled = false;
                dishOfFoundDish = cookedDish.GetComponent<Dish>();

                //delete the items the dish was cooked from
                this.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.Others);
                itemsOnTheAppliance.Clear();
                SlotsController.ClearAppliance();


            }
            else{
                Debug.Log("Ingredients given do not make a dish");
            }
        }
        
	}

    public void addItem(GameObject heldObjArg, PlayerHolding playerHold) {
        if (heldObjArg.GetComponent<IngredientItem>())
        {
            IngredientItem heldObjArgItem = heldObjArg.GetComponent<IngredientItem>();
            itemsOnTheAppliance.Add(heldObjArgItem.item);
            
            playerHold.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.All, playerHold.GetComponent<PhotonView>().ViewID);
            
            if (playerHold.items.Count == 0)
            {
                SlotsController.PutOnAppliance(heldObjArg, playerHold);
            }
        }
        else { Debug.Log("Can't put a cooked dish in a appliance."); }

    }

    public void checkForDish(){
        foundDish = Database.GetDishFromIngredients(itemsOnTheAppliance);
        if(foundDish != null){
            foundMatchingDish = true;
        }
        else{
            foundMatchingDish = false;
        }
    }

      //CALLED BY THE EVENT SYSTEM
    // public void UpdateDishPoints() {
    //     if(dishOfFoundDish != null){
            
    //         dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, dishPoints);
    //         dishOfFoundDish.points = dishPoints;
    //         Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
    //     }
    //     else{
    //         Debug.Log("dishoffounddish is null");
    //     }
        
    // }

   

    [PunRPC]
    void SetToTrue()
    {
        this.isBeingInteractedWith = true;
    }
    [PunRPC]
    void SetToFalse()
    {
        this.isBeingInteractedWith = false;
    }
  
    [PunRPC]
    void addItemRPC(int viewID,int viewID1)
    {
        addItem(PhotonView.Find(viewID).gameObject, PhotonView.Find(viewID1).gameObject.GetComponent<PlayerHolding>());
    }
    [PunRPC]
    void clearItems()
    {
        itemsOnTheAppliance.Clear();
    }

}
