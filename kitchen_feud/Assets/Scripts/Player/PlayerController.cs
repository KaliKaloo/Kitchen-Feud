using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UIElements;

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviourPunCallbacks
{
	public Rigidbody player;
	public float m_speed;
	public Interactable focus;
	public int myTeam;
	[SerializeField] private Camera cam;
	PlayerHolding playerHold;
	public PhotonView view;
	public CharacterController controller;
	Vector3 velocity;



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
        if (view.IsMine)
        {
			this.name = "Local";
			view.RPC("setTeam", RpcTarget.Others, view.ViewID, (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
			myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
			
			if(myTeam == 1)
            {
				GetComponent<PhotonView>().RPC("syncMat", RpcTarget.All, GetComponent<PhotonView>().ViewID,"cat_red");
            }
            else
            {
				GetComponent<PhotonView>().RPC("syncMat", RpcTarget.All, GetComponent<PhotonView>().ViewID, "cat_blue");
			}
		}
		
		
	}
 
    void Update()
	{
	
		if (view.IsMine)
		{
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			
			Vector3 move = transform.right * x + transform.forward * z + new Vector3(0,-10,0);
			controller.Move(move * m_speed * Time.deltaTime);
			Debug.LogError(velocity);
			//velocity.y = -10;
			//controller.Move(velocity * Time.deltaTime);
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

			
		}
	}
   
/*
	void FixedUpdate(){
		if (view.IsMine)
		{
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			Vector3 move = transform.right * x + transform.forward * z;
			controller.Move(move * m_speed * Time.deltaTime);
			velocity.y = -10;
			controller.Move(velocity * Time.deltaTime);
		}
	}

*/
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

	public Vector3 getTransformVelocity(Vector3 transform){
		return transform * m_speed * Time.deltaTime;
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
    [PunRPC]
    void DisablePushing(int viewID)
    {
        Rigidbody r = PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>();
        r.isKinematic = true;
    }

    [PunRPC]
     void EnablePushing(int viewID)
    {
		Rigidbody r = PhotonView.Find(viewID).gameObject.GetComponent<Rigidbody>();
        r.isKinematic = false;
    }

	[PunRPC]
	void syncMat(int viewID, string name)
	{
		Material newMat = Resources.Load(name, typeof(Material)) as Material;
		PhotonView.Find(viewID).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = newMat;
		PhotonView.Find(viewID).tag = "Player";

	}
	[PunRPC]
	void setTeam(int viewID, int team)
    {
		PhotonView.Find(viewID).GetComponent<PlayerController>().myTeam = team;
    }
	[PunRPC]
	void synctele(int viewID, Vector3 pos)
	{
		PhotonView.Find(viewID).transform.position = pos;
	}




}