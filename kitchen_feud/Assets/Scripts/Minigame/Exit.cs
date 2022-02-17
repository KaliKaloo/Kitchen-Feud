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
	public List<Stove> stoves = new List<Stove>();

	//public Stove stove;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		
	
	}

	void TaskOnClick(){
		for (int i = 0; i < stoves.Capacity; i++)
		{
			if (stoves[i].cookedDish != null)
			{
				canvas.gameObject.SetActive(true);
				minigameCanvas.gameObject.SetActive(false);
				stoves[i].GetComponent<PhotonView>().RPC("SetToFalse", RpcTarget.Others);
				stoves[i].isBeingInteractedWith = false;
				stoves[i].cookedDish.GetComponent<PhotonView>().RPC("EnView", RpcTarget.Others);
				stoves[i].r.enabled = true;
			}
		}
		//stove.playerController.enabled = true;
	}
}
