using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerHolding : MonoBehaviour
{
    public int holdingLimit = 1;
    public List<BaseFood> items = new List<BaseFood>();
    public Transform slot;
    GameObject clickedObj;
    public GameObject heldObj;
    // BaseFood item;
	PhotonView view;

    public void pickUpItem(GameObject obj, BaseFood item){
 
        items.Add(item);
        heldObj = obj;
        // move object to slot
		Debug.Log("Pick up item: "+ items[0].name);
        if(heldObj.GetComponent<Rigidbody>()){
            Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
            Collider objcol = heldObj.GetComponent<Collider>();

            heldObj.transform.parent = slot;
            heldObj.transform.localPosition = Vector3.zero;
            heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

            objRig.isKinematic = true;
            objcol.isTrigger = true;
        }
    }

    public void dropItem(){
		Debug.Log("Drop item: "+ items[0].name);
        items.Clear();

        Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
        Collider objcol = heldObj.GetComponent<Collider>();
        heldObj.transform.SetParent(null);
        objRig.isKinematic = false;
        objcol.isTrigger = false;
        objRig.useGravity = true;
    }
}
