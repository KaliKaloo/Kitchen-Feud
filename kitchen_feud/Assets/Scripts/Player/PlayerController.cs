using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviourPunCallbacks
{
	public Rigidbody player;
	public Interactable focus;
	public int myTeam;
	[SerializeField] private Camera cam;
	[SerializeField] private Camera secondaryCam;

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
				secondaryCam.enabled = false;

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

			gameObject.layer = 9;
			gameObject.transform.GetChild(0).gameObject.layer = 9;
			gameObject.transform.GetChild(1).gameObject.layer = 9;


           
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
						if (player.transform.Find("slot").childCount!= 0) playerHold.dropItem();
						RemoveFocus();
					}
					// -------------------------------------------------------------------------
				}
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
		PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().myTeam = team;
	}
	[PunRPC]
	void synctele(int viewID, Vector3 pos)
	{
		PhotonView.Find(viewID).transform.position = pos;
	}

}
