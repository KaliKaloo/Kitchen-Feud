using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


/*	
	This component is for all objects that the player can
	interact with such as appliances, items etc. 
*/

public class Interactable : MonoBehaviour
{
	public Transform interactionTransform;

	public bool isFocus = false;
	public Transform player;

	bool hasInteracted = false;

	//cursor changing on hover
	public void OnMouseEnter(){
		if (MouseControl.instance)
       		MouseControl.instance.Clickable();
    }
    
    public void OnMouseExit(){
		if (MouseControl.instance)
        	MouseControl.instance.Default();
    }
	
	public virtual void Interact()
	{
		// This method is meant to be overwritten
	}

	protected virtual void Update()
	{
		if (isFocus && !hasInteracted)
		{
			Interact();
			hasInteracted = true;
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
}