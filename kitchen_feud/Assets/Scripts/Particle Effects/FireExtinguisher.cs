using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireExtinguisher : Interactable
{ 
    private ParticleSystem PS; 
    PlayerHolding playerHold;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Interact()
    { 
        playerHold = player.GetComponent<PlayerHolding>();
        if (player.transform.Find("slot").childCount == 0) {
                playerHold.pickUpItem(gameObject);
                globalClicked.holdingFireEx = true;
                Vector3 parentPos = transform.parent.position;
                transform.Rotate(0,180,0);
        }
        else {
            playerHold.dropItem();
            PV.RPC("stopPS",RpcTarget.All,PV.ViewID);
        }
    }

      public IEnumerator removeKinematics(GameObject heldObj)
    {
        yield return new WaitForSeconds(0.5f);
        heldObj.GetComponent<Rigidbody>().isKinematic = true;
    }

    [PunRPC]
    void playPS(int viewID)
    {
        PhotonView.Find(viewID).GetComponentInChildren<ParticleSystem>().Play();
    }
    
    [PunRPC]
    void stopPS(int viewID)
    {
        PhotonView.Find(viewID).GetComponentInChildren<ParticleSystem>().Stop();
    }
    [PunRPC]
    void putBackFireExt(int viewID,int viewID1)
    {
        PhotonView.Find(viewID).GetComponentInChildren<ParticleSystem>().Stop();
        PhotonView.Find(viewID).gameObject.transform.SetParent(PhotonView.Find(viewID1).gameObject.transform);
        PhotonView.Find(viewID).gameObject.transform.localPosition = Vector3.zero;
        PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        PhotonView.Find(viewID).gameObject.GetComponent<Collider>().isTrigger = false;
        PhotonView.Find(viewID).gameObject.transform.localRotation= Quaternion.Euler(Vector3.zero);
        StartCoroutine(removeKinematics(PhotonView.Find(viewID).gameObject));

    }
    
}


