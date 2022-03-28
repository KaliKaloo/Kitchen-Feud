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
	}

	void TaskOnClick(){
		MusicManager.instance.minigameEnd();
		MusicManager.instance.inMG = false;


		GameEvents.current.assignPointsEventFunction();
		score.text = "Score: 0/15";
		stoveScore.ResetValues();
		backButton.SetActive(false);
		startButton.SetActive(true);
		topBar.SetActive(true);
		var xBound = Screen.width / 2;
        var lowerBound = pot.position.x - xBound;
        var upperBound = pot.position.x + xBound;
        pot.position = new Vector3(Screen.width / 2, Screen.height / 5.5f, 0);

		canvas.gameObject.SetActive(true);
		minigameCanvas.gameObject.SetActive(false);

		//SOUND ----------------------------------------------------------
		//fix + RPC to others
		//appliance.GetComponent<stoveMinigame>().sound.Stop();
		this.GetComponent<PhotonView>().RPC("StopBoilingSound", RpcTarget.Others);
		appliance.GetComponent<stoveMinigame>().hasPlayed = false;
		//----------------------------------------------------------------

		appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All,appliance.GetComponent<PhotonView>().ViewID);
		appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
		PhotonView	view = appliance.player.GetComponent<PhotonView>();
		view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
		appliance.playerController.enabled = true;
		appliance.player.GetComponentInChildren<Camera>().enabled = true;
		appliance.UIcamera.enabled = false;
		appliance.player.GetComponentInChildren<playerMvmt>().enabled = true;

	}

	void StopBoilingSound() {
        appliance.GetComponent<stoveMinigame>().sound.Stop();
    }
}
