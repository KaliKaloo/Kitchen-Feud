
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

    // BaseFood item;



    public void pickUpItem(GameObject obj, BaseFood item)
    {

        // if(items.Count >= holdingLimit){
        //     Debug.Log("You are already holding an item. Please drop it first.");
        //     return;
        // }
        if (view.IsMine)
        {
            if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
                
                items.Add(item);
                heldObj = obj;
                // move object to slot
                Debug.Log("Pick up item: " + items[0].name);
                if (heldObj.GetComponent<Rigidbody>())
                {
                    //Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                    //Collider objcol = heldObj.GetComponent<Collider>();
                    this.GetComponent<PhotonView>().RPC("SetParentAsSlot", RpcTarget.All,
                        heldObj.GetComponent<PhotonView>().ViewID);
                   /* heldObj.transform.parent = slot;
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

                    objRig.isKinematic = true;
                    objcol.isTrigger = true;
                   */
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
                   // Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
                   // Collider objcol = heldObj.GetComponent<Collider>();
                    this.GetComponent<PhotonView>().RPC("SetParentAsSlot", RpcTarget.All,
                        heldObj.GetComponent<PhotonView>().ViewID);
                   /* heldObj.transform.parent = slot;
                    heldObj.transform.localPosition = Vector3.zero;
                    heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

                    objRig.isKinematic = true;
                    objcol.isTrigger = true;*/
                }
               
            }

        }
    }

    public void dropItem()
    {
        if (view.IsMine)
        {
            Debug.Log("Drop item: " + items[0].name);
            items.Clear();
            this.GetComponent<PhotonView>().RPC("SetParentAsNull", RpcTarget.All,
                         heldObj.GetComponent<PhotonView>().ViewID);
            //Rigidbody objRig = heldObj.GetComponent<Rigidbody>();
            //Collider objcol = heldObj.GetComponent<Collider>();
            //heldObj.transform.SetParent(null);
            //objRig.isKinematic = false;
            //objcol.isTrigger = false;
            //objRig.useGravity = true;
        }
    }
    [PunRPC]
    void SetParentAsSlot(int viewID) {
        PhotonView.Find(viewID).gameObject.transform.SetParent( this.transform.GetChild(2).transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = true;
        PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = true;
    }
    [PunRPC]
    void SetParentAsNull(int viewID)
    {
        if (PhotonView.Find(viewID).gameObject != null){
            PhotonView.Find(viewID).gameObject.transform.SetParent(null);
        }
        //PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
       // PhotonView.Find(viewID).gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

}