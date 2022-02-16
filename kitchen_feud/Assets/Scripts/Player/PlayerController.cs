using UnityEngine;
using Photon.Pun;

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviour
{
	public Rigidbody player;
	public float m_speed, rotatespeed;
	public Interactable focus;
	[SerializeField] private Camera cam;
	PlayerHolding playerHold;
	public PhotonView view;

	void Start()
	{
		if (PhotonNetwork.IsConnected)
		{
			view = GetComponent<PhotonView>();
			player = GetComponent<Rigidbody>();
			playerHold = GetComponent<PlayerHolding>();
			if (!view.IsMine)
			{
				cam.enabled = false;
			}
			
			DontDestroyOnLoad(gameObject);

		
			
		}
	}

	void Update()
	{
		if (view.IsMine)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100))
				{
					Interactable interactable = hit.collider.GetComponent<Interactable>();
					var obj = hit.collider.gameObject;
					if (interactable != null)
					{
						SetFocus(interactable, obj);
					}
					// TEMPORARY - Player should not be able to drop item anywhere. 
					// Drop only on counters, stove etcc
					else
					{
						PlayerHolding playerHold = player.GetComponent<PlayerHolding>();
						if (playerHold.items.Count != 0) playerHold.dropItem();
						RemoveFocus();
					}
					// -------------------------------------------------------------------------
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
	void SetFocus(Interactable newFocus, GameObject obj)
	{
		if (view.IsMine)
		{
			float distance = Vector3.Distance(player.position, obj.GetComponent<Transform>().position);

			if (distance <= 3f)
			{
				if (newFocus != focus)
				{
					// Defocus the old one
					if (focus != null)
						focus.OnDefocused();

					focus = newFocus;
				}
				newFocus.OnFocused(transform);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 3f);
	}

	// Remove our current focus
	void RemoveFocus()
	{
		if (view.IsMine)
		{
			if (focus != null)
				focus.OnDefocused();

			focus = null;
		}
	}

}