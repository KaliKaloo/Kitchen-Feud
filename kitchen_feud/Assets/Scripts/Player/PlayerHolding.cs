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
	public PhotonView view;



    public void pickUpItem(GameObject obj, BaseFood item){
        if (view.IsMine)
        {
            if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
 
                items.Add(item);
                heldObj = obj;
                // move object to slot
                Debug.Log("Pick up item: "+ items[0].name);
                if(heldObj.GetComponent<Rigidbody>()){

                    Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                    Collider objcol = heldObj.GetComponent<Collider>();

                    heldObj.GetComponent<PhotonView>().RPC("SetPlatformAsParent", RpcTarget.Others);
                    heldObj.transform.SetParent(slot.transform);
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
                 
                    objRig.isKinematic = true;
                    objcol.isTrigger = true;
                }
            }
            else
            {
                obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                items.Add(item);
                heldObj = obj;
                // move object to slot
                Debug.Log("Pick up item: " + items[0].name);
                if (heldObj.GetComponent<Rigidbody>())
                {
                    Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                    Collider objcol = heldObj.GetComponent<Collider>();
               
                    heldObj.GetComponent<PhotonView>().RPC("SetPlatformAsParent", RpcTarget.Others);
                    heldObj.transform.SetParent(slot.transform);
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    objRig.isKinematic = true;
                    objcol.isTrigger = true;
                }
            }

        }
    }

    public void dropItem(){
        if (view.IsMine)
        {
            Debug.Log("Drop item: " + items[0].name);
            items.Clear();
            heldObj.GetComponent<PhotonView>().RPC("SetNullAsParent", RpcTarget.Others);
            Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
            Collider objcol = heldObj.GetComponent<Collider>();
            heldObj.transform.SetParent(null);
            objRig.isKinematic = false;
            objcol.isTrigger = false;
            objRig.useGravity = true;
        }
    }
}
