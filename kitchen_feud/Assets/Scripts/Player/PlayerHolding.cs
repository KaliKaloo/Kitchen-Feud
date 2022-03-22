
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
    public bool itemdropped = false;

    // BaseFood item;

    public void pickUpItem(GameObject obj, BaseFood item)
    {

    
        if (view.IsMine)
        {
            if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
               slotItem(obj, item); 
            }
            else
            {
                obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                slotItem(obj, item); 
            }
        }
    }


    void slotItem(GameObject obj, BaseFood item){
                
        items.Add(item);
        heldObj = obj;
        if (heldObj.GetComponent<Rigidbody>())
        {
            this.GetComponent<PhotonView>().RPC("SetParentAsSlot", RpcTarget.All, heldObj.GetComponent<PhotonView>().ViewID);
            
            heldObj.layer = 7;
            foreach ( Transform child in heldObj.transform )
            {
                child.gameObject.layer = 7;
            }
        }
    }

    public void dropItem()
    {
        if (view.IsMine)
        {
            heldObj.layer = 0;
            foreach ( Transform child in heldObj.transform )
            {
                child.gameObject.layer = 0;
            }
            items.Clear();
            this.GetComponent<PhotonView>().RPC("SetParentAsNull", RpcTarget.All,
                         heldObj.GetComponent<PhotonView>().ViewID);
        }
    }
    [PunRPC]
    void SetParentAsSlot(int viewID) {
        PhotonView.Find(viewID).gameObject.transform.SetParent( this.transform.GetChild(2).transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = true;
        PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = true;
        // PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(2.86f, 2, 2.86f);
    }
    [PunRPC]
    void SetParentAsNull(int viewID)
    {
      
       
        {
            this.items.Clear();
            PhotonView.Find(viewID).gameObject.transform.SetParent(null);
            PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
            PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().useGravity = true;
            // PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(2, 2, 2);
            itemdropped = true;
        }
    }

    [PunRPC]
    void clearItems(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<PlayerHolding>().items.Clear();
    }

}