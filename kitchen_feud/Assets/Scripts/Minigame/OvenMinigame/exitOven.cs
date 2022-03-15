using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class exitOven : MonoBehaviour
{

	public Button yourButton;
	public GameObject minigameCanvas;
	public GameObject player;
	public Appliance appliance;
	public PhotonView PV;


	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
		PV = GetComponent<PhotonView>();
	}

	void TaskOnClick()
	{
		GameEvents.current.assignPointsEventFunction();
		//Destroy(minigameCanvas);
		PV.RPC("globalDestroy", RpcTarget.All, minigameCanvas.GetComponent<PhotonView>().ViewID);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

	
	}
	[PunRPC]
	void globalDestroy(int viewID)
	{
		Destroy(PhotonView.Find(viewID).gameObject);
	}
}


