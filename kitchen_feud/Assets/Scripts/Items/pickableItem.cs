
using UnityEngine;

public class pickableItem : Interactable
{
    //public Item item;
    public bool canPickUp = true;
    public BaseFood item;
    // public IngredientSO ingredient;
    // public DishSO dish;

    public override void Interact()
	{
        base.Interact();
       
	}

   
}
