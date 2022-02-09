
using UnityEngine;

public class pickableItem : Interactable
{
    public Item item;
    public bool canPickUp = true;

    public override void Interact()
	{
        base.Interact();
	}

   
}
