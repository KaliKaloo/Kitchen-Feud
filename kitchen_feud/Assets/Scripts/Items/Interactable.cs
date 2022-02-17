using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour
{
	public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

	public bool isFocus = false;
	public Transform player;

	bool hasInteracted = false;

	public virtual void Interact()
	{
		// This method is meant to be overwritten
		// Debug.Log("Interacting with " + transform.name);
	}

	void Update()
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

	public void EnterScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	//allow only one player at a time to interact with the object
	public void LockInteraction()
	{

	}

}