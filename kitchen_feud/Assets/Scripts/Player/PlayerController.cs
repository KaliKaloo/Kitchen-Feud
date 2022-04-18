using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

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
			if (PhotonNetwork.LocalPlayer.CustomProperties["Team"] != null)
			{
				view.RPC("setTeam", RpcTarget.Others, view.ViewID, (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
				myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
			}
			gameObject.layer = 9;
			gameObject.transform.GetChild(0).gameObject.layer = 9;
			gameObject.transform.GetChild(1).gameObject.layer = 9;
		}
	}

	void Update()
	{
		if (PlayerPrefs.GetInt("disconnected") == 1)
		{
			view.TransferOwnership(1000);
		}
		
		if (view.IsMine)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				if(EventSystem.current.IsPointerOverGameObject())
					return;

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
			else if (Input.GetKeyDown(KeyCode.Q) && player.transform.Find("slot").GetChild(0) ){
				Transform fireExtinguisher = player.transform.Find("slot").GetChild(0);
				ParticleSystem fire_ps = fireExtinguisher.GetComponentInChildren<ParticleSystem>();

				if(fireExtinguisher.name == "fireExtinguisher"){
					if(fire_ps && !fire_ps.isPlaying){
						fire_ps.Play();
					}else{
						fire_ps.Stop();
					}
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
