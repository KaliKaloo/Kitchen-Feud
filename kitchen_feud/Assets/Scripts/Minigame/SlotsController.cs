using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotsController : MonoBehaviour {
    public List<Transform> slots = new List<Transform>();
    // Appliance appliance;
    //might need to set to 0 somwhere else
    private int fullnessCount = 0;

    public void PutOnAppliance(GameObject heldObjArg) {
        if(fullnessCount < 3) {
            for (int i =0;i<slots.Count;i++) {
                if (slots[i].transform.childCount == 0) {
                    heldObjArg.GetComponent<PhotonView>().RPC("setParent", RpcTarget.AllBuffered,
                    heldObjArg.GetComponent<PhotonView>().ViewID, slots[i].GetComponent<PhotonView>().ViewID);
                    fullnessCount++;
                    pickableItem pickable = heldObjArg.GetComponent<pickableItem>();
                    pickable.GetComponent<PhotonView>().RPC("applianceBool", RpcTarget.AllBuffered, pickable.GetComponent<PhotonView>().ViewID, this.GetComponent<PhotonView>().ViewID,this.GetComponent<PhotonView>().ViewID);
                    heldObjArg.layer = 0;
                    foreach ( Transform child in heldObjArg.transform )
                    {
                        child.gameObject.layer = 0;
                    }
                    break;
                }
            }
        }

    }
    public void ClearAppliance() {
        for (int i =0;i<slots.Count;i++) {
                if (slots[i].transform.childCount != 0) {
                    GameObject objectInSlot = slots[i].GetChild(0).gameObject;
                    objectInSlot.GetComponent<PhotonView>().RPC("DisableIngredientView", RpcTarget.AllBuffered,
                        objectInSlot.GetComponent<PhotonView>().ViewID);
                    fullnessCount--;
                }
            }
    }

    public void RemoveFromAppliance(Appliance appliance, GameObject heldObjArg, GameObject player) {
       
            appliance.itemsOnTheAppliance.Remove(heldObjArg.GetComponent<IngredientItem>().item);
            pickableItem pickable = heldObjArg.GetComponent<pickableItem>();
            pickable.onAppliance = false;
            fullnessCount--;
 
    }


    [PunRPC]
     void removeFromApplianceRPC(int appViewID, int objViewID,int playerID)
    {
        RemoveFromAppliance(PhotonView.Find(appViewID).GetComponent<Appliance>(), PhotonView.Find(objViewID).gameObject,PhotonView.Find(playerID).gameObject);


    }


}
