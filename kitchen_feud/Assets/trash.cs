using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : Interactable
{
   public override void Interact()
    {
        
        PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
        Debug.Log(playerHold);

        if (playerHold){
            GameObject obj = playerHold.heldObj;
            playerHold.heldObj = null;
            Destroy(obj);
            playerHold.items.Clear();

        }
        
    }
}
