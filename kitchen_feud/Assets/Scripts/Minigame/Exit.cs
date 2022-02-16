using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
	public Button yourButton;
	//to change, access canvas from stove
	public GameObject canvas;
    public GameObject minigameCanvas;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
        //SceneManager.LoadScene("kitchens_miniGame");
		canvas.gameObject.SetActive(true);
        minigameCanvas.gameObject.SetActive(false);
	}
}
