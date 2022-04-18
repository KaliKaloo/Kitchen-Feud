using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitSandwichMinigame : MonoBehaviour
{

	public Button yourButton;
	public GameObject canvas;
	public GameObject minigameCanvas;
	public GameObject player;
	public Appliance appliance;
	public SandwichController SandwichController;


	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		GameObject gamePlayer = GameObject.Find("Local");
		PhotonView playerV = gamePlayer.GetPhotonView();
		CustomProperties.PlayerCookedDishes.AddCookedDishes();

		MusicManager.instance.minigameEnd();
		MusicManager.instance.inMG = false;
		SandwichController.RestartGame();
		
		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.AllBuffered, appliance.GetComponent<PhotonView>().ViewID);

		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.AllBuffered);


		playerV.RPC("EnablePushing", RpcTarget.AllBuffered, view.ViewID);

		gamePlayer.GetComponent<PlayerController>().enabled = true;
		appliance.UIcamera.enabled = false;
		gamePlayer.GetComponentInChildren<playerMvmt>().enabled = true;
	}
}