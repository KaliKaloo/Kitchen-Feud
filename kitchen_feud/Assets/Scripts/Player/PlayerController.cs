using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UIElements;

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviourPunCallbacks
{
	public Rigidbody player;
	public float m_speed, rotatespeed;
	public Interactable focus;
	public int myTeam;
	[SerializeField] private Camera cam;
	PlayerHolding playerHold;
	public GameObject healthbar1;
	public GameObject theirHealthBar;
	public HealthBar theirHealthBarr;
	public Slider theirSlider;
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
						if(obj.tag == "Player" && obj.GetComponent<SpatialAudio>().isKickable) {
							view.RPC("push", RpcTarget.All,obj.GetComponent<PhotonView>().ViewID, view.ViewID);
							if (!obj.GetComponent<PlayerController>().healthbar1)
							{
								theirHealthBar = PhotonNetwork.Instantiate(Path.Combine("HealthBar", "Canvas 1"), obj.transform.GetChild(4).position, Quaternion.identity);
								view.RPC("setObjParent", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID,theirHealthBar.GetComponent<PhotonView>().ViewID);
                            }
                            else
                            {
								theirHealthBarr = obj.GetComponent<PlayerController>().healthbar1.transform.GetChild(0).GetComponent<HealthBar>();
								if(theirHealthBarr.slider.value >0){
									view.RPC("giveDamage", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);
								}
                                else
                                {
									GameObject.FindGameObjectWithTag("Kick").GetComponent<kickPlayers>().kickPlayer(obj);
									view.RPC("destHB", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);
                                }
								
                            }
							
						}

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

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Rotate(0, -rotatespeed * Time.deltaTime, 0);
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				transform.Rotate(0, rotatespeed * Time.deltaTime, 0);
			}
		}
	}
	void FixedUpdate()
	{
		if (view.IsMine)
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				player.velocity = getTransformVelocity(transform.forward);
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				player.velocity = -getTransformVelocity(transform.forward);
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
	[PunRPC]
	void push(int objID, int myID)
    {
		GameObject obj = PhotonView.Find(objID).gameObject;
		GameObject me = PhotonView.Find(myID).gameObject;
		Rigidbody rb = obj.GetComponent<Rigidbody>();
		Vector3 direction = obj.transform.position - me.transform.position;
		direction.y = 0;
		rb.AddForce(direction * 2, ForceMode.Impulse);
    }
	[PunRPC]
	void setObjParent(int viewID,int hBviewID )
    {
		GameObject obj1 = PhotonView.Find(viewID).gameObject;
		obj1.GetComponent<PlayerController>().healthbar1 = PhotonView.Find(hBviewID).gameObject;
		obj1.GetComponent<PlayerController>().healthbar1.transform.SetParent(obj1.transform.GetChild(4));
    }
	[PunRPC]
	void giveDamage(int viewID)
    {
		GameObject obj = PhotonView.Find(viewID).gameObject;
		HealthBar hb = obj.GetComponent<PlayerController>().healthbar1.transform.GetChild(0).GetComponent<HealthBar>();
		hb.SetHealth((int)hb.slider.value - 1);
    }
	[PunRPC]
	void destHB(int viewID)
    {
		GameObject obj = PhotonView.Find(viewID).gameObject;
		GameObject hb = obj.GetComponent<PlayerController>().healthbar1.gameObject;
		Destroy(hb);
	}
}