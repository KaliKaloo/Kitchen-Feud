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

			//if it's a dish print out its points
			Dish dish = gameObject.GetComponent<Dish>();
			if(dish != null) {
				Debug.LogError("points: " + dish.points);
			}


        }
        else{
            playerHold.dropItem();
            playerHold.pickUpItem(gameObject, item);
        }
		
	}
}
