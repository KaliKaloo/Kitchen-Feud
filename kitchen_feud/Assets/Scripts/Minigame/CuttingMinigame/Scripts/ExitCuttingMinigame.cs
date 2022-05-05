using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitCuttingMinigame : MonoBehaviour
{
	public Button yourButton;
	public cutController CutController;
	public GameObject canvas;
	public GameObject minigameCanvas;
	public GameObject player;
	public Appliance appliance;
	public Camera UICamera;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		GameObject gamePlayer = GameObject.Find("Local");
		
		PhotonView playerV = gamePlayer.GetPhotonView();
		playerV.RPC("setInMinigameF", RpcTarget.All, playerV.ViewID);

		CustomProperties.PlayerCookedDishes.AddCookedDishes();

		MusicManager.instance.minigameEnd();
		MusicManager.instance.inMG = false;

		CutController.RestartGame();

		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);

		// stop cooking animation
		playerAnimator.animator.SetBool("IsCooking", false);
		
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.AllBuffered,appliance.GetComponent<PhotonView>().ViewID);
		
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.AllBuffered);

		
		playerV.RPC("EnablePushing",RpcTarget.AllBuffered,playerV.ViewID);
		playerV.GetComponent<PlayerController>().enabled = true;
	
		UICamera.enabled = false;
		playerV.GetComponentInChildren<playerMvmt>().enabled = true;
	}
}
