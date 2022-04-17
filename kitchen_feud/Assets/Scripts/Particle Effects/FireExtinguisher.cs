using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Interactable
{ 
   // public BaseFood item; 
    private ParticleSystem PS; 
    private bool click = true;
    PlayerHolding playerHold;

    public override void Interact()
    { 
        playerHold = player.GetComponent<PlayerHolding>();
        if (player.transform.Find("slot").childCount == 0) {
                playerHold.pickUpItem(gameObject);
        }
        else {
            
            playerHold.dropItem();
        }
    }
}
