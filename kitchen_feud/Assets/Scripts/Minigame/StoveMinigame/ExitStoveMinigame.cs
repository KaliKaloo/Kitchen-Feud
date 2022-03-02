using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitStoveMinigame : MonoBehaviour
{
	public Slider slider;
    public CookingBar cookingBar;
	public Button yourButton;
	public GameObject canvas;
    public GameObject minigameCanvas;
	public GameObject player;

	public Appliance appliance;


	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
        cookingBar = slider.GetComponent<CookingBar>();
	}

	void TaskOnClick(){
		slider.value = -30;
        cookingBar.keyHeld = false;
        cookingBar.done = false;
		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All);
		
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

		PhotonView	view = appliance.player.GetComponent<PhotonView>();
		
		view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
		
		appliance.playerController.enabled = true;
		
		GameEvents.current.assignPoints -= appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove;
	
	}
}
