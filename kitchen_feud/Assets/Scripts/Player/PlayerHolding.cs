using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerHolding : MonoBehaviour
{
    public int holdingLimit = 1;
    public List<ItemObject> items = new List<ItemObject>();
    public Transform slot;
    GameObject clickedObj;
    public GameObject heldObj;
    ItemObject item;
	PhotonView view;

    // void update(){
    //     if (clickedObj == null){
    //         clickedObj = null;
    //         heldObj = null;
    //         item=null;
    //         items.Clear();
    //     }
    // }

    public bool canPickUp(GameObject obj){
        pickableItem PickableItem = obj.GetComponent<pickableItem>();
        if(PickableItem.item.canPickUp){
            item = PickableItem.item;
            clickedObj = obj;
            return true;
        } 
        else{
            Debug.Log("cannot pickup item");
            return false;
        }
    }

    public void pickUpItem(){
        
        // if(items.Count >= holdingLimit){
        //     Debug.Log("You are already holding an item. Please drop it first.");
        //     return;
        // }
        items.Add(item);
        heldObj = clickedObj;
        // move object to slot
		Debug.Log("Pick up item: "+ items[0].name);
        if(heldObj.GetComponent<Rigidbody>()){
            Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
            Collider objcol = heldObj.GetComponent<Collider>();

            heldObj.transform.parent = slot;
            heldObj.transform.localPosition = Vector3.zero;
            heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            // objRig.isKinematic = true;
            // objcol.isTrigger = true;
        }
    }

    public void dropItem(){
		Debug.Log("Drop item: "+ items[0].name);
        items.Clear();

        Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
        Collider objcol = heldObj.GetComponent<Collider>();
        heldObj.transform.SetParent(null);
        // objRig.isKinematic = false;
        // objcol.isTrigger = false;
    }
}
