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
	public Camera UICamera;

	[SerializeField] private Rigidbody2D pot;
	[SerializeField] private GameObject backButton;
	[SerializeField] private GameObject startButton;
	[SerializeField] private Text score;
	[SerializeField] private GameObject instructions;

	[SerializeField] private GameObject background1;
	[SerializeField] private GameObject background2;


	public Appliance appliance;
	StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

	private float prevScore;


	void Start () {
		score.text = "Caught: " + StoveMinigameCounter.collisionCounter + "/" + StoveScore.maximum;
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		btn.onClick.AddListener(TaskOnClick);
		//GameEvents.current.assignPoints += appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove;
	}

	void TaskOnClick(){
		GameObject gamePlayer = GameObject.Find("Local");
		PhotonView playerV = gamePlayer.GetPhotonView();
		playerV.RPC("setInMinigameF", RpcTarget.All, playerV.ViewID);
		appliance.GetComponent<stoveMinigame>().UpdateDishPointsStove();
		// MusicManager.instance.minigameEnd();
		// MusicManager.instance.inMG = false;
		MusicManagerOld.instance.minigameEnd();
		MusicManagerOld.instance.inMG = false;

		// stop cooking animation
		playerAnimator.animator.SetBool("IsCooking", false);

		CustomProperties.PlayerCookedDishes.AddCookedDishes();

		// Reset Counters
		StoveMinigameCounter.ResetCounters();

		// Destroy remaining prefabs
		DestroyRemaining();
		
		backButton.SetActive(false);
		startButton.SetActive(true);
		instructions.SetActive(true);
		var xBound = Screen.width / 2;
        var lowerBound = pot.position.x - xBound;
        var upperBound = pot.position.x + xBound;
        pot.position = new Vector3(Screen.width / 2, Screen.height / 5.5f, 0);

		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);

		//TO FIX!
		//SOUND ----------------------------------------------------------
		appliance.gameObject.GetComponent<PhotonView>().RPC("StopBoilingSound", RpcTarget.AllBuffered);
		appliance.GetComponent<stoveMinigame>().hasPlayed = false;
		//----------------------------------------------------------------

		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.AllBuffered,appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.AllBuffered);
		playerV.RPC("EnablePushing",RpcTarget.AllBuffered,playerV.ViewID);


		gamePlayer.GetComponent<PlayerController>().enabled = true;
		UICamera.enabled = false;
		gamePlayer.GetComponentInChildren<playerMvmt>().enabled = true;

	}

	private void DestroyRemaining()
    {
		GameObject currentBackground;

		// gets corresponding background
		if (background1.activeSelf)
			currentBackground = background1;
		else
			currentBackground = background2;

		// destroy game objects of background
		for (var i = currentBackground.transform.childCount - 1; i >= 0; i--)
		{
			Object.Destroy(currentBackground.transform.GetChild(i).gameObject);
		}
	}
}
