using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;
using System.Collections;


public class pickableItem : Interactable
{
    //public Item item;
    //public bool canPickUp = true;
    public BaseFood item;
    
    PlayerHolding playerHold;
	public bool onTray = false;
    public bool onStove = false;
	public TraySO Tray;
    public Tray tray2;
    public Stove stove;
    public StoveSlotsController stoveSlots;
    public List<GameObject> stoves = new List<GameObject>();
    
   
    // public GameObject obj;
    public override void Interact()
    {
        // base.Interact();
        
        playerHold = player.GetComponent<PlayerHolding>();

        if (playerHold.items.Count == 0) {
            //playerHold.pickUpItem(gameObject, item); should be here!!! It broke everything
            if (onStove == false && onTray == false) {
                playerHold.pickUpItem(gameObject, item);
            }
			else if (onTray == true){
                //not here!!!
                playerHold.pickUpItem(gameObject, item);
                tray2.GetComponent<PhotonView>().RPC("removeFromTray", RpcTarget.All, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onTrayF", RpcTarget.All);
				//removeFromTray(Tray);
			}
            //same for stove! + removing from the stove list
            //write rpcs for stove
            //not multiplayer yet!!!
            else if (onStove == true){
                //null!
                stoves.AddRange(GameObject.FindGameObjectsWithTag("Stove"));
                Debug.Log(stoves.Count);
                for (int i = 0; i < stoves.Count; i++) {
                    StoveSlotsController ssc = stoves[i].GetComponent<StoveSlotsController>();
                    Debug.LogError("These are the slots: "+ssc.slots.Count);
                    Debug.LogError("Name of this Stove: " + stoves[i].name);
                        for (int j=0;j<ssc.slots.Count;j++) {
                            if(ssc.slots[j] == gameObject.transform.parent) {
                                stove = stoves[i].GetComponent<Stove>();
                                stoveSlots = ssc;
                                break;
                            }
                        }
                    Debug.LogError("Iteration: " + i);
                }
                stoves.Clear();
                  
				//stoveSlots = stove.gameObject.GetComponent<StoveSlotsController>();
                stoveSlots.RemoveFromStove(gameObject);
                playerHold.pickUpItem(gameObject, item);
			}
            
            //if it's a dish print out its points
            Dish dish = gameObject.GetComponent<Dish>();
            if (dish != null) {
               
                Debug.Log("points: " + dish.points);
            }


        }
        else {
            playerHold.dropItem();
            playerHold.pickUpItem(gameObject, item);
        }
      

    }
		public void removeFromTray(TraySO tray)
	{
        
		tray.ServingTray.Remove(item);
		onTray = false;
	}



    [PunRPC]
    void SetGrav()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
    }
    [PunRPC]
    void setParent(int viewID,int viewID1)
    {
        PhotonView.Find(viewID).gameObject.transform.SetParent(PhotonView.Find(viewID1).gameObject.transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.transform.localRotation= Quaternion.Euler(Vector3.zero);

    }
    [PunRPC]
    void trayBool(int viewID,int trayID)
    {
        PhotonView.Find(viewID).GetComponent<pickableItem>().onTray = true;
        PhotonView.Find(viewID).GetComponent<pickableItem>().Tray = PhotonView.Find(trayID).GetComponent<Tray>().tray;
        PhotonView.Find(viewID).GetComponent<pickableItem>().tray2 = PhotonView.Find(trayID).GetComponent<Tray>();

    }
    [PunRPC]
    void onTrayF()
    {
        this.onTray = false;
    }

    [PunRPC]
    void DisableIngredientView()
    {
        Renderer r = GetComponent<Renderer>();
   
        
            r.enabled = !r.enabled;
        
    }
}