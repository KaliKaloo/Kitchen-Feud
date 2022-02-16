
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
    public PhotonView view;

    // void update(){
    //     if (clickedObj == null){
    //         clickedObj = null;
    //         heldObj = null;
    //         item=null;
    //         items.Clear();
    //     }
    // }



    public void pickUpItem(GameObject obj, BaseFood item)
    {

        // if(items.Count >= holdingLimit){
        //     Debug.Log("You are already holding an item. Please drop it first.");
        //     return;
        // }
        if (view.IsMine)
        {
            if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
<<<<<<< HEAD
            {
=======
        {
>>>>>>> dev
                items.Add(item);
                heldObj = obj;
                // move object to slot
                Debug.Log("Pick up item: " + items[0].name);
                if (heldObj.GetComponent<Rigidbody>())
                {
                    Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                    Collider objcol = heldObj.GetComponent<Collider>();
                    //heldObj.GetComponent<PhotonView>().RPC("SetPlatformAsParent", RpcTarget.Others);
                    heldObj.transform.parent = slot;
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

                    objRig.isKinematic = true;
                    objcol.isTrigger = true;
                }
            }
<<<<<<< HEAD
            else
            {
                obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
=======
        else
        {
            obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
>>>>>>> dev
                items.Add(item);
                heldObj = obj;
                // move object to slot
                Debug.Log("Pick up item: " + items[0].name);
                if (heldObj.GetComponent<Rigidbody>())
                {
                    Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                    Collider objcol = heldObj.GetComponent<Collider>();
                    //heldObj.GetComponent<PhotonView>().RPC("SetPlatformAsParent", RpcTarget.Others);
                    heldObj.transform.parent = slot;
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

                    objRig.isKinematic = true;
                    objcol.isTrigger = true;
                }
<<<<<<< HEAD

=======
               
>>>>>>> dev
            }

        }
    }

    public void dropItem()
    {
        if (view.IsMine)
        {
            Debug.Log("Drop item: " + items[0].name);
            items.Clear();
<<<<<<< HEAD
            // heldObj.GetComponent<PhotonView>().RPC("SetNullAsParent", RpcTarget.Others);
=======
           // heldObj.GetComponent<PhotonView>().RPC("SetNullAsParent", RpcTarget.Others);
>>>>>>> dev
            Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
            Collider objcol = heldObj.GetComponent<Collider>();
            heldObj.transform.SetParent(null);
            objRig.isKinematic = false;
            objcol.isTrigger = false;
            objRig.useGravity = true;
        }
    }
}