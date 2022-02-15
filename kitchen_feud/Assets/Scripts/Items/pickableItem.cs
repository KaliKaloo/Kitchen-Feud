using UnityEngine;

public class pickableItem : Interactable
{
    //public Item item;
    public bool canPickUp = true;
    public BaseFood item;
    PlayerHolding playerHold;
    public override void Interact()
    {
        playerHold = player.GetComponent<PlayerHolding>();

        if (playerHold.items.Count == 0)
        {
            playerHold.pickUpItem(gameObject, item);
        }
        else
        {
            playerHold.dropItem();
            playerHold.pickUpItem(gameObject, item);
        }
    }
}