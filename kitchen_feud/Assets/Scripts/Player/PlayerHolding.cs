
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickupLock
{
    private static bool pickupLock = false;

    public void Lock()
    {
        pickupLock = true;

    }
    public void Unlock()
    {
        pickupLock = false;
    }


    public bool GetLock()
    {
        return pickupLock;
    }

}
public class PlayerHolding : MonoBehaviour
{
    public int holdingLimit = 1;
    public Transform slot;
    GameObject clickedObj;
    public GameObject heldObj;
    public PhotonView view;
    public bool itemdropped = false;

    public bool itemLock = false;
    PickupLock pickupLock = new PickupLock();


    public void pickUpItem(GameObject obj, BaseFood item)
    {
        // if serve canvas is enabled then dont let player pickup item
        if (!pickupLock.GetLock())
        {
            StartCoroutine(LockPickup());

            if (view.IsMine)
            {
                if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    slotItem(obj, item);
                }
                else
                {
                    this.GetComponent<PhotonView>().RPC("changeLayer", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, 0);
                    obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                    slotItem(obj, item);
                }
            }
        }
    }

    [PunRPC]
    void changeLayer(int viewID, int layer)
    {
        PhotonView.Find(viewID).gameObject.layer = layer;
        foreach (Transform child in PhotonView.Find(viewID).gameObject.transform)
        {
            child.gameObject.layer = layer;
        }
    }



    void slotItem(GameObject obj, BaseFood item)
    {

        
        PhotonView objPV = obj.GetComponent<PhotonView>();
        if (objPV)
        {
            view.RPC("setHeldobj", RpcTarget.All, view.ViewID, objPV.ViewID);
        }
        else
        {
            heldObj = obj;
        }

        if (heldObj.GetComponent<Rigidbody>())
        {
            this.GetComponent<PhotonView>().RPC("SetParentAsSlot", RpcTarget.All, heldObj.GetComponent<PhotonView>().ViewID);

            heldObj.layer = 8;
            foreach (Transform child in heldObj.transform)
            {
                child.gameObject.layer = 8;
            }
        }
    }

    IEnumerator LockPickup()
    {
        itemLock = true;
        // wait 0.5 seconds before can do anything else
        yield return new WaitForSeconds(0.5f);
        itemLock = false;

    }

    public void dropItem()
    {
        if (view.IsMine)
        {
            heldObj.layer = 0;
            foreach (Transform child in heldObj.transform)
            {
                child.gameObject.layer = 0;
            }
            this.GetComponent<PhotonView>().RPC("SetParentAsNull", RpcTarget.All,
                         heldObj.GetComponent<PhotonView>().ViewID);
            //SOUND ---------------------------------------------------------

            this.GetComponent<PhotonView>().RPC("PlayDropSound", RpcTarget.All);

            //---------------------------------------------------------------
            view.RPC("setHeldobjAsNull", RpcTarget.All, view.ViewID);

        }
    }
    [PunRPC]
    void SetParentAsSlot(int viewID)
    {
        PhotonView.Find(viewID).gameObject.transform.SetParent(this.transform.GetChild(2).transform);
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
            PhotonView.Find(viewID).gameObject.transform.SetParent(null);
            PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
            // PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().useGravity = true;
            // PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(2, 2, 2);
            itemdropped = true;
        }
    }
    [PunRPC]
    void setHeldobj(int viewID,int heldObjId)
    {
        PhotonView.Find(viewID).GetComponent<PlayerHolding>().heldObj = PhotonView.Find(heldObjId).gameObject;
    }
    [PunRPC]
    void setHeldobjAsNull(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<PlayerHolding>().heldObj = null;
    }

    [PunRPC]
    void PlayDropSound() {
        //FindObjectOfType<SoundEffectsManager>().dropSound.Play();
        if(heldObj.GetComponent<AudioSource>() != null) heldObj.GetComponent<AudioSource>().Play();
    }

    
}