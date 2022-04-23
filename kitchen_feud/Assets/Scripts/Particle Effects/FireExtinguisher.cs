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
        }
        else {
            
            playerHold.dropItem();
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


