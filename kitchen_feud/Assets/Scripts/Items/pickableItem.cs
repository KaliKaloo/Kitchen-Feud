
using UnityEngine;
using Photon.Pun;

public class pickableItem : Interactable
{
    //public Item item;
    //public bool canPickUp = true;
    public BaseFood item;

	public PhotonPlayer p;
    // public IngredientSO ingredient;
    // public DishSO dish;


	//[PunRPC]
	//private void SetPlatformAsParent()
	//{
		
	//	{
	//		/*
			
	//		this.transform.parent = p.myAvatar.transform;
	//		this.transform.localPosition = Vector3.zero;
	//		this.transform.localRotation = Quaternion.Euler(Vector3.zero);
	//		this.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |
	//			RigidbodyConstraints.FreezePositionZ;
			
	//		Debug.Log(p.name);
	//		*/
	//	}
		
	//}
	//[PunRPC]
	//private void SetNullAsParent()
	//{
	//	this.transform.parent = null;
	//}
   
	
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
