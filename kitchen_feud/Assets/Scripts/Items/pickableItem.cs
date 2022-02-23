using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;


public class pickableItem : Interactable
{
    //public Item item;
    //public bool canPickUp = true;
    public BaseFood item;
    
    PlayerHolding playerHold;
	public bool onTray = false;
	public TraySO Tray;
    public Tray tray2;

   
    // public GameObject obj;
    public override void Interact()
    {
        // base.Interact();
        playerHold = player.GetComponent<PlayerHolding>();

        if (playerHold.items.Count == 0) {
            playerHold.pickUpItem(gameObject, item);
			if (onTray == true){
                tray2.GetComponent<PhotonView>().RPC("removeFromTray", RpcTarget.All, this.GetComponent<PhotonView>().ViewID);
                GetComponent<PhotonView>().RPC("onTrayF", RpcTarget.All);
				//removeFromTray(Tray);
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
}