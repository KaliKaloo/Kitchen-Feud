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
    public bool isBeingInteractedWith = false;
    public Renderer r;
    public PlayerController playerController;
    public Rigidbody playerRigidbody; 
    public StoveSlotsController stoveSlotsController;

    public override void Interact(){
 	    PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        playerRigidbody = player.GetComponent<Rigidbody>(); 
        stoveSlotsController = this.GetComponent<StoveSlotsController>();

        //view control
        PhotonView pv = player.GetComponent<PhotonView>();
        cookingBar = slider.GetComponent<CookingBar>();

        //EVENT SYSTEM: LISTEN FROM AN EVENT (assignPoints) IN THE COOKINGBAR, IT CALLS UpdateDishPoints()
        if (!isBeingInteractedWith) {
            if(playerHold.items.Count!=0){
                this.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.All, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                    player.GetComponent<PhotonView>().ViewID);
                //addItem(playerHold.heldObj, playerHold);
            }else{
                //view control
                if(pv.IsMine) {
                    GameEvents.current.assignPoints += UpdateDishPoints;
                    cookDish();
                }
            }
        }
    }
    public void cookDish(){
        if(itemsOnTheStove.Count != 0){
            checkForDish();
            if(foundMatchingDish){

                Debug.Log("Recipe found: "+foundDish.name + " - "+ foundDish.dishID);

                //open the minigame canvas
                slider.value = -30;
                cookingBar.keyHeld = false;
                cookingBar.done = false;
                this.GetComponent<PhotonView>().RPC("SetToTrue", RpcTarget.Others);
                isBeingInteractedWith = true;
                
                canvas.gameObject.SetActive(false);
                minigameCanvas.gameObject.SetActive(true);

                playerController = player.GetComponent<PlayerController>();
                playerController.enabled = false;

                player.GetComponent<PhotonView>().RPC("DisablePushing", RpcTarget.Others,player.GetComponent<PhotonView>().ViewID);
                playerRigidbody.isKinematic = true;
            
                //playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
                

                //the position the dish will be instantiated at
                Vector3 playerPosition = player.transform.position;
                Vector3 offset = new Vector3(0,1f,0);
                

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
                itemsOnTheStove.Clear();
                stoveSlotsController.ClearStove();


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
            itemsOnTheStove.Add(heldObjArgItem.item);
            
            playerHold.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.All, playerHold.GetComponent<PhotonView>().ViewID);
            
            //Destroy(heldObjArg, 4.0f);
            if (playerHold.items.Count == 0)
            {
                
                //Destroy(heldObjArg);
                //throws a nullref, idk why
                stoveSlotsController.PutOnStove(heldObjArg, playerHold);

            }
       
        }
        else { Debug.Log("Can't put a cooked dish in a stove."); }

    }

    public void checkForDish(){
        foundDish = Database.GetDishFromIngredients(itemsOnTheStove);
        if(foundDish != null){
            foundMatchingDish = true;
        }
        else{
            foundMatchingDish = false;
        }
    }

    //CALLED BY THE EVENT SYSTEM
    public void UpdateDishPoints() {
        if(dishOfFoundDish != null){
            dishOfFoundDish.GetComponent<PhotonView>().RPC("pointSync", RpcTarget.Others, (int)cookingBar.cookedLevel);
            dishOfFoundDish.points = cookingBar.cookedLevel;
            Debug.Log("UpdateDishPoints: " + dishOfFoundDish.points);
        }
        else{
            Debug.Log("dishoffounddish is null");
        }
        
    }
    [PunRPC]
    void SetToTrue(bool isEnabled)
    {
        isEnabled = true;
    }
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
        itemsOnTheStove.Clear();
    }

}
