using UnityEngine;

public class pickableItem : Interactable
{
	//public Item item;
	//public bool canPickUp = true;
	public BaseFood item;
	public bool onTray = false;
	PlayerHolding playerHold;
	public TraySO Tray;
	

	// public GameObject obj;
	public override void Interact()
	{
		// base.Interact();
		playerHold = player.GetComponent<PlayerHolding>();

		if (playerHold.items.Count == 0)
		{
			playerHold.pickUpItem(gameObject, item);
			if (onTray == true){
				removeFromTray(Tray);
			}

		}
		else
		{
			playerHold.dropItem();
			playerHold.pickUpItem(gameObject, item);
		}

	}

	public void removeFromTray(TraySO tray)
	{
		tray.ServingTray.Remove(item);
		onTray = false;
	}
}


