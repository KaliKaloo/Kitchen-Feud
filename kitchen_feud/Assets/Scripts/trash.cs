using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class trash : Interactable
{
   public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();

        if (playerHold){
            GameObject obj = playerHold.heldObj;
            playerHold.heldObj = null;
            if (obj){
                PhotonNetwork.Destroy(obj);
            }
            playerHold.items.Clear();

        }
        
    }
}
