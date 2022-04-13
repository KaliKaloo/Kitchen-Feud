using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System;
using System.Collections;


public class pickableItem : Interactable
{
    public BaseFood item;
    
    PlayerHolding playerHold;
	public bool onTray = false;
    public bool onAppliance = false;
	public TraySO Tray;
    public Tray tray2;
    public Appliance appliance;
    public SlotsController applianceSlots;
    //SOUND --------------------------------------------
    public AudioSource dropSound;
   //-----------------------------------------------------
    public override void Interact()
    {
        
        playerHold = player.GetComponent<PlayerHolding>();

        if (player.transform.Find("slot").childCount == 0) {
            if (onAppliance == false && onTray == false) {
                playerHold.pickUpItem(gameObject, item);
            }
			else if (onTray == true){
                playerHold.pickUpItem(gameObject, item);
                tray2.GetComponent<PhotonView>().RPC("removeFromTray", RpcTarget.AllBuffered, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onTrayF", RpcTarget.AllBuffered);
			}
            else if (onAppliance == true){
                playerHold.pickUpItem(gameObject, item);
                applianceSlots.GetComponent<PhotonView>().RPC("removeFromApplianceRPC", RpcTarget.AllBuffered, appliance.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID, player.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onApplianceF", RpcTarget.AllBuffered);
			}
            
            //if it's a dish print out its points
            Dish dish = gameObject.GetComponent<Dish>();
            if (dish != null) {
               
                Debug.Log("points: " + dish.points);
            }
        }
        else {
            
            playerHold.dropItem();
            //playerHold.pickUpItem(gameObject, item);
        }
    }
    
    public void removeFromTray(TraySO tray)
	{
        
		tray.ServingTray.Remove(item);
		onTray = false;
	}

    public void interactPublic()
    {
        Interact();
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
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
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
    void applianceBool(int viewID,int applianceID, int slotsID)
    {
        PhotonView.Find(viewID).GetComponent<pickableItem>().onAppliance = true;
        PhotonView.Find(viewID).GetComponent<pickableItem>().appliance = PhotonView.Find(applianceID).GetComponent<Appliance>();
        PhotonView.Find(viewID).GetComponent<pickableItem>().applianceSlots = PhotonView.Find(slotsID).GetComponent<SlotsController>();
    
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
    void DisableIngredientView(int viewID)
    {

        Destroy(PhotonView.Find(viewID).gameObject);
    }
}