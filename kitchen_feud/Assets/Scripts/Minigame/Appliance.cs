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
    private GameObject inputObj;
    public List<IngredientSO> itemsOnTheAppliance = new List<IngredientSO>();
    bool foundMatchingDish = false;
    public DishSO foundDish;
    public Dish dishOfFoundDish;
    public GameObject canvas;
    public GameObject minigameCanvas;
    public GameObject cookedDish;
    private GameObject cookedDishLocal;
  
    public bool isBeingInteractedWith = false;
    private Renderer r;
    public PlayerController playerController;
    private Rigidbody playerRigidbody; 
    private SlotsController SlotsController;
    public int dishPoints;

    
    public PhotonView pv;
    public PhotonView myPv;
    private void Start()
    {
        minigameCanvas.SetActive(false);
        myPv = GetComponent<PhotonView>();
    }
    

    public override void Interact(){
 	    PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        playerRigidbody = player.GetComponent<Rigidbody>(); 
        //stoveSlotsController = this.GetComponent<StoveSlotsController>();
        SlotsController = gameObject.GetComponent<SlotsController>();
        //view control
        pv = player.GetComponent<PhotonView>();

        //EVENT SYSTEM: LISTEN FROM AN EVENT (assignPoints) IN THE COOKINGBAR, IT CALLS UpdateDishPoints()
        if (!isBeingInteractedWith) {
            if(pv.IsMine) {
                if(playerHold.items.Count!=0){
                    this.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.All, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                        player.GetComponent<PhotonView>().ViewID);
                }else{
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

                
                this.GetComponent<PhotonView>().RPC("SetToTrue", RpcTarget.All,this.GetComponent<PhotonView>().ViewID);
                //Debug.LogError(isBeingInteractedWith);
                //isBeingInteractedWith = true;

                //open the minigame canvas

                if (this.gameObject.tag == "Oven")
                {
                    //minigameCanvas.gameObject.SetActive(true);
                    //minigameCanvas.transform.parent = this.transform.GetChild(0);
                    minigameCanvas.transform.position = transform.GetChild(0).position;
                    myPv.RPC("ovenGame", RpcTarget.All,
                        minigameCanvas.GetComponent<PhotonView>().ViewID,
                        myPv.ViewID);
                    cookedDishLocal = PhotonNetwork.Instantiate(foundDish.Prefab.name, player.transform.GetChild(2).position, transform.rotation);
                    //Rigidbody dishRigidbody = cookedDish.GetComponent<Rigidbody>();
                }
                else
                {
                    canvas.gameObject.SetActive(false);
                    minigameCanvas.gameObject.SetActive(true);
                    player.GetComponent<PhotonView>().RPC("DisablePushing", RpcTarget.Others, player.GetComponent<PhotonView>().ViewID);
                    playerRigidbody.isKinematic = true;
                    cookedDishLocal = PhotonNetwork.Instantiate(foundDish.Prefab.name, transform.TransformPoint(0, 1, 0), transform.rotation);




                }


                //cookedDish = cookedDishLocal;
                myPv.RPC("cookedDishG", RpcTarget.All, myPv.ViewID, cookedDishLocal.GetComponent<PhotonView>().ViewID);

                //instantiate the cooked dish

                //setting gravity of cookedDish
                
                Rigidbody dishRigidbody = cookedDish.GetComponent<Rigidbody>();
                cookedDish.GetComponent<PhotonView>().RPC("SetGrav", RpcTarget.Others);
                dishRigidbody.useGravity = true; 
                r = cookedDish.GetComponent<Renderer>();
                cookedDish.GetComponent<PhotonView>().RPC("DisableView", RpcTarget.Others);
                r.enabled = false;

                myPv.RPC("doFd", RpcTarget.All, myPv.ViewID, cookedDish.GetComponent<PhotonView>().ViewID);
                //dishOfFoundDish = cookedDish.GetComponent<Dish>();

                //delete the items the dish was cooked from
                this.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.Others,this.GetComponent<PhotonView>().ViewID);
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
            
            if (playerHold.items.Count == 0 && playerHold.GetComponent<PhotonView>().IsMine)
            {
                SlotsController.PutOnAppliance(heldObjArg);
            }
        }
        else { Debug.Log("Can't put a cooked dish in a appliance."); }

    }

    public void checkForDish(){
        foundDish = Database.GetDishFromIngredients(itemsOnTheAppliance);
        Debug.LogError("This is the found Dish " + foundDish);
        string applianceName = gameObject.tag;
        Debug.LogError(foundDish.toCook);
        string howToCook = foundDish.toCook;

        //if (foundDish != null){
        if ((foundDish != null) && (applianceName == howToCook)){
            foundMatchingDish = true;
        }
        else{
            foundMatchingDish = false;
        }
    }


    

    [PunRPC]
    void SetToTrue(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().isBeingInteractedWith = true;
    }
    [PunRPC]
    void SetToFalse(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().isBeingInteractedWith = false;
    }
  
    [PunRPC]
    void addItemRPC(int viewID,int viewID1)
    {
        addItem(PhotonView.Find(viewID).gameObject, PhotonView.Find(viewID1).gameObject.GetComponent<PlayerHolding>());
    }
    [PunRPC]
    void clearItems(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().itemsOnTheAppliance.Clear();
    }
    [PunRPC]
    void ovenGame(int viewID,int stoveID)
    {
        PhotonView.Find(viewID).gameObject.SetActive(true);
        PhotonView.Find(viewID).transform.position = PhotonView.Find(stoveID).transform.GetChild(0).position;
    }
    [PunRPC]
    void doFd(int viewID, int dishID) {
        PhotonView.Find(viewID).GetComponent<Appliance>().dishOfFoundDish =
            PhotonView.Find(dishID).GetComponent<Dish>();

    }
    [PunRPC]
    void cookedDishG(int viewID, int dishID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().cookedDish =
            PhotonView.Find(dishID).gameObject;
    }
}
