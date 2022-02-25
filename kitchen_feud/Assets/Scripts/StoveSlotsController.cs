using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoveSlotsController : MonoBehaviour {
    public List<Transform> slots = new List<Transform>();
    public Stove stove;
    //might need to set to 0 somwhere else
    private int fullnessCount = 0;

    public void PutOnStove(GameObject heldObjArg, PlayerHolding playerHold) {
        if(fullnessCount < 3) {
            for (int i =0;i<slots.Count;i++) {
                if (slots[i].transform.childCount == 0) {

                    heldObjArg.GetComponent<PhotonView>().RPC("setParent", RpcTarget.Others,
                    heldObjArg.GetComponent<PhotonView>().ViewID, slots[i].GetComponent<PhotonView>().ViewID);
                    heldObjArg.transform.parent = slots[i];
                    heldObjArg.transform.localPosition = Vector3.zero;
                    heldObjArg.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    fullnessCount++;

                    break;
                }
            }
        }

    }
    public void ClearStove() {
        for (int i =0;i<slots.Count;i++) {
                if (slots[i].transform.childCount != 0) {
                    //make the destroying multiplayer!!!!
                    GameObject objectInSlot = slots[i].GetChild(0).gameObject;
                    objectInSlot.GetComponent<PhotonView>().RPC("DisableIngredientView", RpcTarget.Others);
                    Destroy(objectInSlot);
                    
                    fullnessCount--;
                }
            }
    }

}
