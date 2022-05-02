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
                Debug.Log(transform.parent.position);
                Vector3 parentPos = transform.parent.position;
                parentPos.y = 0.8f;

                transform.position = parentPos;
                Debug.Log(transform.position);
                transform.Rotate(-20,0,0);
        }
        else {
            playerHold.dropItem();
            transform.Rotate(0,0,0);
        }
    }
}
