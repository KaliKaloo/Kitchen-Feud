using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
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
    public GameObject minigameCanvas2;
    public Camera UIcamera;
    public GameObject cookedDish;
    private GameObject cookedDishLocal;
    public List<int> appliancePlayers;
    public bool isBeingInteractedWith = false;
    private Renderer r;
    public PlayerController playerController;
    public Rigidbody playerRigidbody;
    public SlotsController SlotsController;
    public playerMvmt playermoving;
    public int dishPoints;
    public bool added;
    public bool canUse = true;

    public PhotonView pv;
    public PhotonView myPv;
    private void Start()
    {
        added = false;
        myPv = GetComponent<PhotonView>();
        SlotsController = GetComponent<SlotsController>();
    }

    public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        playerRigidbody = player.GetComponent<Rigidbody>();
 
        pv = player.GetComponent<PhotonView>();
        if(myPv.Owner != GameObject.Find("Local").GetPhotonView().Owner)
        {
            PhotonView.Find(myPv.ViewID).TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
        }

        if (pv.IsMine)
        {
            if (!isBeingInteractedWith)
            {
                    if (player.transform.Find("slot").childCount != 0)
                    {
                        this.GetComponent<PhotonView>().RPC("addItemRPC", RpcTarget.AllBuffered, playerHold.heldObj.GetComponent<PhotonView>().ViewID,
                            player.GetComponent<PhotonView>().ViewID);

                        globalClicked.applianceInteract = true;
                    }
                    else
                    {
                        cookDish();
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
                if (foundDish.stoveFry && !appliancePlayers.Contains(player.GetComponent<PhotonView>().ViewID))
                {

                    myPv.RPC("addPlayer", RpcTarget.AllBuffered, player.GetComponent<PhotonView>().ViewID, myPv.ViewID);

                    added = true;
                }

                Debug.Log("Recipe found: " + foundDish.name + " - " + foundDish.dishID);

                if (this.gameObject.tag == "Oven")
                {
                    if (this.name == "Oven1")
                    {
                        minigameCanvas = PhotonNetwork.Instantiate(Path.Combine("Canvas", "ovencanvas"), transform.GetChild(0).position, Quaternion.Euler(0, 90, 0));
                        minigameCanvas.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        minigameCanvas = PhotonNetwork.Instantiate(Path.Combine("Canvas", "ovencanvas"), transform.GetChild(0).position, Quaternion.identity);
                        minigameCanvas.GetComponent<AudioSource>().Play();
                    }

                    myPv.RPC("setParent", RpcTarget.AllBuffered, minigameCanvas.GetComponent<PhotonView>().ViewID, myPv.ViewID);

                    cookedDishLocal = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", foundDish.Prefab.name),
                        transform.GetChild(1).position, transform.rotation);
                }
                else if (this.gameObject.tag == "Stove" && foundDish.stoveFry)
                {
                    if (!minigameCanvas2)
                    {
                        if (kitchenNum == 1)
                        {
                            minigameCanvas2 = PhotonNetwork.Instantiate(Path.Combine("Canvas", "Frying"),
                                new Vector3(0, 0, 0), transform.rotation);
                        }else if(kitchenNum == 2)
                        {
                            minigameCanvas2 = PhotonNetwork.Instantiate(Path.Combine("Canvas", "Frying2"),
                                new Vector3(0, 0, 0), transform.rotation);
                        }
                        pv.RPC("setInMinigame", RpcTarget.All, pv.ViewID);


                        UIcamera.enabled = true;
                        playerController = player.GetComponent<PlayerController>();
                        player.GetComponentInChildren<playerMvmt>().enabled = false;

                        playerController.enabled = false;
                        minigameCanvas2.GetComponent<Canvas>().worldCamera = UIcamera;
                        canvas.SetActive(false);

                        myPv.RPC("falseForOthers", RpcTarget.OthersBuffered, myPv.ViewID,
                            minigameCanvas2.GetPhotonView().ViewID);
                        minigameCanvas2.transform.SetParent(transform);

                        myPv.RPC("setAppl", RpcTarget.AllBuffered, minigameCanvas2.GetComponent<PhotonView>().ViewID,
                            myPv.ViewID);

                    }
                    else
                    {
                        
                        minigameCanvas2.SetActive(true);
                        pv.RPC("setInMinigame", RpcTarget.All, pv.ViewID);

                        myPv.RPC("hideFryingUI",RpcTarget.AllBuffered,minigameCanvas2.GetPhotonView().ViewID);
                        
                        canvas.SetActive(false);
                        UIcamera.enabled = false;
                        UIcamera.enabled = true;
                        playerController = player.GetComponent<PlayerController>();
                        player.GetComponentInChildren<playerMvmt>().enabled = false;

                        playerController.enabled = false;

                        minigameCanvas2.GetComponent<Canvas>().worldCamera = UIcamera;
                        cookedDishLocal = PhotonNetwork.Instantiate(
                            Path.Combine("DishPrefabs", foundDish.Prefab.name), transform.GetChild(0).position,
                            transform.rotation);

                        // start cooking animation
                        playerAnimator.animator.SetBool("IsCooking", true);
                    }
                }
                else
                {
                    if (kitchenNum == 1)
                        minigameCanvas.tag = "Team1";
                    else if (kitchenNum == 2)
                        minigameCanvas.tag = "Team2";

                    canvas.gameObject.SetActive(false);
                    minigameCanvas.gameObject.SetActive(true);
                    pv.RPC("setInMinigame", RpcTarget.All, pv.ViewID);
                    
                    
                   if (this.gameObject.tag == "Stove") {
                       myPv.RPC("PlayBoilingSound", RpcTarget.AllBuffered);  
                   }


                    UIcamera.enabled = true;
                    playerController = player.GetComponent<PlayerController>();
                    player.GetComponentInChildren<playerMvmt>().enabled = false;

                    playerController.enabled = false;
                    player.GetComponent<PhotonView>().RPC("DisablePushing", RpcTarget.OthersBuffered,
                        player.GetComponent<PhotonView>().ViewID);
                    playerRigidbody.isKinematic = true;
                    cookedDishLocal = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", foundDish.Prefab.name),
                        transform.GetChild(0).position, transform.rotation);

                    playerAnimator.animator.SetBool("IsCooking", true);
                    
                }

                myPv.RPC("SetToTrue", RpcTarget.AllBuffered, this.GetComponent<PhotonView>().ViewID);

                if (cookedDishLocal)
                {
                    myPv.RPC("cookedDishG", RpcTarget.AllBuffered, myPv.ViewID, cookedDishLocal.GetComponent<PhotonView>().ViewID);

                    Rigidbody dishRigidbody = cookedDish.GetComponent<Rigidbody>();

                    //setting gravity of cookedDish
                    cookedDish.GetComponent<PhotonView>().RPC("SetGrav", RpcTarget.OthersBuffered);
                    dishRigidbody.useGravity = true;
                    r = cookedDish.GetComponent<Renderer>();
                    cookedDish.GetComponent<PhotonView>().RPC("DisableView", RpcTarget.OthersBuffered);
                    r.enabled = false;
                    myPv.RPC("doFd", RpcTarget.All, myPv.ViewID, cookedDish.GetComponent<PhotonView>().ViewID);

                }
                if (tag == "Stove" && foundDish.stoveFry && appliancePlayers.Count > 1)
                {
                    this.GetComponent<PhotonView>().RPC("clearItems", RpcTarget.AllBuffered,
                        this.GetComponent<PhotonView>().ViewID);
                }
                else if (!foundDish.stoveFry)
                {
                
                    myPv.RPC("clearItems", RpcTarget.AllBuffered, myPv.ViewID);
                    myPv.RPC("clearPlayers", RpcTarget.AllBuffered, myPv.ViewID);

                }

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
            

            if (playerHold && playerHold.transform.Find("slot") && playerHold.transform.Find("slot").childCount != 0
                )
            {
                itemsOnTheAppliance.Add(heldObjArgItem.item);
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
            myPv.RPC("setFoundDish", RpcTarget.OthersBuffered, myPv.ViewID, foundDish.dishID);
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
        Appliance appl = PhotonView.Find(viewID).GetComponent<Appliance>();
        if (appl.minigameCanvas2) {
            if(appl.appliancePlayers.Count > 1) {
                appl.isBeingInteractedWith = true;
            }
        }
        else
        {

            PhotonView.Find(viewID).GetComponent<Appliance>().isBeingInteractedWith = true;
        }
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
    void clearItems(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<Appliance>().itemsOnTheAppliance.Clear();
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
    void PlayBoilingSound() {
        if (GetComponent<stoveMinigame>().sound)
            GetComponent<stoveMinigame>().sound.Play();
    }
    [PunRPC]
	void StopBoilingSound() {
        GetComponent<stoveMinigame>().sound.Stop();
    }

    
    [PunRPC]
    void addPlayer(int viewID,int appID)
    {
        PhotonView.Find(appID).GetComponent<Appliance>().appliancePlayers.Add(viewID);
    }
    [PunRPC]
    void falseForOthers(int applID,int viewID)
    {
        GameObject canv = PhotonView.Find(viewID).gameObject;
        Appliance appl = PhotonView.Find(applID).GetComponent<Appliance>();
        canv.transform.SetParent(appl.transform);
        appl.minigameCanvas2 = canv;
        canv.SetActive(false);

    }
    [PunRPC]
    void setAppl(int viewID,int applID)
    {
        GameObject canv = PhotonView.Find(viewID).gameObject;
        GameObject appl = PhotonView.Find(applID).gameObject;
        canv.GetComponentInChildren<Plate>().appliance = appl.GetComponent<Appliance>();
        canv.transform.Find("PanGameObject").transform.GetChild(0).GetComponent<PanController>().appliance = appl.GetComponent<Appliance>();

    }
    [PunRPC]
    void clearPlayers(int viewiD)
    {
        PhotonView.Find(viewiD).GetComponent<Appliance>().appliancePlayers.Clear();
    }
    [PunRPC]
    void setPlayer(int viewiD,int playerID)
    {
        PhotonView.Find(viewiD).GetComponent<Appliance>().player = PhotonView.Find(playerID).transform;
    }

    [PunRPC]
    void hideFryingUI(int canvID)
    {
        PhotonView.Find(canvID).transform.Find("BackButton").gameObject.SetActive(false);

    }
    [PunRPC]
    void setFoundDish(int viewID, string dishID)
    {
            PhotonView.Find(viewID).GetComponent<Appliance>().foundDish = Database.GetDishByID(dishID);
    }
 
}

