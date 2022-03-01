using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Exit : MonoBehaviour
{
	public Button yourButton;
	public GameObject canvas;
    public GameObject minigameCanvas;
	public GameObject player;
	//private PhotonView view;
	public List<Appliance> appliance = new List<Appliance>();
	int theOne;

	//public Stove stove;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		
		btn.onClick.AddListener(TaskOnClick);
		
	
	}

	void TaskOnClick(){
		for (int i = 0; i < appliance.Capacity; i++)
		{
			if (appliance[i].cookedDish != null)
			{
				canvas.gameObject.SetActive(true);
				minigameCanvas.gameObject.SetActive(false);
				appliance[i].GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All);

				
				appliance[i].cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
				
				

				PhotonView	view = appliance[i].player.GetComponent<PhotonView>();
				
				view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
				
				appliance[i].playerController.enabled = true;
				//stoves[i].playerRigidbody.isKinematic = false;
				theOne = i;
                GameEvents.current.assignPoints -= appliance[i].GetComponent<stoveMinigame>().UpdateDishPoints;
			}
		}
		Debug.Log(appliance[theOne].transform.position);
		
	}


}
