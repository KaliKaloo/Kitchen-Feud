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
    public bool onAppliance = false;
	public TraySO Tray;
    public Tray tray2;
    public Appliance appliance;
   
    // public GameObject obj;
    public override void Interact()
    {
        // base.Interact();
        
        playerHold = player.GetComponent<PlayerHolding>();

        if (playerHold.items.Count == 0) {
            //playerHold.pickUpItem(gameObject, item); should be here!!! It broke everything
            if (onAppliance == false && onTray == false) {
                playerHold.pickUpItem(gameObject, item);
            }
			else if (onTray == true){
                //not here!!!
                playerHold.pickUpItem(gameObject, item);
                tray2.GetComponent<PhotonView>().RPC("removeFromTray", RpcTarget.All, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onTrayF", RpcTarget.All);
				//removeFromTray(Tray);
			}
            else if (onAppliance == true){
                playerHold.pickUpItem(gameObject, item);
                appliance.GetComponent<PhotonView>().RPC("removeFromApplianceRPC", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onApplianceF", RpcTarget.All);
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
    void applianceBool(int viewID,int applianceID)
    {
        PhotonView.Find(viewID).GetComponent<pickableItem>().onAppliance = true;
        PhotonView.Find(viewID).GetComponent<pickableItem>().appliance = PhotonView.Find(applianceID).GetComponent<Appliance>();
    }
    [PunRPC]
    void onTrayF()
    {
        this.onTray = false;
    }
    [PunRPC]
     void onApplianceF()
    {
        this.onAppliance = false;
    }

    [PunRPC]
    void DisableIngredientView()
    {
        Renderer r = GetComponent<Renderer>();
   
        
            r.enabled = !r.enabled;
        
    }
}