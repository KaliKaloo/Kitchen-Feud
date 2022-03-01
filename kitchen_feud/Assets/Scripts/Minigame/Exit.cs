using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Exit : MonoBehaviour
{
	public bool finishedGame = false;
	public Button yourButton;
	public GameObject canvas;
    public GameObject minigameCanvas;
	public GameObject player;

	public Appliance appliance;
	//private PhotonView view;
	// private List<GameObject> ApplianceObjs = new List<GameObject>();
	int theOne;

	//public Stove stove;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		// ApplianceObjs.AddRange(GameObject.FindGameObjectsWithTag("Appliance"));

		
		btn.onClick.AddListener(TaskOnClick);
		
	
	}

	void TaskOnClick(){
				finishedGame = true;
				canvas.gameObject.SetActive(true);
				minigameCanvas.gameObject.SetActive(false);
				appliance.GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All);
				
				appliance.cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);

				PhotonView	view = appliance.player.GetComponent<PhotonView>();
				
				view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
				
				appliance.playerController.enabled = true;
				
				if(appliance.minigameNum == 0){
                	GameEvents.current.assignPoints -= appliance.GetComponent<stoveMinigame>().UpdateDishPoints;
				}
				else if(appliance.minigameNum == 1){
                	GameEvents.current.assignPoints -= appliance.GetComponent<cuttingMinigame>().UpdateDishPoints;

				}
	}


}
