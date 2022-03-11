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

	}
}
