using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PlayerVoiceManager : MonoBehaviour
{
	public Rigidbody player;
	public Interactable focus;
	public int myTeam;
	[SerializeField] private Camera cam;
	PlayerHolding playerHold;
	public GameObject healthbar1;
	public GameObject theirHealthBar;
	public HealthBar theirHealthBarr;
	public Slider theirSlider;
	public PhotonView view;
	public bool isKickable;
	public bool entered1;
	public bool entered2;
	public int myC;
	public bool played;
	public float timer;
	public float timer1;
	public bool started;
	public bool started1;
	public List<int> kickedBy;
	// Start is called before the first frame update
	void Start()
	{
		entered1 = false;
		entered2 = false;
		started = false;
		started1 = false;
		view = GetComponent<PhotonView>();
		myC = 1;
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
			view.RPC("setTeam", RpcTarget.Others, view.ViewID, (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
			myTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
			if (myTeam == 1)
			{
				view.RPC("setEntered", RpcTarget.All, view.ViewID, 1);
			}
			else 
			{
				view.RPC("setEntered", RpcTarget.All, view.ViewID, 2);
			}
		}
	
	}

	// Update is called once per frame
	void Update()
	{
		

		if (view.IsMine)
		{
			if(started == true)
            {
				Increment();
            }
			if(timer > 3 && healthbar1.GetComponentInChildren<HealthBar>().slider.value <= 4)
            {
				//started1 = true;
				timer1 += Time.deltaTime;
				Debug.Log("HERRERE");
				if (timer1 > 1)
				{
					view.RPC("giveDamage", RpcTarget.All, view.ViewID, 1);
					timer1 = 0;
				}
            }
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
						Debug.LogError(obj.name);



						if (obj.tag == "Player" && obj.GetComponent<PlayerVoiceManager>().isKickable &&
							obj.GetComponent<PlayerController>().myTeam != GetComponent<PlayerController>().myTeam)
						{
							view.RPC("setStarted", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, 1);
							view.RPC("resetTimer", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);

							if (!obj.GetComponent<PlayerVoiceManager>().kickedBy.Contains(view.ViewID))
							{
								view.RPC("appendKickedBy", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID,
									view.ViewID);
							}
							view.RPC("push", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, view.ViewID);
							if (!obj.GetComponent<PlayerVoiceManager>().healthbar1)
							{
								theirHealthBar = PhotonNetwork.Instantiate(Path.Combine("HealthBar", "Canvas 1"), obj.transform.GetChild(4).position, Quaternion.identity);
								view.RPC("setObjParent", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, theirHealthBar.GetComponent<PhotonView>().ViewID);
							}
							else
							{
								theirHealthBarr = obj.GetComponent<PlayerVoiceManager>().healthbar1.transform.GetChild(0).GetComponent<HealthBar>();
								if (theirHealthBarr.slider.value > 0)
								{
									
									view.RPC("giveDamage", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID,0);
										
								}
								else
								{
									if (obj.GetComponent<PlayerVoiceManager>().kickedBy.Count > 0)
									{
										GameObject.FindGameObjectWithTag("Kick").GetComponent<kickPlayers>().kickPlayer(obj);
										view.RPC("destHB", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);
										view.RPC("setStarted", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID, 0);
										view.RPC("clearKickedBy", RpcTarget.All, obj.GetComponent<PhotonView>().ViewID);
									}
								}

							}

						}

					}

				}
			}

			if (entered1 == false && entered2 == false && !healthbar1)
			{
				view.RPC("setKickableF", RpcTarget.All, view.ViewID);
			}
		}
	}
	void Increment()
    {
		
		timer += Time.deltaTime;
			
    }
	
	[PunRPC]
	void setObjParent(int viewID, int hBviewID)
	{
		GameObject obj1 = PhotonView.Find(viewID).gameObject;
		obj1.GetComponent<PlayerVoiceManager>().healthbar1 = PhotonView.Find(hBviewID).gameObject;
		obj1.GetComponent<PlayerVoiceManager>().healthbar1.transform.SetParent(obj1.transform.GetChild(4));
	}
	[PunRPC]
	void giveDamage(int viewID,int x)
	{
		GameObject obj = PhotonView.Find(viewID).gameObject;
		HealthBar hb = obj.GetComponent<PlayerVoiceManager>().healthbar1.transform.GetChild(0).GetComponent<HealthBar>();
		if (x == 0)
		{
			hb.SetHealth(hb.slider.value - 0.3f);
        }
        else
        {
			hb.SetHealth(hb.slider.value + 0.2f);
		}
	}
	[PunRPC]
	void destHB(int viewID)
	{
		GameObject obj = PhotonView.Find(viewID).gameObject;
		GameObject hb = obj.GetComponent<PlayerVoiceManager>().healthbar1.gameObject;
		Destroy(hb);
	}
	[PunRPC]
	void setKickable(int viewID)
	{
		PlayerVoiceManager PC = PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>();
		PC.isKickable = true;
	}
	[PunRPC]
	void setKickableF(int viewID)
	{
		PlayerVoiceManager PC = PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>();
		PC.isKickable = false;
	}
	[PunRPC]
	void setEntered(int viewID, int x)
	{
		GameObject obj = PhotonView.Find(viewID).gameObject;
		if (x == 1)
		{
			obj.GetComponent<PlayerVoiceManager>().entered1 = true;
		}
		else
		{
			obj.GetComponent<PlayerVoiceManager>().entered2 = true;
		}


	}
	[PunRPC]
	void setEnteredF(int viewID, int x)
	{
		GameObject obj = PhotonView.Find(viewID).gameObject;
		if (x == 1)
		{
			obj.GetComponent<PlayerVoiceManager>().entered1 = false;
		}
		else
		{
			obj.GetComponent<PlayerVoiceManager>().entered2 = false;
		}


	}
	[PunRPC]
	void setPlayed(int viewID, int x)
	{
		if (x == 0)
		{
			PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().played = false;
		}
		else
		{
			PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().played = true;
		}
	}
	[PunRPC]
	void push(int objID, int myID)
	{
		GameObject obj = PhotonView.Find(objID).gameObject;
		GameObject me = PhotonView.Find(myID).gameObject;
		Rigidbody rb = obj.GetComponent<Rigidbody>();
		Vector3 direction = obj.transform.position - me.transform.position;
		direction.y = 0;
		rb.AddForce(direction * 1, ForceMode.Impulse);
	}
	[PunRPC]
	void setStarted(int viewID,int x)
    {
		if (x == 1)
		{
			PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().started = true;
        }
        else
        {
			PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().started = false;
		}
    }
	[PunRPC]
	void resetTimer(int viewID)
    {
		PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().timer = 0;
    }
	[PunRPC]
	void appendKickedBy(int viewID, int myID)
	{
		PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().kickedBy.Add(myID);
	}
	[PunRPC]
	void clearKickedBy(int viewID)
	{
		PhotonView.Find(viewID).GetComponent<PlayerVoiceManager>().kickedBy.Clear();
	}
}