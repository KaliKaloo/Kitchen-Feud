using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Appliance : Interactable
{
    [SerializeField] public int kitchenNum;

    GameObject clickedObj;
    private GameObject inputObj;
    public List<IngredientSO> itemsOnTheAppliance = new List<IngredientSO>();
    bool foundMatchingDish = false;
    public DishSO foundDish;
    public Dish dishOfFoundDish;
    public GameObject canvas;
    public GameObject minigameCanvas;
    public Camera UIcamera;
    public GameObject cookedDish;
    private GameObject cookedDishLocal;

    public bool isBeingInteractedWith = false;
    private Renderer r;
    public PlayerController playerController;
    public Rigidbody playerRigidbody;
    public SlotsController SlotsController;
    public int dishPoints;

    public bool canUse = true;


    public PhotonView pv;
    public PhotonView myPv;
    private void Start()
    {
        //minigameCanvas.SetActive(false);
        myPv = GetComponent<PhotonView>();
    }

    public override void Interact()
    {
        // checks whether player has been assigned yet
        if (player != null)
        {
            PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
            playerRigidbody = player.GetComponent<Rigidbody>();
            SlotsController = gameObject.GetComponent<SlotsController>();
            //view control
            pv = player.GetComponent<PhotonView>();

            //EVENT SYSTEM: LISTEN FROM AN EVENT (assignPoints) IN THE COOKINGBAR, IT CALLS UpdateDishPoints()
            if (!isBeingInteractedWith && canUse)
            {
                if (pv.IsMine)
                {
                    if (player.transform.Find("slot").childCount != 0)
                    {
                        this.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.All, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                            player.GetComponent<PhotonView>().ViewID);
                    }
                    else
                    {
                        cookDish();
                    }
                }
            }
            else
            {
                Debug.Log("Appliance in use");
            }
        }
    }
    public void cookDish()
    {
        if (itemsOnTheAppliance.Count != 0)
        {
            checkForDish();
            if (foundMatchingDish)
            {

                Debug.Log("Recipe found: " + foundDish.name + " - " + foundDish.dishID);


                this.GetComponent<PhotonView>().RPC("SetToTrue", RpcTarget.All, this.GetComponent<PhotonView>().ViewID);

                if (this.gameObject.tag == "Oven")
                {
                    if (this.name == "Oven1")
                    {
                        minigameCanvas = PhotonNetwork.Instantiate(Path.Combine("Canvas", "ovencanvas"), transform.GetChild(0).position, Quaternion.Euler(0, 90, 0));
                    }
                    else
                    {
                        minigameCanvas = PhotonNetwork.Instantiate(Path.Combine("Canvas", "ovencanvas"), transform.GetChild(0).position, Quaternion.identity);
                    }
                    myPv.RPC("setParent", RpcTarget.All, minigameCanvas.GetComponent<PhotonView>().ViewID, myPv.ViewID);
                    
                    cookedDishLocal = PhotonNetwork.Instantiate(Path.Combine( "DishPrefabs", foundDish.Prefab.name), transform.GetChild(0).position, transform.rotation);
                }
                else
                {
                    if (kitchenNum == 1)
                        minigameCanvas.tag = "Team1";
                    else if (kitchenNum == 2)
                        minigameCanvas.tag = "Team2";

                    canvas.gameObject.SetActive(false);
                    minigameCanvas.gameObject.SetActive(true);
                    
                   

                    playerController = player.GetComponent<PlayerController>();
                    playerController.enabled = false;
                    player.GetComponentInChildren<playerMvmt>().enabled = false;
                    UIcamera.enabled = true;
                    player.GetComponentInChildren<Camera>().enabled = false;
                    
                    player.GetComponent<PhotonView>().RPC("DisablePushing", RpcTarget.Others, player.GetComponent<PhotonView>().ViewID);
                    playerRigidbody.isKinematic = true;
                    cookedDishLocal = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", foundDish.Prefab.name), transform.TransformPoint(0, 1, 0), transform.rotation);
                }


                myPv.RPC("cookedDishG", RpcTarget.All, myPv.ViewID, cookedDishLocal.GetComponent<PhotonView>().ViewID);
                Rigidbody dishRigidbody = cookedDish.GetComponent<Rigidbody>();
                
                //setting gravity of cookedDish
                cookedDish.GetComponent<PhotonView>().RPC("SetGrav", RpcTarget.Others);
                dishRigidbody.useGravity = true;
                r = cookedDish.GetComponent<Renderer>();
                cookedDish.GetComponent<PhotonView>().RPC("DisableView", RpcTarget.Others);
                r.enabled = false;
                myPv.RPC("doFd", RpcTarget.All, myPv.ViewID, cookedDish.GetComponent<PhotonView>().ViewID);

                //delete the items the dish was cooked from
                //this.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.Others, this.GetComponent<PhotonView>().ViewID);
                itemsOnTheAppliance.Clear();
                SlotsController.ClearAppliance();


            }
            else
            {
                Debug.Log("Ingredients given do not make a dish");
            }
        }

    }

    public void addItem(GameObject heldObjArg, PlayerHolding playerHold)
    {
        if (heldObjArg.GetComponent<IngredientItem>())
        {
            IngredientItem heldObjArgItem = heldObjArg.GetComponent<IngredientItem>();
            itemsOnTheAppliance.Add(heldObjArgItem.item);


            if (player.transform.Find("slot").childCount!= 0 && playerHold.GetComponent<PhotonView>().IsMine)
            {
                SlotsController.PutOnAppliance(heldObjArg);

            }
        }
        else { Debug.Log("Can't put a cooked dish in a appliance."); }

    }

    public void checkForDish()
    {
        foundDish = Database.GetDishFromIngredients(itemsOnTheAppliance);
        

        if (foundDish != null)
        {
            string applianceName = gameObject.tag;
            string howToCook = foundDish.toCook;
            
            if (applianceName == howToCook){
                foundMatchingDish = true;
            }
        }
        else
        {
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
    void addItemRPC(int viewID, int viewID1)
    {
        addItem(PhotonView.Find(viewID).gameObject, PhotonView.Find(viewID1).gameObject.GetComponent<PlayerHolding>());
    }
   
    [PunRPC]
    void ovenGame(int viewID, int stoveID)
    {
        PhotonView.Find(viewID).gameObject.SetActive(true);
        PhotonView.Find(viewID).transform.position = PhotonView.Find(stoveID).transform.GetChild(0).position;
    }
    [PunRPC]
    void doFd(int viewID, int dishID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().dishOfFoundDish =
            PhotonView.Find(dishID).GetComponent<Dish>();

    }
    [PunRPC]
    void cookedDishG(int viewID, int dishID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().cookedDish =
            PhotonView.Find(dishID).gameObject;
    }
    [PunRPC]
    void syncSmoke(int viewID)
    {
        ParticleSystem PS = PhotonView.Find(viewID).gameObject.GetComponentInChildren<ParticleSystem>();
        PS.Play();
        
    }
    [PunRPC]
    void setParent(int canvasID,int ovenID)
    {
        PhotonView.Find(canvasID).transform.SetParent(PhotonView.Find(ovenID).transform);
    }
    [PunRPC]
    void setPlayer(int viewiD,int playerID)
    {
        PhotonView.Find(viewiD).GetComponent<Appliance>().player = PhotonView.Find(playerID).transform;
    }
}

