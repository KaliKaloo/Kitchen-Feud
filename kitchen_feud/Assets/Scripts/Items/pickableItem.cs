
using UnityEngine;
using Photon.Pun;

public class pickableItem : Interactable
{
    public BaseFood item;
	public PhotonPlayer p;
    // public IngredientSO ingredient;
    // public DishSO dish;

    public override void Interact()
	{
		base.Interact();
       
	}

	[PunRPC]
	private void SetPlatformAsParent()
	{
		
		{
			/*
			
			this.transform.parent = p.myAvatar.transform;
			this.transform.localPosition = Vector3.zero;
			this.transform.localRotation = Quaternion.Euler(Vector3.zero);
			this.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX |
				RigidbodyConstraints.FreezePositionZ;
			
			Debug.Log(p.name);
			*/
		}
		
	}
	[PunRPC]
	private void SetNullAsParent()
	{
		this.transform.parent = null;
	}
}
