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
		
		//canvas.gameObject.SetActive(true);
	
		GameEvents.current.assignPointsEventFunction();
		//minigameCanvas.GetComponent<Timer>().timer = 10;
		minigameCanvas.gameObject.SetActive(false);
		Debug.LogError(appliance.name);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);
		Debug.LogError("This is the Dish" + appliance.cookedDish.name);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

		//PhotonView view = appliance.player.GetComponent<PhotonView>();

		//view.RPC("EnablePushing", RpcTarget.All, view.ViewID);

		//appliance.playerController.enabled = true;
	}
}


