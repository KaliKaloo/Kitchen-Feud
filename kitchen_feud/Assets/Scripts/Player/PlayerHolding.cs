
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


    public void pickUpItem(GameObject obj)
    {
        // if serve canvas is enabled then dont let player pickup item
        if (!pickupLock.GetLock())
        {
            StartCoroutine(LockPickup());

            //if (view.IsMine)
            {

                if (obj.GetComponent<PhotonView>().Owner.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    slotItem(obj);

                }
                else
                {
                    this.GetComponent<PhotonView>().RPC("changeLayer", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, 0);
                    obj.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
                    slotItem(obj);
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



    void slotItem(GameObject obj)
    {
        PhotonView objPV = obj.GetComponent<PhotonView>();
        this.GetComponent<PhotonView>().RPC("SetParentAsSlot", RpcTarget.All, view.ViewID, obj.GetComponent<PhotonView>().ViewID);
        if (heldObj.GetComponent<Rigidbody>() || heldObj.name == "TrayPrefab(Clone)") 
        {


            if (!transform.CompareTag("Waiter1") && !transform.CompareTag("Waiter2") && !transform.tag.Contains("Owner"))

            {
                heldObj.layer = 8;

                foreach (Transform child in heldObj.transform)
                {
                    if (!child.GetComponent<ParticleSystem>()){
                        child.gameObject.layer = 8;
                    }
                   
                }
            }
        }
    }

    private void Update()
    {
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

            if (heldObj.name == "fireExtinguisher")
            {
                heldObj.GetComponent<PhotonView>()
                    .RPC("stopPS", RpcTarget.All, heldObj.GetComponent<PhotonView>().ViewID);
            }

            this.GetComponent<PhotonView>().RPC("PlayDropSound", RpcTarget.All);

            this.GetComponent<PhotonView>().RPC("SetParentAsNull", RpcTarget.All,view.ViewID,
                         heldObj.GetComponent<PhotonView>().ViewID);

        }
    }
    [PunRPC]
    void SetParentAsSlot(int viewID,int heldObjId)
    {
        PhotonView.Find(viewID).GetComponent<PlayerHolding>().heldObj = PhotonView.Find(heldObjId).gameObject;

        GameObject obj = PhotonView.Find(heldObjId).gameObject;
        obj.transform.SetParent(this.transform.GetChild(2).transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        if(obj.GetComponent<Rigidbody>()){
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Collider>().isTrigger = true;
        }
        // PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(2.86f, 2, 2.86f);
    }
    [PunRPC]
    void SetParentAsNull(int viewID, int heldObjId)
    {
        {
            PhotonView.Find(viewID).GetComponent<PlayerHolding>().heldObj = null;
            GameObject obj = PhotonView.Find(heldObjId).gameObject;
            obj.transform.SetParent(null);
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Collider>().isTrigger = false;
            // PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().useGravity = true;
            // PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(2, 2, 2);
            itemdropped = true;
        }
    }
   

    [PunRPC]
    void PlayDropSound() {
        if(heldObj != null && heldObj.GetComponent<AudioSource>() != null) {
            heldObj.GetComponent<AudioSource>().pitch = Random.Range(heldObj.GetComponent<pickableItem>().pitchMin, heldObj.GetComponent<pickableItem>().pitchMax);
            heldObj.GetComponent<AudioSource>().volume = Random.Range(heldObj.GetComponent<pickableItem>().volumeMin, heldObj.GetComponent<pickableItem>().volumeMax);
            heldObj.GetComponent<AudioSource>().Play();
        }
    }

    
}