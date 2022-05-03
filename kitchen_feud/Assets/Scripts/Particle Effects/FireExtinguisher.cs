using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireExtinguisher : Interactable
{ 
   // public BaseFood item;
    private ParticleSystem PS; 
    private bool click = true;
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
                Vector3 parentPos = transform.parent.position;
                parentPos.y = 0.8f;

                transform.position = parentPos;
                transform.Rotate(-20,0,0);
        }
        else {
            playerHold.dropItem();
            transform.Rotate(0,0,0);
            PV.RPC("stopPS",RpcTarget.All,PV.ViewID);
        }
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
    
}


