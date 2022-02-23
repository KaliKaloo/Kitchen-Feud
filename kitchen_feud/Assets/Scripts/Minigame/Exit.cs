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
	public List<Stove> stoves = new List<Stove>();
	int theOne;

	//public Stove stove;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		player = GetComponent<GameObject>();
		
		btn.onClick.AddListener(TaskOnClick);
		
	
	}

	void TaskOnClick(){
		for (int i = 0; i < stoves.Capacity; i++)
		{
			if (stoves[i].cookedDish != null)
			{
				canvas.gameObject.SetActive(true);
				minigameCanvas.gameObject.SetActive(false);
				stoves[i].GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.All);

				
				stoves[i].cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.All);
				
				

				PhotonView	view = stoves[i].player.GetComponent<PhotonView>();
				
				view.RPC("EnablePushing",RpcTarget.All,view.ViewID);
				
				stoves[i].playerController.enabled = true;
				//stoves[i].playerRigidbody.isKinematic = false;
				theOne = i;
                GameEvents.current.assignPoints -= stoves[i].UpdateDishPoints;
			}
		}
		Debug.Log(stoves[theOne].transform.position);
		
	}


}
