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
	[SerializeField] private GameObject topBar;

	public Appliance appliance;
	StoveScore stoveScore = new StoveScore();
	StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

	private float prevScore;


	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
		//GameEvents.current.assignPoints += appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove;
	}

	void TaskOnClick(){
		appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove();
		pot.transform.position = new Vector2(Screen.width / 2, Screen.height / 4.3f);
		score.text = "Score: 0/15";
		stoveScore.ResetValues();
		backButton.SetActive(false);
		startButton.SetActive(true);
		topBar.SetActive(true);
		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);

		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
		PhotonView	view = appliance.player.GetComponent<PhotonView>();
		view.RPC("EnablePushing",RpcTarget.All,view.ViewID);

		appliance.playerController.enabled = true;
		//GameEvents.current.makeNull();
		//GameEvents.current.assignPoints -= appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove;
		//GameEvents.current = null;

	}
}
