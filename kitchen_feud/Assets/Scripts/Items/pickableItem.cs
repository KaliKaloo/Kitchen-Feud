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

    public Vector3 defaultScale;
    public AudioSource dropSound;
    public float pitchMin, pitchMax, volumeMin, volumeMax;

   private void Start()
   {
       defaultScale = transform.localScale;
       pitchMin = 0.5f;
       pitchMax = 2f;
       volumeMax = 1f;
       volumeMin = 0.5f;
   }

   public override void Interact()
    {
        
        playerHold = player.GetComponent<PlayerHolding>();

        if (player.transform.Find("slot").childCount == 0) {
            if (onAppliance == false && onTray == false) {
                playerHold.pickUpItem(gameObject);
            }
			else if (onTray == true){
                playerHold.pickUpItem(gameObject);
                tray2.GetComponent<PhotonView>().RPC("removeFromTray", RpcTarget.All, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onTrayF", RpcTarget.All);
			}
            else if (onAppliance == true){
                playerHold.pickUpItem(gameObject);
                applianceSlots.GetComponent<PhotonView>().RPC("removeFromApplianceRPC", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID, player.GetComponent<PhotonView>().ViewID);
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
    public IEnumerator removeKinematics(GameObject heldObj)
    {
        yield return new WaitForSeconds(0.5f);
        heldObj.GetComponent<Rigidbody>().isKinematic = true;
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
        StartCoroutine(removeKinematics(PhotonView.Find(viewID).gameObject));

    }
    [PunRPC]
    void setParentTray(int viewID,int viewID1)
    {
        PhotonView.Find(viewID).gameObject.transform.SetParent(PhotonView.Find(viewID1).gameObject.transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
    [PunRPC]
    void DisableItemPickable(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<pickableItem>().enabled = false;
    }
}