using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tray : Interactable
{
    public TraySO tray;
    public BaseFood item;
    public GameObject objectHolding;
    public List<Transform> slots = new List<Transform>();

    PlayerHolding playerHold;
    public pickableItem pickable;
    
    public override void Interact()
    {
        playerHold = player.GetComponent<PlayerHolding>();
        objectHolding = playerHold.heldObj;

        if (playerHold.items.Count == 1)
        {
            //add object holding to tray slot if tray slot empty
            if (tray.ServingTray.Count < 4)
            {
                //foreach (Transform slot in slots)
                for (int i =0;i<slots.Count;i++)
                { 
                    if (slots[i].transform.childCount == 0)
                    {
                        playerHold.dropItem();
                        
                        objectHolding.GetComponent<PhotonView>().RPC("setParent", RpcTarget.Others,
                            objectHolding.GetComponent<PhotonView>().ViewID, slots[i].GetComponent<PhotonView>().ViewID);
                        objectHolding.transform.parent = slots[i];
                        objectHolding.transform.localPosition = Vector3.zero;
                        objectHolding.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        break;
                    }
                }

                //add basefood item to list of foods of the tray

                pickable = objectHolding.GetComponent<pickableItem>();
                pickable.GetComponent<PhotonView>().RPC("trayBool", RpcTarget.All, pickable.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID);
                //pickable.onTray = true;
                this.GetComponent<PhotonView>().RPC("addComps", RpcTarget.All,this.GetComponent<PhotonView>().ViewID,objectHolding.GetComponent<PhotonView>().ViewID);
                //tray.ServingTray.Add(pickable.item);
                //tray.objectsOnTray.Add(objectHolding);
                //pickable.Tray = tray;
                //Debug.Log(tray.ServingTray.Count);
            }

            else
            {
                //do nothing because there are no empty slots
              

            }
        }


    }
    [PunRPC]
    void addComps(int viewID, int objID)
    {
        PhotonView.Find(viewID).GetComponent<Tray>().tray.ServingTray.Add(PhotonView.Find(objID).GetComponent<pickableItem>().item);
       // tray.ServingTray.Add(pickable.item);
        PhotonView.Find(viewID).GetComponent<Tray>().tray.objectsOnTray.Add(PhotonView.Find(objID).gameObject);
       // tray.objectsOnTray.Add(objectHolding);

    }
   [PunRPC]
   void removeFromTray(int viewID)
    {
        this.tray.ServingTray.Remove(PhotonView.Find(viewID).GetComponent<pickableItem>().item);
        this.tray.objectsOnTray.Remove(PhotonView.Find(viewID).gameObject);

    }

} 
 