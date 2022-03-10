using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitStoveMinigame : MonoBehaviour
{
	public Button yourButton;
	public GameObject canvas;
    public GameObject minigameCanvas;
	public GameObject player;

	[SerializeField] private Rigidbody2D pot;
	[SerializeField] private GameObject backButton;
	[SerializeField] private GameObject startButton;
	[SerializeField] private Text score;


	public Appliance appliance;
	StoveScore stoveScore = new StoveScore();
	StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

	private ParticleSystem particleSystem;
	// private float score;
	private float prevScore;


	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);		
	}

	void TaskOnClick(){
		GameEvents.current.assignPointsEventFunction();
		pot.transform.position = new Vector2(485, 70);
		score.text = "Score: 0/15";
		stoveScore.ResetValues();
		backButton.SetActive(false);
		startButton.SetActive(true);
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
		// particleSystem = appliance.GetComponentInChildren<ParticleSystem>();
		// var main = particleSystem.main;
		// var emission = particleSystem.emission;

		// bool isStopped = particleSystem.isStopped;
		// float duration = 20 + (10 * score/slider.maxValue);

		// if (isStopped){
		// 	main.duration = duration;
		// }else if (prevScore != score){
		// 	particleSystem.Stop();
		// 	particleSystem.Clear();
		// 	main.duration = (main.duration + duration)/2 ;
		// }
		
		// particleSystem.Play();

		// float startSpeed = 0.5f + (0.25f * score/slider.maxValue);
		// float rateOverTime;
		// if (score > 0){
		// 	rateOverTime = 15 + (25 * score/slider.maxValue);
		// }else{
		// 	rateOverTime = 15;
		// }


		// if (!isStopped){
		// 	main.startSpeed = (main.startSpeed.constant + startSpeed)/2;
		// 	emission.rateOverTime = (emission.rateOverTime.constant + rateOverTime)/2;
		// }else{
		// 	main.startSpeed = startSpeed;
		// 	emission.rateOverTime = rateOverTime;
		// }
	}
}
