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

	private ParticleSystem particleSystem;
	private float score;


	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
        cookingBar = slider.GetComponent<CookingBar>();
		particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
		
	}

	void TaskOnClick(){
		score = slider.value;
		slider.value = -30;
        cookingBar.keyHeld = false;
        cookingBar.done = false;
		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);
		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
		PhotonView	view = appliance.player.GetComponent<PhotonView>();
		view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
		appliance.playerController.enabled = true;
		
		smokeEffect();

	}


	void smokeEffect(){
		Debug.Log(slider.maxValue);
		particleSystem.Play();
		var main = particleSystem.main;
		main.startSpeed = 0.5f + (0.25f * score/slider.maxValue);
		
	}
}
