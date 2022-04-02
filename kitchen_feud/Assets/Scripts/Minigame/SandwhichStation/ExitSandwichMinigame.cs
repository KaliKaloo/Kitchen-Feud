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
		CustomProperties.PlayerCookedDishes.AddCookedDishes();

		MusicManager.instance.minigameEnd();
		MusicManager.instance.inMG = false;
		SandwichController.RestartGame();
		
		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All, appliance.GetComponent<PhotonView>().ViewID);

		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

		PhotonView view = appliance.player.GetComponent<PhotonView>();

		view.RPC("EnablePushing", RpcTarget.All, view.ViewID);

		appliance.playerController.enabled = true;
		appliance.player.GetComponentInChildren<Camera>().enabled = true;
		appliance.UIcamera.enabled = false;
		appliance.player.GetComponentInChildren<playerMvmt>().enabled = true;
	}
}