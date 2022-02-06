using UnityEngine;
using Photon.Pun;


/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour
{

	public float radius = 3f;              
	public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

	bool isFocus = false;  
	Transform player;      

	bool hasInteracted = false; 

	public virtual void Interact()
	{
		// This method is meant to be overwritten
		// TEMPORARY
		Debug.Log("Interacting with " + transform.name);
		Destroy(gameObject);
		// TEMPORARY
	}

	void Update()
	{
		if (isFocus && !hasInteracted)
		{
			// If we are close enough
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	// Draw our radius in the editor
	void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}