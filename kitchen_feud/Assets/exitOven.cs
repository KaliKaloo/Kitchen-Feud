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


	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		GameEvents.current.assignPointsEventFunction();
		PhotonNetwork.Destroy(minigameCanvas);
		//minigameCanvas.gameObject.SetActive(false);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

	
	}
}


