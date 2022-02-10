using UnityEngine;
using Photon.Pun; 

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviour
{
 	public Rigidbody player;
    public float m_speed, rotatespeed;
	public Interactable focus;  // Our current focus: Item, Enemy etc.
	[SerializeField] private Camera cam;         // Reference to our camera
 	PlayerHolding playerHold;
	Stove stove;

	PhotonView view;

	// Get references
	void Start()
	{
        if(PhotonNetwork.IsConnected) {
			// cam = Camera.main;
			view = GetComponent<PhotonView>();
			player = GetComponent<Rigidbody>();
			playerHold = GetComponent<PlayerHolding>();
			//not sure if correct, will work only for one stove so prob not
			stove = GameObject.Find("Stove 1").GetComponent<Stove>();

			if (!view.IsMine)
			{
				cam.enabled = false;
			}
			DontDestroyOnLoad(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// If we press left mouse
		if (view.IsMine)
        {
			if(Input.GetMouseButtonDown(0) && playerHold.items.Count!=0){

				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;				
				//if stove in focus cooking
				if (Physics.Raycast(ray, out hit, 100))
				{
					Interactable interactableStove = hit.collider.GetComponent<Interactable>();
					var potentialStove = hit.collider.gameObject;
					
					if (interactableStove != null)
					{						
						bool isStove = stove.isStoveFunction(potentialStove);
						if (isStove) {
							SetFocus(interactableStove);
							stove.Cook(playerHold.heldObj, playerHold);
							RemoveFocus();
						}
						else {
							RemoveFocus();
							playerHold.dropItem();
						}
					}
				}
				else {
					RemoveFocus();
					playerHold.dropItem();
				}

			}
			else if (Input.GetButtonDown("Fire1"))
			{
				// We create a ray
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				// If the ray hits
				if (Physics.Raycast(ray, out hit, 100))
				{
					Interactable interactable = hit.collider.GetComponent<Interactable>();
					var obj = hit.collider.gameObject;
					//instantiate the particular stove here instead of at the top
					if (interactable != null)
					{
						SetFocus(interactable);
						
						bool isStove = stove.isStoveFunction(obj);
						bool canPickUp = playerHold.canPickUp(obj);
						if (isStove) stove.Cook();
						else if (canPickUp) playerHold.pickUpItem();
					}
				}
			}
		
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
            }
        }

	}
	void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.velocity = transform.forward * m_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.velocity = -transform.forward * m_speed * Time.deltaTime;
            }
        }
    }

	// Set our focus to a new focus
	void SetFocus(Interactable newFocus)
	{
		if (view.IsMine){
			// If our focus has changed
			if (newFocus != focus)
			{
				// Defocus the old one
				if (focus != null)
					focus.OnDefocused();

				focus = newFocus;   // Set our new focus
			}
			newFocus.OnFocused(transform);
		}
	}

	// Remove our current focus
	void RemoveFocus()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
	}

	
}