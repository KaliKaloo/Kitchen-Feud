
using UnityEngine;
using Photon.Pun;

public class pickableItem : Interactable
{
    //public Item item;
    //public bool canPickUp = true;
    public BaseFood item;
    PlayerHolding playerHold;
	// public GameObject obj;
    public override void Interact()
	{
		// base.Interact();
        playerHold = player.GetComponent<PlayerHolding>();

        if(playerHold.items.Count==0){
            playerHold.pickUpItem(gameObject, item);
        }
        else{
            playerHold.dropItem();
            playerHold.pickUpItem(gameObject, item);
        }
		
	}
}
